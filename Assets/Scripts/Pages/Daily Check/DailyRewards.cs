using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewards : MonoBehaviour, IIncreaserWalletValueAndCardsCount
{
    [SerializeField] private Button _claimButton;

    [SerializeField] private RewardPref _rewardPref;
    [SerializeField] Transform _rewardsGrid;

    [SerializeField] private List<Prize> _rewards = new();

    [SerializeField] private TMP_Text _status;

    [SerializeField] private CristalWallet _cristalWallet;
    [SerializeField] private GoldWallet _goldWallet;
    [SerializeField] private Inventory _inventory;

    private bool _canClaimReward;
    private int _maxStreakCount;
    private float _claimCoolDown = 24f;
    private float _claimDeadLine = 48f;

    private List<RewardPref> _rewardPrefabs = new();

    private int _currentStreak
    {
        get => PlayerPrefs.GetInt("currentStreak", 0);
        set => PlayerPrefs.SetInt("currentStreak", value);
    }

    private DateTime? _lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClaimedTime", null);

            if (string.IsNullOrEmpty(data) == false)
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastClaimedTime", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastClaimedTime");
        }
    }

    public CardCollection CardCollection => throw new NotImplementedException();

    public CristalWallet CristalWallet => _cristalWallet;

    public GoldWallet GoldWallet => _goldWallet;

    public Inventory Inventory => _inventory;


    private void Start()
    {
        _maxStreakCount = _rewards.Count;

        InitPrefabs();
        UpdateRewardsState();
    }

    private void OnEnable()
    {
        _claimButton.onClick.AddListener(ClaimReward);
        StartCoroutine(RewardsStateUpdater());
    }

    private void OnDisable()
    {
        _claimButton.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }

    private void InitPrefabs()
    {
        for (int i = 0; i < _maxStreakCount; i++)
            _rewardPrefabs.Add(Instantiate(_rewardPref, _rewardsGrid, false));
    }


    private IEnumerator RewardsStateUpdater()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            UpdateRewardsState();
        }
    }

    private void UpdateRewardsState()
    {
        _canClaimReward = true;

        if (_lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _lastClaimTime.Value;

            if (timeSpan.TotalHours > _claimDeadLine)
            {
                _lastClaimTime = null;
                _currentStreak = 0;
            }
            else
            {
                if (timeSpan.TotalHours < _claimCoolDown)
                    _canClaimReward = false;
            }
        }

        UpdateRewardsUI();   
    }

    private void UpdateRewardsUI()
    {
        _claimButton.interactable = _canClaimReward;

        if (_canClaimReward == false)
        {
            var nextClaimTime = _lastClaimTime.Value.AddHours(_claimCoolDown);
            var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

            _status.text = $"Left {currentClaimCooldown.Hours}:{currentClaimCooldown.Minutes}:{currentClaimCooldown.Seconds}";
        }
        else
        {
            _status.text = "Claim your rewards";
        }

        for (int i = 0; i < _rewardPrefabs.Count; i++)
            _rewardPrefabs[i].SetRewardData(i, _currentStreak, _rewards[i], _canClaimReward);
    }

    private void ClaimReward()
    {
        if (_canClaimReward == false)
            return;

        _rewards[_currentStreak].TakeItem(this);

        _lastClaimTime = DateTime.UtcNow;
        _currentStreak += 1;

        if (_currentStreak >= _maxStreakCount)
            _currentStreak = _maxStreakCount - 1;
    }
}

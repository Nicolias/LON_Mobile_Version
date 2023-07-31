using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DeckSlot : MonoBehaviour
{
    [SerializeField] private GameObject _emptyFone;
    [SerializeField] private Image _avatar;
    [SerializeField] private StatisticWindow _statisticWindow;

    [SerializeField] private CardStatsPanel _cardStatsPanel;

    private Button _selfButton;
    private CardCell _cardData;

    public bool IsSet { get; private set; }

    public CardCell CardData => _cardData;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(() => _statisticWindow.Render(this));
        _selfButton.interactable = IsSet;
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void SetCard(CardCell cardData)
    {
        IsSet = true;
        _selfButton.interactable = true;
        _cardData = cardData;

        Render();

        _avatar.sprite = CardData.Statistic.UiIcon;
        _cardStatsPanel.Initialize(CardData);
    }

    public void ResetCardData()
    {
        IsSet = false;
        _selfButton.interactable = false;
        _cardData = null;

        Render();
    }

    private void Render()
    {
        _emptyFone.SetActive(IsSet == false);
        _avatar.gameObject.SetActive(IsSet);

        _cardStatsPanel.gameObject.SetActive(IsSet);
    }
}


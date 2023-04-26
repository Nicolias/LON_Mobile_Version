using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CardCellInDeck : CardCell
{
    [SerializeField] private GameObject _emptyFone, _avatar;
    [SerializeField] private StatisticWindow _statisticWindow;

    [SerializeField] private Button _selfButton;

    public bool IsSet { get; set; } 

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

    public void SetCard(CardCollectionCell cardCell)
    {
        base.Render(cardCell);
        IsSet = true;
        _selfButton.interactable = true;
        ChangeRenderStatus(true);
    }

    public void ResetCardData()
    {
        IsSet = false;
        _selfButton.interactable = false;
        _card = null;
        ChangeRenderStatus(false);
    }

    private void ChangeRenderStatus(bool isCardSet)
    {
        _emptyFone.SetActive(isCardSet == false);
        _avatar.SetActive(isCardSet);
    }
}


using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DeckSlot : CardCellView
{
    [SerializeField] private GameObject _emptyFone, _avatar;
    [SerializeField] private StatisticWindow _statisticWindow;

    [SerializeField] private Button _selfButton;
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
        _cardData = cardData;
        IsSet = true;
        _selfButton.interactable = true;

        ChangeRenderStatus(true);
    }

    public void ResetCardData()
    {
        IsSet = false;
        _selfButton.interactable = false;
        _cardData = null;

        ChangeRenderStatus(false);
    }

    private void ChangeRenderStatus(bool isCardSet)
    {
        _emptyFone.SetActive(isCardSet == false);
        _avatar.SetActive(isCardSet);
    }
}


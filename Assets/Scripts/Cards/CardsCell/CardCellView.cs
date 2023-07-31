using System;
using UnityEngine;
using UnityEngine.UI;

public class CardCellView : MonoBehaviour, ICardViewInCollection, ICardViewInEnchance, ICardViewForDelete
{
    [SerializeField] private Image _icon;
    [SerializeField] private CardStatsPanel _cardStatsPanel;

    [SerializeField] private Button _selfButton;
    [SerializeField] private Button _selectButton;

    public event Action<ICardView> OnSelfButtonClicked;
    public event Action<ICardView> OnSelectButtonClicked;

    public CardStatistic Statistic => CardData.Statistic;
    public Card Card => null;
    public CardCell CardData { get; private set; }

    public Sprite UIIcon => CardData.Statistic.UiIcon;

    public Transform Transform => transform;

    public bool IsSelect { get; private set; }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(() => OnSelfButtonClicked?.Invoke(this));
        _selectButton.onClick.AddListener(() => OnSelectButtonClicked?.Invoke(this));
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
        _selectButton.onClick.RemoveAllListeners();
    }

    public void Init(CardCell cardData)
    {
        CardData = cardData;
    }

    void ICardViewInCollection.Render()
    {
        Render();

        _selectButton.gameObject.SetActive(false);
    }

    void ICardViewInEnchance.Render()
    {
        Render();

        _selectButton.gameObject.SetActive(true);
    }

    void ICardViewForDelete.Render()
    {
        IsSelect = false;

        Render();

        _selectButton.gameObject.SetActive(false);

        OnSelfButtonClicked = null;
    }

    void ICardViewForDelete.Select()
    {
        IsSelect = true;

        Color defaultColor = _icon.color;

        defaultColor.a = 0.4f;

        _icon.color = defaultColor;
    }

    void ICardViewForDelete.Unselect()
    {
        IsSelect = false;

        Color defaultColor = _icon.color;

        defaultColor.a = 1f;

        _icon.color = defaultColor;
    }

    private void Render()
    {
        if (_cardStatsPanel == null)
            throw new NullReferenceException("Окно статистики не проинициализированно");

        _icon.sprite = CardData.Statistic.UiIcon;

        _cardStatsPanel.gameObject.SetActive(true);
        _cardStatsPanel.Initialize(CardData);
        _icon.sprite = CardData.Statistic.UiIcon;
    }
}

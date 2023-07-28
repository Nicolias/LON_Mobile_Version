using System;
using UnityEngine;
using UnityEngine.UI;

public class CardCellView : MonoBehaviour, ICardViewInCollection, ICardViewInEnchance
{
    [SerializeField] private Image _icon;
    [SerializeField] private CardStatsPanel _cardStatsPanel;

    [SerializeField] private Button _selfButton;
    [SerializeField] private Button _selectButton;

    public event Action<ICardView> OnSelected;

    public CardStatistic Statistic => CardData.Statistic;
    public Card Card => null;
    public CardCell CardData { get; private set; }

    public Sprite UIIcon => CardData.Statistic.UiIcon;

    public Transform Transform => transform;

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

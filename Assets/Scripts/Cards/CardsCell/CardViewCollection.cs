using Cards;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CardViewCollection : CardCellView
{
    [SerializeField] private Button _button;

    private StatisticWindow _statisticWindow;

    private CardCell _cardData;

    public override CardCell CardData => _cardData;

    public string Discription => CardData.Card.Discription;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenStatisticCard);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void Init(CardCell cardData, StatisticWindow statisticWindow)
    {
        _cardData = cardData;
        _statisticWindow = statisticWindow;
    }

    private void OpenStatisticCard()
    {
        _statisticWindow.Render(this);
    }
}

public abstract class CardCellView : MonoBehaviour, ICard
{
    [SerializeField] private Image _icon;
    [SerializeField] private CardStatsPanel _cardStatsPanel;

    public CardStatistic Statistic => CardData.Statistic;
    public Card Card => null;
    public abstract CardCell CardData { get; }

    public Sprite UIIcon => CardData.UIIcon;

    public void Render(ICard card)
    {
        _icon.sprite = card.UIIcon;

        if (_cardStatsPanel)
        {
            if (card.Statistic.Id != 0)
            {
                _cardStatsPanel.gameObject.SetActive(true);
                _cardStatsPanel.Initialize(card);
                _icon.sprite = card.UIIcon;
            }
        }
    }
}
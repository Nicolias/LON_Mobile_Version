using System.Linq;
using UnityEngine;
using Zenject;

public class CardCollectionView : CardCollectionSort<CardViewCollection>
{
    [SerializeField] private StatisticWindow _statisticWindow;
    [SerializeField] private CardViewCollection _cardCellTemplate;
    [SerializeField] private Transform _container;

    private CardCollection _cardCollection;

    [Inject]
    public void Construct(CardCollection cardCollection)
    {
        _cardCollection = cardCollection;
    }

    private void Awake()
    {
        _cardCollection.OnCardAdded += CreateCardView;
        _cardCollection.OnCardDeleted += DeleteCardsView;

        foreach (var card in _cardCollection.Cards)
            CreateCardView(card);
    }

    private void OnEnable()
    {
        ShowAllCards();
    }

    private void OnDestroy()
    {
        _cardCollection.OnCardAdded -= CreateCardView;
        _cardCollection.OnCardDeleted -= DeleteCardsView;
    }

    private void ShowAllCards()
    {
        ActiveAllCards();

        _cards = _cards.OrderByDescending(e => e.CardData.Statistic.Power).ThenByDescending(e => e.CardData.Card.Rarity).ToList();
        RenderCardsSiblingIndex();
    }

    private void ActiveAllCards()
    {
        foreach (var item in _cards)
            item.gameObject.SetActive(true);
    }

    private void CreateCardView(CardCell newCard)
    {
        var cell = Instantiate(_cardCellTemplate, _container);
        cell.Init(newCard, _statisticWindow);
        _cards.Add(cell);
    }

    private void DeleteCardsView(CardCell cardForDelete)
    {
        var card = _cards.Find(x => x.CardData == cardForDelete);
        _cards.Remove(card);
        Destroy(card.gameObject);
    }
}
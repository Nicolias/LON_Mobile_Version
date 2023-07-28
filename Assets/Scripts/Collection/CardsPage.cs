using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class CardsPage<T> : MonoBehaviour where T : class, ICardView 
{
    [SerializeField] private Transform _container;

    private List<T> _cards = new List<T>();

    private CardsCollection _cardsCollection;

    [SerializeField] protected StatisticWindow StatisticWindow;

    protected List<T> Cards
    {
        get
        {
            return new List<T>(_cards);
        }
    }
    
    [Inject]
    public void Construct(CardsCollection cardCollection)
    {
        _cardsCollection = cardCollection;
    }

    private void OnEnable()
    {
        _cardsCollection.CardCreated += SubscribeOnCardView;
        _cardsCollection.CardDeleted += UnsubscribeOnCardView;

        ShowAllCards();
    }

    private void OnDisable()
    {
        _cardsCollection.CardCreated -= SubscribeOnCardView;
        _cardsCollection.CardDeleted -= UnsubscribeOnCardView;

        foreach (T card in _cards)
            UnsubscribeOnCardView(card);
    }

    public void ShowAllCards()
    {
        _cards = _cardsCollection.GetAllCardsView<T>();
        ChangeCardsParent();

        foreach (T card in _cards)
            SubscribeOnCardView(card);

        RenderAllCards();

        _cards = _cards
            .OrderByDescending(e => e.CardData.Statistic.Power)
            .ThenByDescending(e => e.CardData.Statistic.Rarity)
            .ToList();
    }

    public void SubscribeOnCardView(ICardView newCardCell)
    {
        newCardCell.OnSelected += OnCardSelect;
    }

    public void UnsubscribeOnCardView(ICardView newCardCell)
    {
        newCardCell.OnSelected -= OnCardSelect;
    }

    protected abstract void OnCardSelect(ICardView cardView);

    protected abstract void RenderAllCards();

    private void ChangeCardsParent()
    {
        foreach (T card in _cards)
            card.Transform.parent = _container;
    }
}

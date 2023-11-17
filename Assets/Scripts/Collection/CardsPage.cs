using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class CardsPage<T> : MonoBehaviour where T : class, ICardView 
{
    [SerializeField] protected StatisticWindow StatisticWindow;
    [SerializeField] private Transform _container;

    private List<T> _cards = new List<T>();

    protected List<T> Cards
    {
        get
        {
            return new List<T>(_cards);
        }
    }
    protected CardsCollection CardsCollection { get; private set; }
    
   [Inject]
    public void Construct(CardsCollection cardCollection)
    {
        CardsCollection = cardCollection;
    }

    protected virtual void OnEnable()
    {
        CardsCollection.CardCreated += SubscribeOn;
        CardsCollection.CardDeleted += UnsubscribeFrom;

        ShowAllCards();
    }

    protected virtual void OnDisable()
    {
        CardsCollection.CardCreated -= SubscribeOn;
        CardsCollection.CardDeleted -= UnsubscribeFrom;

        foreach (ICardView card in _cards)
            UnsubscribeFrom(card);
    }

    public void ShowAllCards()
    {
        _cards = CardsCollection.GetAllCardsView<T>();
        ChangeCardsParent();

        foreach (T card in _cards)
            card.Transform.gameObject.SetActive(false);
        RenderAllCards();

        foreach (T card in _cards)
            SubscribeOn(card);

        _cards = _cards
            .OrderByDescending(e => e.CardData.Statistic.Power)
            .ThenByDescending(e => e.CardData.Statistic.Rarity)
            .ToList();

        for (int i = 0; i < _cards.Count; i++)
            _cards[i].Transform.SetSiblingIndex(i);
    }

    protected void SubscribeOn(ICardView cardView)
    {
        cardView.OnSelfButtonClicked += OnCardClicked;
        cardView.OnSelectButtonClicked += OnCardSelected;
    }

    protected void UnsubscribeFrom(ICardView cardView)
    {
        cardView.OnSelfButtonClicked -= OnCardClicked;
        cardView.OnSelectButtonClicked -= OnCardSelected;
    }

    protected abstract void OnCardClicked(ICardView cardView);
    protected abstract void OnCardSelected(ICardView cardView);

    protected abstract void RenderAllCards();

    private void ChangeCardsParent()
    {
        foreach (T card in _cards)
            card.Transform.parent = _container;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class CardsCollection : MonoBehaviour
{
    [SerializeField] private CardCellView _cardCellTemplate;

    [SerializeField] private Transform _container;

    private List<CardCellView> _cards = new();

    public event Action<CardCellView> CardCreated;
    public event Action<CardCellView> CardDeleted;

    public void AddCards(Card[] newCards)
    {
        foreach (var newCard in newCards)
            AddCard(newCard);
    }

    public void AddCard(Card newCardPrefab)
    {
        CardStatistic cardStatistic = new(newCardPrefab);

        CardCellView cardCellView = Instantiate(_cardCellTemplate, _container);
        cardCellView.Init(new CardCell(cardStatistic));

        _cards.Add(cardCellView);

        CardCreated?.Invoke(cardCellView);
    }

    public void DeleteCards(CardCellView[] cardsForDelete)
    {
        foreach (CardCellView card in cardsForDelete)
        {
            _cards.Remove(card);
            Destroy(card.gameObject);
        }
    }

    public List<T> GetAllCardsView<T>() where T : class, ICardView
    {
        List<T> cards = new List<T>();

        foreach (CardCellView cardCellView in _cards)
            cards.Add(cardCellView as T);

        return cards;
    }
}

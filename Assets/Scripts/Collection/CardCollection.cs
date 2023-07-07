using System;
using System.Collections.Generic;

public class CardCollection 
{
    public event Action<CardCell> OnCardAdded;
    public event Action<CardCell> OnCardDeleted;

    private List<CardCell> _cards = new();

    public List<CardCell> Cards => _cards;

    public void AddCards(Card[] newCards)
    {
        foreach (var newCard in newCards)
            AddCard(newCard);
    }

    public void AddCard(Card newCard)
    {
        newCard.Id = 1;

        CardCell cell = new(); 
        _cards.Add(cell);

        OnCardAdded?.Invoke(cell);
    }

    public void DeleteCards(CardCell[] cardsForDelete)
    {
        foreach (var card in cardsForDelete)
        {
            _cards.Remove(card);
            OnCardDeleted?.Invoke(card);
        }
    }
}

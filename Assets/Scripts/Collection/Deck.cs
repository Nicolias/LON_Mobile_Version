using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Deck : MonoBehaviour
{
    public event Action OnDeckActiveChanged;
    public event Action<List<CardCellInDeck>> OnCardChanged;

    [SerializeField] protected List<CardCellInDeck> _cardsInDeck;

    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private StatisticWindow _statisticWindow;
    
    public List<CardCellInDeck> CardsInDeck => _cardsInDeck;

    public bool IsDeckEmpty { get; private set; }

    private void Awake()
    {
        IsDeckEmpty = true;
    }

    private void OnEnable()
    {
        OnCardChanged?.Invoke(_cardsInDeck);
        OnDeckActiveChanged?.Invoke();
    }

    private void OnDisable()
    {
        OnDeckActiveChanged?.Invoke();
    }

    public void SetCardInDeck(CardCollectionCell cardCell)
    {
        if (cardCell == null) throw new ArgumentNullException();

        int? cardPositionInDeck = GetNearbySlotIndex();

        if (cardPositionInDeck == null)
            return;

        if (cardPositionInDeck == _cardsInDeck.Count) throw new ArgumentOutOfRangeException();

        _cardsInDeck[(int)cardPositionInDeck].SetCard(cardCell);

        _cardCollection.DeleteCards(new[] { cardCell });

        IsDeckEmpty = false;
    }

    public void UnsetCardInCollection(CardCellInDeck cardCellInDeck, int cardPosition)
    {
        if (_cardsInDeck[cardPosition].IsSet == false) return;

        _cardCollection.AddCardCell(cardCellInDeck);

        cardCellInDeck.ResetCardData();

        foreach (var card in _cardsInDeck)
        {
            if (card.IsSet == true)
                return;
        }

        IsDeckEmpty = true;
    }

    private int? GetNearbySlotIndex()
    {
        foreach (var card in _cardsInDeck)
        {
            if (card.IsSet == false)
                return card.transform.GetSiblingIndex();
        }

        return null;
    }
}
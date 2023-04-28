using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Deck : MonoBehaviour
{
    public event Action OnDeckActiveChanged;
    public event Action<List<CardCellInDeck>> OnCardChanged;

    [SerializeField] protected List<CardCellInDeck> _deckSlot;

    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private StatisticWindow _statisticWindow;
    
    public List<CardCellInDeck> CardCellsInDeck
    {
        get
        {
            List<CardCellInDeck> cardsInDeck = new();

            foreach (var deckSlots in _deckSlot)
            {
                if (deckSlots.Card != null)
                    cardsInDeck.Add(deckSlots);
            }

            return cardsInDeck;
        }
    }
    
    public bool IsDeckEmpty { get; private set; }

    private void Awake()
    {
        IsDeckEmpty = true;
    }

    private void OnEnable()
    {
        OnCardChanged?.Invoke(_deckSlot);
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

        if (cardPositionInDeck == _deckSlot.Count) throw new ArgumentOutOfRangeException();

        _deckSlot[(int)cardPositionInDeck].SetCard(cardCell);

        _cardCollection.DeleteCards(new[] { cardCell });

        IsDeckEmpty = false;
    }

    public void UnsetCardInCollection(CardCellInDeck cardCellInDeck, int cardPosition)
    {
        if (_deckSlot[cardPosition].IsSet == false) return;

        _cardCollection.AddCardCell(cardCellInDeck);

        cardCellInDeck.ResetCardData();

        foreach (var card in _deckSlot)
        {
            if (card.IsSet == true)
                return;
        }

        IsDeckEmpty = true;
    }

    private int? GetNearbySlotIndex()
    {
        foreach (var deckSlot in _deckSlot)
        {
            if (deckSlot.IsSet == false)
                return deckSlot.transform.GetSiblingIndex();
        }

        return null;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Deck : MonoBehaviour
{
    public event Action OnDeckActiveChanged;
    public event Action<List<DeckSlot>> OnCardChanged;

    [SerializeField] protected List<DeckSlot> _deckSlot;

    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private StatisticWindow _statisticWindow;
    
    public List<DeckSlot> Slots
    {
        get
        {
            List<DeckSlot> cardsInDeck = new();

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

    public void SetCardInDeck(CardCell cardData)
    {
        if (cardData == null) throw new ArgumentNullException();

        int? cardPositionInDeck = GetNearbySlotIndex();

        if (cardPositionInDeck == null) return;
        if (cardPositionInDeck == _deckSlot.Count) throw new ArgumentOutOfRangeException();

        _deckSlot[(int)cardPositionInDeck].SetCard(cardData);
        IsDeckEmpty = false;
    }

    public void UnsetCardInCollection(DeckSlot deckSlot)
    {
        if (_deckSlot[deckSlot.transform.GetSiblingIndex()].IsSet == false) return;

        deckSlot.ResetCardData();

        foreach (var card in _deckSlot)
            if (card.IsSet == true)
                return;

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
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Deck : MonoBehaviour
{
    public event Action OnDeckActiveChanged;
    public event Action<List<DeckSlot>> OnCardChanged;

    [SerializeField] protected List<DeckSlot> _deckSlots;

    private CardsCollection _cardsCollection;
    private List<CardCellView> _cardsViewInDeck = new();

    public List<DeckSlot> Slots
    {
        get
        {
            List<DeckSlot> cardsInDeck = new();

            foreach (var deckSlots in _deckSlots)
            {
                if (deckSlots.CardView != null)
                    cardsInDeck.Add(deckSlots);
            }

            return cardsInDeck;
        }
    }
    
    public bool IsDeckEmpty { get; private set; }

    [Inject]
    public void Construct(CardsCollection cardsCollection)
    {
        _cardsCollection = cardsCollection;
    }

    private void Awake()
    {
        IsDeckEmpty = true;
    }

    private void OnEnable()
    {
        OnCardChanged?.Invoke(_deckSlots);
        OnDeckActiveChanged?.Invoke();
    }

    private void OnDisable()
    {
        OnDeckActiveChanged?.Invoke();
    }

    public void SetCard(ICardView card)
    {
        if (card == null) throw new ArgumentNullException();

        int? cardPositionInDeck = GetNearbySlotIndex();

        if (cardPositionInDeck == null) return;

        _cardsCollection.GiveCard(card as CardCellView);
        _deckSlots[(int)cardPositionInDeck].SetCard(card);

        IsDeckEmpty = false;
    }

    public void Reset(DeckSlot deckSlot)
    {
        if (_deckSlots[deckSlot.transform.GetSiblingIndex()].IsSet == false) return;

        _cardsCollection.TakeCard(deckSlot.CardView as CardCellView);
        deckSlot.ResetCardData();

        foreach (var card in _deckSlots)
            if (card.IsSet == true)
                return;

        IsDeckEmpty = true;
    }

    private int? GetNearbySlotIndex()
    {
        foreach (var deckSlot in _deckSlots)
            if (deckSlot.IsSet == false)
                return deckSlot.transform.GetSiblingIndex();

        return null;
    }
}
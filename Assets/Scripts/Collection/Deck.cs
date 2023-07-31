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
                if (deckSlots.CardData != null)
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
        if (cardPositionInDeck >= _deckSlots.Count) throw new ArgumentOutOfRangeException();

        _cardsViewInDeck.Add(card as CardCellView);
        card.Transform.gameObject.SetActive(false);
        _deckSlots[(int)cardPositionInDeck].SetCard(card.CardData);

        IsDeckEmpty = false;
    }

    public void Reset(DeckSlot deckSlot)
    {
        if (_deckSlots[deckSlot.transform.GetSiblingIndex()].IsSet == false) return;

        foreach (CardCellView cardView in _cardsViewInDeck)
        {
            if (cardView.CardData == deckSlot.CardData)
            {
                _cardsViewInDeck.Remove(cardView);
                cardView.gameObject.SetActive(true);
                break;
            }
        }

        deckSlot.ResetCardData();

        foreach (var card in _deckSlots)
            if (card.IsSet == true)
                return;

        IsDeckEmpty = true;
    }

    private int? GetNearbySlotIndex()
    {
        foreach (var deckSlot in _deckSlots)
        {
            if (deckSlot.IsSet == false)
                return deckSlot.transform.GetSiblingIndex();
        }

        return null;
    }
}
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    #region CardsCollectionInit
    private const int StartingCardsCount = 5;

    [SerializeField] private Card[] _variationCards;

    private CardsCollection _cardCollection;
    #endregion

    [Inject]
    public void Construct(CardsCollection cardsCollection)
    {
        _cardCollection = cardsCollection;
    }

    private void Awake()
    {
        InitStartingCards();
    }

    private void InitStartingCards()
    {
        List<Card> startingCards = new();

        for (int i = 0; i < StartingCardsCount; i++)
            startingCards.Add(_variationCards[Random.Range(0, _variationCards.Length)]);

        _cardCollection.AddCards(startingCards.ToArray());
    }
}

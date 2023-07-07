using System.Collections.Generic;
using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private Card[] _variationCards;

    [SerializeField] private CardCollection _cardCollection;

    private const int _startingCardsCount = 5;

    private void Awake()
    {
        InitStartingCards();
    }

    private void InitStartingCards()
    {
        List<Card> startingCards = new();

        for (int i = 0; i < _startingCardsCount; i++)
            startingCards.Add(_variationCards[Random.Range(0, _variationCards.Length)]);

        _cardCollection.AddCards(startingCards.ToArray());
    }
}

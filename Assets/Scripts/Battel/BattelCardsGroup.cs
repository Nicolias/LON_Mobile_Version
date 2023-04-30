using Cards.Card;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattelCardsGroup : MonoBehaviour
{
    [SerializeField] private CardAnimator _battelCardTemplate;
    [SerializeField] private Transform _container;

    private List<Card> _dataCardsInGroup;
    private List<CardAnimator> _cardsInGroup = new();

    public IEnumerator Initialize(List<CardCellInDeck> currentDeckCards)
    {
        List<Card> cardsInDeck = new();

        foreach (var cardCell in currentDeckCards)
            cardsInDeck.Add(cardCell.Card);

        yield return Initialize(cardsInDeck);
    }

    public IEnumerator Initialize(List<Card> currentDeckCards)
    {
        _dataCardsInGroup = currentDeckCards;
        yield return CreateBattleCard();
    }

    private IEnumerator CreateBattleCard()
    {
        List<CardAnimator> newBattelCards = new();

        foreach (Card cardInGroup in _dataCardsInGroup)
        {
            var newBattelCard = Instantiate(_battelCardTemplate, _container);
            yield return newBattelCard.Initialize(cardInGroup);
            newBattelCards.Add(newBattelCard);            
        }

        _cardsInGroup = newBattelCards;
    }
}

using Cards.Card;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BattelCardsGroup : MonoBehaviour
{
    [SerializeField] private CardAnimator _battelCardTemplate;
    [SerializeField] private Transform _container;

    private List<Card> _dataCardsInGroup;
    private List<CardAnimator> _cardsInGroup = new();

    public void Initialize(List<CardCellInDeck> currentDeckCards)
    {
        List<Card> cardsInDeck = new();

        foreach (var cardCell in currentDeckCards)
            cardsInDeck.Add(cardCell.Card);

        Initialize(cardsInDeck);
    }

    public void Initialize(List<Card> currentDeckCards)
    {
        _dataCardsInGroup = currentDeckCards;
        _cardsInGroup = CreateBattleCard();
    }

    public IEnumerator PlayFallAnimation()
    {
        foreach (var card in _cardsInGroup)
        {
            yield return card.StartIntro();
        }
    }

    private List<CardAnimator> CreateBattleCard()
    {
        List<CardAnimator> newBattelCards = new();

        foreach (Card cardInGroup in _dataCardsInGroup)
        {
            var newBattelCard = Instantiate(_battelCardTemplate, _container);
            newBattelCard.Initialize(cardInGroup);

            newBattelCards.Add(newBattelCard);
        }

        return newBattelCards;
    }
}

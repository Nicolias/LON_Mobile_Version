using Cards.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattelCardsFactory
{
    private DiContainer _di;

    private CardAnimator _battelCardTemplate;
    private Transform _container;

    private List<Card> _dataCardsInGroup;
    private List<CardAnimator> _cardsInGroup;

    public BattelCardsFactory(CardAnimator battelCardTemplate, Transform container, List<Card> dataCardsInGroup, DiContainer di)
    {
        _battelCardTemplate = battelCardTemplate;
        _container = container;
        _dataCardsInGroup = dataCardsInGroup;
        _di = di;
    }

    public IEnumerator CreateBattleCard()
    {
        List<CardAnimator> newBattelCards = new();
        _cardsInGroup = new();

        foreach (Card cardInGroup in _dataCardsInGroup)
        {
            var newBattelCard = _di.InstantiatePrefabForComponent<CardAnimator>(_battelCardTemplate, _container);
            yield return newBattelCard.Initialize(cardInGroup);
            newBattelCards.Add(newBattelCard);
        }

        _cardsInGroup = newBattelCards;
    }

    public List<CardAnimator> GetCreatedCards()
    {
        if (_cardsInGroup == null)
            throw new System.Exception("For begining need to create cards");

        List<CardAnimator> cardsInGrop = new();
        cardsInGrop.AddRange(_cardsInGroup);
        _cardsInGroup = null;

        return cardsInGrop;
    }
}
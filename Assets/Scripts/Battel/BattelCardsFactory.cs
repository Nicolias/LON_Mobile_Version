using Cards.BattelCard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattelCardsFactory
{
    private DiContainer _di;

    private BattelCard _battelCardTemplate;
    private Transform _container;

    private List<Card> _dataCardsInGroup;
    private List<BattelCard> _cardsInGroup;

    public BattelCardsFactory(BattelCard battelCardTemplate, Transform container, List<Card> dataCardsInGroup, DiContainer di)
    {
        _battelCardTemplate = battelCardTemplate;
        _container = container;
        _dataCardsInGroup = dataCardsInGroup;
        _di = di;
    }

    public IEnumerator CreateBattleCard()
    {
        if (_battelCardTemplate == null) throw new System.NullReferenceException();

        List<BattelCard> newBattelCards = new();
        _cardsInGroup = new();

        foreach (Card cardInGroup in _dataCardsInGroup)
        {
            var newBattelCard = _di.InstantiatePrefabForComponent<BattelCard>(_battelCardTemplate, _container);
            yield return newBattelCard.Initialize(cardInGroup);
            newBattelCards.Add(newBattelCard);
        }

        _cardsInGroup = newBattelCards;
    }

    public List<BattelCard> GetCreatedCards()
    {
        if (_cardsInGroup == null)
            throw new System.Exception("For begining need to create cards");

        List<BattelCard> cardsInGrop = new();
        cardsInGrop.AddRange(_cardsInGroup);
        _cardsInGroup = null;

        return cardsInGrop;
    }
}
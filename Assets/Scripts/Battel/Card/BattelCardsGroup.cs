﻿using Cards.BattelCard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BattelCardsGroup : MonoBehaviour
{
    [SerializeField] private BattelCard _battelCardTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;

    private DiContainer _di;

    private BattelCardsFactory _battelCardsFactory;

    private List<BattelCard> _cardsInGroup;
    private BattelCard _currentCharacter;

    [Inject]
    public void Constract(DiContainer di)
    {
        _di = di;
    }

    public List<BattelCard> CardsInGroup => _cardsInGroup;

    public IEnumerator Initialize(List<DeckSlot> currentDeckCards)
    {
        List<Card> cardsInDeck = new();

        foreach (var cardCell in currentDeckCards)
            cardsInDeck.Add(cardCell.CardView.Card);

        yield return Initialize(cardsInDeck);
    }

    public IEnumerator Initialize(List<Card> currentDeckCards)
    {
        _gridLayoutGroup.enabled = true;

        _battelCardsFactory = new(_battelCardTemplate, _container, currentDeckCards, _di);
        yield return _battelCardsFactory.CreateBattleCard();
        _cardsInGroup = _battelCardsFactory.GetCreatedCards();

        foreach (var cardInGroup in _cardsInGroup)
            cardInGroup.OnDead += () => _cardsInGroup.Remove(cardInGroup);

        _gridLayoutGroup.enabled = false;
    }

    public IEnumerator Turn(BattelCardsGroup enemiesGroup)
    {
        if (_cardsInGroup == null)
            throw new System.Exception("Cards isn't inizialized");

        _cardsInGroup.Shuffle();
        
        for (int i = 0; i < _cardsInGroup.Count; i++)
        {
            if (enemiesGroup.CardsInGroup.Count == 0)
                yield break;

            _currentCharacter = _cardsInGroup[i];
            _currentCharacter.transform.SetSiblingIndex(_cardsInGroup.Count);

            yield return _currentCharacter.AttackEnemy(enemiesGroup);
        }        
    }       
}

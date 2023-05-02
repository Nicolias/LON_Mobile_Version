using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using Cards.Card;
using DG.Tweening;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;
using System.Linq;

namespace FarmPage.Battle
{
    public class BattleController : Battel
    {
        private static readonly int Effect = Animator.StringToHash("Effect");

        [SerializeField] private BattleAnimator _battleAnimator;

        [SerializeField] private BattleIntro _roundIntro;

        [SerializeField] private BattelCard[] _enemyCardAnimators;

        [SerializeField] private BattelCard[] _playerCardAnimators;

        [SerializeField] private Shaking shaking;

        [SerializeField] private Animator _turnEffect;

        [SerializeField] private GameObject _battleChouse;

        [SerializeField] private Window _winWindow;
        
        [SerializeField] private Window _loseWindow;

        [SerializeField] private PrizeWindow _prizeWindow;

        [SerializeField] private AttackDeck _attackDeck;

        private EnemyBattle _enemy;
        private Card[] _enemyCards;

        public override void Initialize(EnemyBattle enemy)
        {
            _enemy = enemy;

            RenderEnemyDefCard();
            
            gameObject.SetActive(true);

            RenderPlayerCards(_attackDeck.CardCellsInDeck, _playerCardAnimators);

            StartCoroutine(Fight());
        }

        private void RenderEnemyDefCard()
        {
            //_enemyCards = _enemy.Cards.ToArray();
            
            //for (int i = 0; i < _enemy.Cards.Count; i++)
            //{
            //    _enemyCardAnimators[i].Initialize(_enemyCards[i]);
            //}
        }

        private IEnumerator Fight()
        {
            yield return _battleAnimator.AppearanceCards(_enemyCardAnimators, _playerCardAnimators,
                GetAliveCards(GetCardArrayFrom(_attackDeck.CardCellsInDeck)), GetAliveCards(_enemyCards));

            int roundCounter = 0;
            while (GetAmountCardsHealth(_enemyCardAnimators) > 0 && GetAmountCardsHealth(_playerCardAnimators) > 0)
            {
                roundCounter++;
                yield return _roundIntro.PlayRoundIntro(roundCounter);
                yield return new WaitForSeconds(0.5f);
                yield return roundCounter % 2 != 0 ? PlayerTurn() : EnemyTurn();
            }

            if (GetAmountCardsHealth(_playerCardAnimators) >= GetAmountCardsHealth(_enemyCardAnimators))
                yield return PlayerWin();
            else
                yield return PlayerLose();
        }

        private IEnumerator PlayerTurn()
        {
            var playerAliveCardNumbers = GetAliveCards(GetCardArrayFrom(_attackDeck.CardCellsInDeck));
            var enemyAliveCardNumbers = GetAliveCards(_enemyCards);

            yield return Turn(playerAliveCardNumbers, enemyAliveCardNumbers,
                _playerCardAnimators.ToList(), _enemyCardAnimators.ToList(),
                GetCardArrayFrom(_attackDeck.CardCellsInDeck).ToList(), _enemyCards.ToList());
        }

        private IEnumerator EnemyTurn()
        {
            var playerAliveCardNumbers = GetAliveCards(GetCardArrayFrom(_attackDeck.CardCellsInDeck));
            var enemyAliveCardNumbers = GetAliveCards(_enemyCards);

            yield return Turn(enemyAliveCardNumbers, playerAliveCardNumbers,
                _enemyCardAnimators.ToList(), _playerCardAnimators.ToList(),
                _enemyCards.ToList(), GetCardArrayFrom(_attackDeck.CardCellsInDeck).ToList());
        }

            private IEnumerator Turn(List<int> myAliveCardNumbers, List<int> opponentAliveCardNumbers, 
            List<BattelCard> myCardAnimators, List<BattelCard> opponentCardAnimators, List<Card> myCards, List<Card> opponentCards)
        {
            var randomMyCardDamageCount = _enemyCardAnimators.Length < 2 ? 1 : 2;

            for (int j = 0; j < randomMyCardDamageCount; j++)
            {
                if (FindRandomAliveCard(opponentCardAnimators) == null)
                    yield break;

                var myCardAnimator = FindRandomAliveCard(myCardAnimators);
                if (myCardAnimator == null) throw new InvalidOperationException();

                //Card randomMyCard = myCardAnimator.Card;
                Card randomMyCard = null;

                //myCardAnimator.Select();
                yield return new WaitForSeconds(0.2f);

                var randomOpponentCardDamageCount = 1;

                if (opponentCardAnimators.Count > 2)
                    randomOpponentCardDamageCount = Random.Range(1, 3);

                var attackEffect = randomMyCard.AttackEffect;
                var attack = randomMyCard.Attack;

                if (IsRandomChance(randomMyCard.SkillChance))
                {
                    var skillEffect = randomMyCard.SkillEffect;
                    
                    foreach (var opponentCardAnimator in opponentCardAnimators)
                        StartCoroutine(opponentCardAnimator.TakeDamage(skillEffect, attack));

                    yield return new WaitForSeconds(0.2f);
                    shaking.Shake(0.5f, 10);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    for (int k = 0; k < randomOpponentCardDamageCount; k++)
                    {
                        BattelCard opponentCardAnimator = FindRandomAliveCard(opponentCardAnimators);

                        if (opponentCardAnimator == null) yield break;

                        var myAnimatorPosition = myCardAnimator.transform.position;
                        var opponentAnimatorPosition = opponentCardAnimator.transform.position;
                            
                        float angleTurnEffect = 
                            Mathf.Atan2(myAnimatorPosition.y - opponentAnimatorPosition.y, 
                                myAnimatorPosition.x - opponentAnimatorPosition.x) * Mathf.Rad2Deg;
                            
                        var turnEffectPosition =
                            new Vector3(
                                (myAnimatorPosition.x + opponentAnimatorPosition.x) / 2, 
                                transform.position.y, transform.position.z);
                            
                        var turnEffect = 
                            Instantiate(_turnEffect, turnEffectPosition, new Vector3(0, 0, angleTurnEffect)
                                .EulerToQuaternion(), transform);

                        var turnEffectImage = turnEffect.GetComponentInChildren<Image>();
                        turnEffectImage.color = Color.clear;
                        turnEffectImage.DOColor(Color.white, 0.2f);
                            
                        var ratioScale = 1f;
                        var ratioScaleRotation = (opponentAnimatorPosition.x - myAnimatorPosition.x) < 0 ? 1 : -1;
                        var scale = ratioScale * (opponentAnimatorPosition.x - myAnimatorPosition.x) *
                                    ratioScaleRotation;

                        if (Math.Abs(scale) < 1f)
                            scale = -1;
                            
                        turnEffect.transform.localScale = turnEffect.transform.localScale.ToX(scale);
                        turnEffect.SetTrigger(Effect);
                            
                        StartCoroutine(opponentCardAnimator.TakeDamage(attackEffect, attack));

                        
                        yield return new WaitForSeconds(0.2f);
                        shaking.Shake(0.5f, 10);
                        yield return new WaitForSeconds(0.1f);
                            
                        turnEffectImage.DOColor(Color.clear, 0.2f).OnComplete(()=>Destroy(turnEffect.gameObject));
                    }

                    //myCardAnimator.Unselect();
                    
                    yield return new WaitForSeconds(0.7f);
                }
            }

            yield return new WaitForSeconds(1);
        }

        private BattelCard FindRandomAliveCard(List<BattelCard> opponents)
        {
            var randomOpponent = opponents[Random.Range(0, opponents.Count)];

            if (randomOpponent != null & randomOpponent.HealthLeft > 0)
                return randomOpponent;
            else
                foreach (var opponent in opponents)
                    if (opponent != null & opponent.HealthLeft > 0)
                        return FindRandomAliveCard(opponents);

            return null;
        }

        private bool IsRandomChance(float chance) => 
            Random.Range(0, 10000) <= (int)(chance * 100);

        private int GetAmountCardsHealth(BattelCard[] cards)
        {
            int amountHealt = 0;

            foreach (BattelCard card in cards)
                if (card != null)
                amountHealt += card.HealthLeft;

            return amountHealt;
        }

        private List<int> GetAliveCards(Card[] cards)
        {
            var aliveCards = new List<int>();
            
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != null) 
                    aliveCards.Add(i);
            }

            return aliveCards;
        }
    
        private void RenderPlayerCards(List<CardCellInDeck> cardCells, BattelCard[] cardAnimators)
        {
            //for (int i = 0; i < cardCells.Count; i++)
            //{
            //    cardAnimators[i].Initialize(cardCells[i].Card);
            //}
        }

        private Card[] GetCardArrayFrom(List<CardCellInDeck> cardsInDeck)
        {
            var cards = new Card[cardsInDeck.Count];

            for (int i = 0; i < cardsInDeck.Count; i++)
            {
                cards[i] = cardsInDeck[i].Card;
            }

            return cards;
        }

        private IEnumerator PlayerWin()
        {
            yield return new WaitForSeconds(1);
            _winWindow.ShowSmooth(_enemy);
            _prizeWindow.Render(_enemy.RandomPrizes);
        }

        private IEnumerator PlayerLose()
        {
            yield return new WaitForSeconds(1);
            _loseWindow.ShowSmooth(_enemy);
        }
    }
}

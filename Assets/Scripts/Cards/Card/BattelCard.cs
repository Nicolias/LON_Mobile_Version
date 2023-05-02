using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Cards.Card
{
    public class BattelCard : MonoBehaviour
    {
        public event UnityAction OnDead;

        [SerializeField] private BattelCardUI _battelCardUi;
        [SerializeField] private CardStatsPanel _cardStatsPanel;

        private global::Card _card;

        public int HealthLeft => _cardStatsPanel.Health;

        public IEnumerator Initialize(global::Card card)
        {
            _card = card;

            _battelCardUi.Initialize(this, card, _cardStatsPanel);

            _battelCardUi.Hide();

            yield return _battelCardUi.AnimateIntro();
        }       

        public IEnumerator AttackEnemy(BattelCardsGroup enemiesGroup)
        {
            BattelCard enemy = SelectEnemy(enemiesGroup);

            yield return _battelCardUi.AnimateAttackEnemy(enemy, 
                () => enemy.TakeDamage(_card.SkillEffect, _card.Attack));
        }

        public void TakeDamage(ParticleSystem attackEffect, int damage)
        {
            _cardStatsPanel.DecreaseHealth(damage);

            StartCoroutine(_battelCardUi.AnimateTakeDamge(attackEffect,
                _cardStatsPanel.DamageAfterRessist));

            if (_cardStatsPanel.Health == 0)
                OnDead?.Invoke();
        }      


        private BattelCard SelectEnemy(BattelCardsGroup enemiesGroup)
            {
                if (enemiesGroup.CardsInGroup.Count != 0)
                    return enemiesGroup.CardsInGroup[UnityEngine.Random.Range(0, enemiesGroup.CardsInGroup.Count)];
                else
                    return null;
            }
    }
}
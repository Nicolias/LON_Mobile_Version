using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Cards.BattelCard
{
    public class BattelCard : MonoBehaviour
    {
        public event UnityAction OnDead;

        [SerializeField] private BattelCardUI _battelCardUi;
        [SerializeField] private CardStatsPanel _cardStatsPanel;

        private Card _card;
        private CoroutineServise _coroutineServise;

        public int HealthLeft => _cardStatsPanel.Health;

        [Inject]
        public void Construct(CoroutineServise coroutineServise)
        {
            _coroutineServise = coroutineServise;
        }

        public IEnumerator Initialize(Card card)
        {
            _card = card;

            _battelCardUi.Initialize(this, card, _cardStatsPanel, _coroutineServise);

            _battelCardUi.Hide();

            yield return _battelCardUi.AnimateIntro();
        }

        public void TakeDamage(ParticleSystem attackEffect, int damage)
        {
            _cardStatsPanel.DecreaseHealth(damage);

            if (_cardStatsPanel.Health == 0)
                OnDead?.Invoke();

            _battelCardUi.AnimateTakeDamge(attackEffect, _cardStatsPanel.DamageAfterRessist);
        }

        public IEnumerator AttackEnemy(BattelCardsGroup enemiesGroup)
        {
            BattelCard enemy = SelectEnemy(enemiesGroup);

            if (enemy == null)
                yield break;

            yield return _battelCardUi.AnimateAttackEnemy(enemy,
                () => enemy.TakeDamage(_card.SkillEffect, _card.Attack));
        }

        private BattelCard SelectEnemy(BattelCardsGroup enemiesGroup)
        {
            if (enemiesGroup.CardsInGroup.Count != 0)
                return enemiesGroup.CardsInGroup[Random.Range(0, enemiesGroup.CardsInGroup.Count)];
            else
                return null;
        }
    }
}
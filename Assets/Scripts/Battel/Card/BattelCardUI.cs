using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.BattelCard
{
    [Serializable]
    public class BattelCardUI
    {
        #region Field from inspector
        [SerializeField] private Image _cardAvatar, _deadImage, _selectImage, _magicCircleImage;

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _fallAnimatino;
        [SerializeField] private float _fallSpeed;

        [SerializeField] private ParticleSystem _burstParticleWhenCircleFalt;
        [SerializeField] private Transform _effectContainer;

        [SerializeField] private TextMeshProUGUI[] _damageTexts;
        
        [SerializeField] private GameObject _frame;

        [SerializeField] private Color _numberNormalColor;
        #endregion

        private CoroutineServise _coroutineServise;
        private CardStatsPanel _cardStatsPanel;
        private Transform transform;

        private BattelCardAttackAnimation _attackAnimation;

        private Vector3 _scale;
        private Vector3 _localPosition;

        public void Initialize(BattelCard battelCard, Card card, 
            CardStatsPanel cardStatsPanel, CoroutineServise coroutineServise)
        {
            _coroutineServise = coroutineServise;

            transform = battelCard.transform;
            _cardStatsPanel = cardStatsPanel;

            _cardAvatar.sprite = card.UIIcon;
            //_cardStatsPanel.Initialize(card);

            transform.localPosition = transform.localPosition.ToY(100);
            _animator.SetFloat("Fall Speed", _fallSpeed);
            _deadImage.DOColor(new Color(0, 0, 0, 0), 0);

            _attackAnimation = new(battelCard, _selectImage);
        }

        public void Hide()
        {
            _magicCircleImage.color = Color.white;
            _cardAvatar.gameObject.SetActive(false);
            _frame.gameObject.SetActive(false);
            _cardStatsPanel.gameObject.SetActive(false);
        }

        public IEnumerator AnimateIntro()
        {
            _magicCircleImage.gameObject.SetActive(true);

            _animator.SetTrigger("Intro");
            yield return new WaitForSeconds(_fallAnimatino.length / _fallSpeed);

            _magicCircleImage.gameObject.SetActive(false);

            _burstParticleWhenCircleFalt.Play();
            yield return new WaitForSeconds(_burstParticleWhenCircleFalt.main.duration);

            _frame.SetActive(true);
            _cardStatsPanel.gameObject.SetActive(true);
            _cardAvatar.gameObject.SetActive(true);

            _localPosition = transform.localPosition;
            _scale = transform.localScale;
        }

        public IEnumerator AnimateAttackEnemy(BattelCard enemy, Action enemyTakeDamageMethod)
        {
            yield return _attackAnimation.AnimateAttackEnemy(enemy, enemyTakeDamageMethod, _scale, _localPosition);
        }

        public void AnimateTakeDamge(ParticleSystem attackEffect, int damage)
        {
            _coroutineServise.StartRoutine(InstantiateEffect(attackEffect));
                        
            ShowTakenDamage(damage);

            if (_cardStatsPanel.Health == 0)
                _deadImage.DOColor(new Color(0, 0, 0, 0.5f), 1);
        }

        private void ShowTakenDamage(int damage)
        {
            var damageText = _damageTexts[0];

            if (damage <= 0)
            {
                damageText.DOColor(Color.gray, 0.3f);
            }
            else
            {
                damageText.DOColor(new Color(1, 0, 0, 1), 0.3f);
                _cardStatsPanel.HealthText.color = Color.red;
            }

            damageText.text = '-' + damage.ToString();

            _coroutineServise.StartRoutine(ResetDamgeText());

            IEnumerator ResetDamgeText()
            {
                yield return new WaitForSeconds(0.6f);
                damageText.DOColor(new Color(1, 0, 0, 0), 0.3f);

                if (damage <= 0)
                    damageText.DOColor(new Color(0.5f, 0.5f, 0.5f, 0), 0.3f);
                else
                    damageText.DOColor(new Color(1, 0, 0, 0), 0.3f);

                _cardStatsPanel.HealthText.color = _numberNormalColor;
            }
        }

        private IEnumerator InstantiateEffect(ParticleSystem attackEffect)
        {
            var effect = GameObject.Instantiate(attackEffect, _effectContainer);
            effect.transform.localPosition = new(effect.transform.localPosition.x, effect.transform.localPosition.y, -4);
            effect.Play();

            yield return Shake();
            yield return new WaitUntil(() => effect.isPlaying == false);
            GameObject.Destroy(effect);

        }

        private IEnumerator Shake()
            {
                var startLocalPosition = transform.localPosition;

                for (int i = 0; i < 10; i++)
                {
                    var multiplier = 1 - (i / 9);

                    transform.DOLocalMove(transform.localPosition.RandomVector2(10 * multiplier), 0.005f);
                    yield return new WaitForSeconds(0.005f);
                    transform.DOLocalMove(startLocalPosition, 0.005f);
                    yield return new WaitForSeconds(0.005f);
                }
            }
    }
}
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.BattelCard
{
    public class BattelCardAttackAnimation
    {
        private Image _selectImage;

        private Transform transform;

        private Vector3 _scale;
        private Vector3 _localPosition;

        public BattelCardAttackAnimation(BattelCard battelCard)
        {
            transform = battelCard.transform;
        }

        public IEnumerator AnimateAttackEnemy(BattelCard enemy, Action enemyTakeDamageMethod, Vector3 scale, Vector3 localPosition)
        {
            if (enemy == null)
                yield break;

            _scale = scale;
            _localPosition = localPosition;
            Vector3 startPosition = transform.position;

            Sequence sequence = DOTween.Sequence();

            AnimateSelect();
            AnimateMoveToAndAttack(enemy, sequence, enemyTakeDamageMethod);

            yield return new WaitUntil(() => sequence.IsPlaying() == false);

            AnimateUnselect();
        }

        private void AnimateSelect()
        {
            DOTween.Sequence()
                .Insert(0, _selectImage.DOColor(new Color(1, 1, 1, 0.5f), 0.5f))
                .Insert(0, transform.DOLocalMove(_localPosition + new Vector3(0, 50, 0), 0.2f))
                .Insert(0, transform.DOScale(_scale * 1.2f, 0.2f))
                .Insert(0.5f, _selectImage.DOColor(Color.clear, 0.5f));
        }

        private void AnimateMoveToAndAttack(BattelCard enemy, Sequence sequence, Action enemyTakeDamageMethod)
        {
            sequence
                .Append(transform.DORotate(new Vector3(0, 0, GetZAngelBetweenCurrentCharacterAnd(enemy)), 0.4f).SetEase(Ease.InCubic))
                .Append(transform.DOMove(enemy.transform.position, 0.7f).SetEase(Ease.InOutBack))
                .AppendCallback(() => enemyTakeDamageMethod.Invoke())
                .Append(transform.DORotate(new Vector3(0, 0, 0), 0.4f));
        }

        private float GetZAngelBetweenCurrentCharacterAnd(BattelCard enemy)
        {
            float x = enemy.transform.position.y - transform.position.y;
            float y = enemy.transform.position.x - transform.position.x;

            return Mathf.Atan(-y / x) * 180 / Mathf.PI;
        }

        private void AnimateUnselect()
        {
            DOTween.Sequence()
                .Insert(0, transform.DOLocalMove(_localPosition, 0.2f))
                .Insert(0, transform.DOScale(_scale, 0.2f));
        }
    }
}
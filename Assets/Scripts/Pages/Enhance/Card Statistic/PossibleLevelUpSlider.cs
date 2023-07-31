using TMPro;
using UnityEngine;

namespace QuestPage.Enhance.Card_Statistic
{
    [RequireComponent(typeof(SliderAnimator))]
    public class PossibleLevelUpSlider : MonoBehaviour
    {
        [SerializeField] private PossibleIncreaseLevelTextAnimator _possibleIncreaseLevelTextAnimator;
        [SerializeField] private SliderAnimator _increaseLevelPointSlider;

        [SerializeField] private TMP_Text _levelPointText, _increaseLevelPointText;

        private ICardView _upgradeCard;

        private int _levelPointUpgradeCard;
        private int _increaseLevelPoint;

        private float _lastMaxLevelPointUpgradeCard;
        private float _maxLevelPointUpgradeCard;

        private int _howMuchIncreaseLevel;
        public int HowMuchIncreaseLevel => _howMuchIncreaseLevel;

        private void OnDisable()
        {
            Reset();
        }

        public void Reset()
        {
            _howMuchIncreaseLevel = 0;
            _increaseLevelPointSlider.UpdateSlider(0);
            _possibleIncreaseLevelTextAnimator.Reset();
        }

        public void SetUpgradeCard(ICardView upgradeCard)
        {
            _increaseLevelPoint = 0;
            _increaseLevelPointText.text = "";

            _upgradeCard = upgradeCard;
            _howMuchIncreaseLevel = 0;
            _possibleIncreaseLevelTextAnimator.Reset();

            _levelPointUpgradeCard = _upgradeCard.CardData.LevelPoint;

            _maxLevelPointUpgradeCard = _upgradeCard.CardData.MaxLevelPoint;
            _lastMaxLevelPointUpgradeCard = _maxLevelPointUpgradeCard;
        
            _increaseLevelPointSlider.UpdateSlider(_upgradeCard.CardData.LevelPoint, _upgradeCard.CardData.MaxLevelPoint);
            _levelPointText.text = $"{_upgradeCard.CardData.LevelPoint}/{_upgradeCard.CardData.MaxLevelPoint}";
        }

        public void IncreasePossibleSliderLevelPoints(CardCell cardForDelete)
        {
            if (_upgradeCard.CardData.Level + _howMuchIncreaseLevel > _upgradeCard.CardData.MaxLevel || _maxLevelPointUpgradeCard == 0) throw new System.InvalidOperationException();

            _levelPointUpgradeCard += cardForDelete.GetCardDeletePoint();
            _increaseLevelPoint += cardForDelete.GetCardDeletePoint();

            _increaseLevelPointSlider.UpdateSlider(_levelPointUpgradeCard, _upgradeCard.CardData.MaxLevelPoint);

            while (_levelPointUpgradeCard >= _maxLevelPointUpgradeCard)
            {
                _howMuchIncreaseLevel++;
                _possibleIncreaseLevelTextAnimator.LevelUp($"+ {_howMuchIncreaseLevel}");

                if (_upgradeCard.CardData.Level + _howMuchIncreaseLevel >= _upgradeCard.CardData.MaxLevel) 
                    _possibleIncreaseLevelTextAnimator.LevelUp("MAX");

                _lastMaxLevelPointUpgradeCard *= _upgradeCard.CardData.NextMaxLevelPoitnMultiplier;
                _maxLevelPointUpgradeCard += _lastMaxLevelPointUpgradeCard;
            }

            _increaseLevelPointText.text = $"+{_increaseLevelPoint}";
        }

        public void DecreasePossibleSliderLevelPoints(CardCell cardForDelete)
        {
            _levelPointUpgradeCard -= cardForDelete.GetCardDeletePoint();
            _increaseLevelPoint -= cardForDelete.GetCardDeletePoint();
            _increaseLevelPointSlider.UpdateSlider(_levelPointUpgradeCard, _upgradeCard.CardData.MaxLevelPoint);

            while (_levelPointUpgradeCard + 0.01f < _maxLevelPointUpgradeCard - _lastMaxLevelPointUpgradeCard)
            {
                _howMuchIncreaseLevel--;
                _possibleIncreaseLevelTextAnimator.LevelUp($"+ {_howMuchIncreaseLevel}");

                if (_howMuchIncreaseLevel == 0)
                    _possibleIncreaseLevelTextAnimator.Reset();;

                _maxLevelPointUpgradeCard -= _lastMaxLevelPointUpgradeCard;
                _lastMaxLevelPointUpgradeCard /= 1.1f;
            }

            if (_howMuchIncreaseLevel < 0) throw new System.InvalidOperationException();

            _increaseLevelPointText.text = $"+{_increaseLevelPoint}";

            if (_increaseLevelPoint == 0)
                _increaseLevelPointText.text = "";
        }
    }
}

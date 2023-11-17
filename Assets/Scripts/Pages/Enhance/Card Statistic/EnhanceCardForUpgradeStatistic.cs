using TMPro;
using UnityEngine;

namespace QuestPage.Enhance.Card_Statistic
{
    public class EnhanceCardForUpgradeStatistic : EnhanceCardStatistic
    {
        [SerializeField] private SliderAnimator _levelPointsSliderAnimator;
        [SerializeField] private PossibleLevelUpSlider _possibleLevelUpSlider;

        [SerializeField] private TMP_Text _maxLevelText;

        protected override void OnDisable()
        {
            base.OnDisable();

            _levelPointsSliderAnimator.UpdateSlider(0);
        }

        public void Render(ICardView cardForUpgrade)
        {
            Render(cardForUpgrade.CardData);

            _possibleLevelUpSlider.SetUpgradeCard(cardForUpgrade);

            _levelPointsSliderAnimator.UpdateSlider(cardForUpgrade.CardData.LevelPoint, cardForUpgrade.CardData.MaxLevelPoint);

            if (cardForUpgrade.CardData.Level == cardForUpgrade.CardData.MaxLevel)
            {
                _levelPointsSliderAnimator.UpdateSlider(cardForUpgrade.CardData.MaxLevelPoint, cardForUpgrade.CardData.MaxLevelPoint);
                _maxLevelText.text = "MAX";
            }
        }    
    }
}

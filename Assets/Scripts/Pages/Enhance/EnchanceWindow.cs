using System;
using System.Collections.Generic;
using QuestPage.Enhance.Card_Statistic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace QuestPage.Enhance
{
    public class EnchanceWindow : MonoBehaviour
    {
        [SerializeField] private EnchanceCardsForDeleteCollection _enhanceCardsForDeleteCollection;

        [SerializeField] private EnhanceCardForUpgradeStatistic _upgradeCardStatistic;

        [SerializeField] private Button _enhanceButton;
        [SerializeField] private GameObject _exeptionWindow;
        [SerializeField] private PossibleLevelUpSlider _possibleLevelUpSlider;

        public event Action OnCardEnchanced;

        private CardsCollection _cardsCollection;
        private ICardView _upgradeCard;

        public ICardView UpgradeCard => _upgradeCard;

        [Inject]
        public void Construct(CardsCollection cardsCollection)
        {
            _cardsCollection = cardsCollection;
        }

        private void OnEnable()
        {
            _enhanceButton.onClick.AddListener(Enhance);
        }

        private void OnDisable()
        {
            _enhanceButton.onClick.RemoveListener(Enhance);
            _cardsCollection.TakeCard(_upgradeCard as CardCellView);
        }

        public void Set(ICardView cardForUpgrade)
        {
            gameObject.SetActive(true);
            _upgradeCard = cardForUpgrade;

            _upgradeCardStatistic.Render(cardForUpgrade);
        }

        private void Enhance()
        {
            if (_enhanceCardsForDeleteCollection.CardsForDeleteCount == 0)
            {
                _exeptionWindow.SetActive(true);
                return;
            }

            _possibleLevelUpSlider.Reset();

            if (_upgradeCard.CardData == null) return;

            _upgradeCard.CardData.LevelUp(_enhanceCardsForDeleteCollection.GetCardsModel());
            _upgradeCardStatistic.Render(_upgradeCard);
            _cardsCollection.DeleteCards(_enhanceCardsForDeleteCollection.GetCardsView());

            OnCardEnchanced?.Invoke();
        }
    }
}

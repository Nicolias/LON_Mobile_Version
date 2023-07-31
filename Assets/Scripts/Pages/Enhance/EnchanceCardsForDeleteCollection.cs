using System;
using System.Collections.Generic;
using QuestPage.Enhance.Card_Statistic;
using UnityEngine;

namespace QuestPage.Enhance
{
    public class EnchanceCardsForDeleteCollection : CardsPage<ICardViewForDelete>
    {
        [SerializeField] private PossibleLevelUpSlider _possibleLevelUpSlider;
        [SerializeField] private EnchanceWindow _enchance;

        private List<ICardViewForDelete> _cardsForDelete = new();

        public int CardsForDeleteCount => _cardsForDelete.Count;

        protected override void OnEnable()
        {
            base.OnEnable();
            _enchance.OnCardEnchanced += ClearCardsForDelete;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _enchance.OnCardEnchanced -= ClearCardsForDelete;
        }

        public CardCell[] GetCardsModel()
        {
            CardCell[] cardsForDelete = new CardCell[_cardsForDelete.Count];

            for (int i = 0; i < cardsForDelete.Length; i++)
                cardsForDelete[i] = _cardsForDelete[i].CardData;

            return cardsForDelete;
        }

        public CardCellView[] GetCardsView()
        {
            CardCellView[] cardsForDelete = new CardCellView[_cardsForDelete.Count];

            for (int i = 0; i < cardsForDelete.Length; i++)
                cardsForDelete[i] = _cardsForDelete[i] as CardCellView;

            return cardsForDelete;
        }

        protected override void OnCardClicked(ICardView cardView)
        {
            ICardViewForDelete cardForDelete = cardView as ICardViewForDelete;

            if (cardForDelete == null)
                throw new InvalidCastException("Это карта не является картой для удаления.");


            if (cardForDelete.IsSelect)
            {
                cardForDelete.Unselect();
                _cardsForDelete.Remove(cardForDelete);
                _possibleLevelUpSlider.DecreasePossibleSliderLevelPoints(cardForDelete.CardData);
            }
            else if (_enchance.UpgradeCard.CardData.Level + _possibleLevelUpSlider.HowMuchIncreaseLevel 
                < _enchance.UpgradeCard.CardData.Statistic.MaxLevel)
            {
                cardForDelete.Select();
                _cardsForDelete.Add(cardForDelete);
                _possibleLevelUpSlider.IncreasePossibleSliderLevelPoints(cardForDelete.CardData);
            }
        }

        protected override void OnCardSelected(ICardView cardView)
        {
            throw new NotImplementedException();
        }

        protected override void RenderAllCards()
        {
            for (int i = 0; i < Cards.Count; i++)
                Cards[i].Render();
        }

        private void ClearCardsForDelete()
        {
            _cardsForDelete.Clear();
        }
    }
}

using System.Collections.Generic;
using QuestPage.Enhance.Card_Statistic;
using UnityEngine;

namespace QuestPage.Enhance
{
    public class EnchanceCardsForDeleteCollection : CardsPage<ICardViewInEnchance>
    {
        [SerializeField] private StatisticWindow _statisticWindow;
        [SerializeField] private EnchanceCardForDeleteCell _cardCellTemplate;
        [SerializeField] private Transform _container;
        [SerializeField] private PossibleLevelUpSlider possibleLevelUpSlider;
        [SerializeField] private Enchance _enchance;

        private List<CardCell> _cardsForDelete = new();
        public PossibleLevelUpSlider PossibleLevelUpSlider => possibleLevelUpSlider;
        public List<CardCell> CardForDelete => _cardsForDelete;

        public void AddToDeleteCollection(CardCell cardForDelete)
        {
            if (cardForDelete == null) throw new System.ArgumentNullException();

            _cardsForDelete.Add(cardForDelete);

            possibleLevelUpSlider.IncreasePossibleSliderLevelPoints(cardForDelete);
        }

        public void RetrieveCard(CardCell cardForDelete)
        {
            if (_cardsForDelete.Contains(cardForDelete) == false) throw new System.ArgumentOutOfRangeException();

            _cardsForDelete.Remove(cardForDelete);
            possibleLevelUpSlider.DecreasePossibleSliderLevelPoints(cardForDelete);
        }

        protected override void OnCardSelect(ICardView cardView)
        {
            throw new System.NotImplementedException();
        }

        protected override void RenderAllCards()
        {
            for (int i = 0; i < Cards.Count; i++)
                Cards[i].Render();
        }
    }
}

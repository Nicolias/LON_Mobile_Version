using System.Collections.Generic;
using QuestPage.Enhance.Card_Statistic;
using UnityEngine;

namespace QuestPage.Enhance
{
    public class EnchanceCardsForDeleteCollection : CardCollectionSort<CardViewCollection>
    {
        [SerializeField] private StatisticWindow _statisticWindow;
        [SerializeField] private EnchanceCardForDeleteCell _cardCellTemplate;
        [SerializeField] private Transform _container;
        [SerializeField] private PossibleLevelUpSlider possibleLevelUpSlider;
        [SerializeField] private Enchance _enchance;

        private List<CardCell> _cardsForDelete = new();
        public PossibleLevelUpSlider PossibleLevelUpSlider => possibleLevelUpSlider;
        public List<CardCell> CardForDelete => _cardsForDelete;

        private void OnEnable()
        {
            _cards.Clear();
        }

        private void OnDisable()
        {
            ClearCardForDeleteCollection();
        }

        public void DisplayCardsForDelete(List<CardCell> cardsForDelete)
        {
            gameObject.SetActive(true);

            ClearCardForDeleteCollection();

            if (cardsForDelete == null) throw new System.ArgumentNullException();

            RenderCards();

            void RenderCards()
            {
                for (int i = 0; i < cardsForDelete.Count; i++)
                {
                    var cell = Instantiate(_cardCellTemplate, _container);
                    cell.Init(this, _enchance, cardsForDelete[i]);
                    cell.Render(cardsForDelete[i]);
                    //cell.InitStatisticCard(_statisticWindow);
                    //_cards.Add(cell);
                }
            }
        }

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

        private void ClearCardForDeleteCollection()
        {
            foreach (Transform child in _container)
                Destroy(child.gameObject);

            _cards.Clear();
            _cardsForDelete.Clear();
        }
    }
}

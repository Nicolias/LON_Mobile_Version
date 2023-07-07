using System.Collections.Generic;
using UnityEngine;

namespace QuestPage.Enhance
{
    public class EnchanceCardCollection : MonoBehaviour
    {
        [SerializeField] private CardCollection _cardCollection;

        [SerializeField] private StatisticWindow _statisticWindow;

        [SerializeField] private Enchance _enchance;
        [SerializeField] private EnchanceCardsForDeleteCollection _enchanceCardsForDeleteCollection;

        [SerializeField] private EnchanceCardCell _cardCellTemplate;
        [SerializeField] private Transform _container;

        [HideInInspector] public EnchanceUpgradeCard UpgradeCard;

        private List<CardCell> _listCardsInCollection = new();
        
        private void OnEnable()
        {
            InitCardCollection();
            RenderCards();
        }

        public void SelectCard(CardCell cardCollectionCell)
        {
            _listCardsInCollection.Remove(cardCollectionCell);

            _enchance.SetCardForUpgrade(cardCollectionCell);
            _enchanceCardsForDeleteCollection.DisplayCardsForDelete(_listCardsInCollection);
        }

        private void InitCardCollection()
        {
            _listCardsInCollection.Clear();
            _listCardsInCollection.AddRange(_cardCollection.Cards);
        }

        private void RenderCards()
        {
            foreach (Transform card in _container)
            {
                Destroy(card.gameObject);
            }

            for (int i = 0; i < _listCardsInCollection.Count; i++)
            {
                EnchanceCardCell cell = Instantiate(_cardCellTemplate, _container);
                cell.Init(this, _listCardsInCollection[i]);
                //cell.Render(_listCardsInCollection[i]);
                //cell.InitStatisticCard(_statisticWindow);
            }
        }
    }
}

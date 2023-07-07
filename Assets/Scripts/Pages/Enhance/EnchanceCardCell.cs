using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace QuestPage.Enhance
{
    public class EnchanceCardCell : CardCellView
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _selfButton;

        private EnchanceCardCollection _enchanceCardCollection;
        private CardCell _cardInCollection;

        public override CardCell CardData => throw new System.NotImplementedException();

        private void OnEnable()
        {
            if(_selfButton != null)
                //_selfButton.onClick.AddListener(() => _statisticWindow.Render(this));
            _selectButton.onClick.AddListener(SelectCard);
        }

        private void OnDisable()
        {
            _selectButton.onClick.RemoveAllListeners();
        }

        public void Init(EnchanceCardCollection enchanceCardCollection, CardCell cardInCollection)
        {
            _enchanceCardCollection = enchanceCardCollection;
            _cardInCollection = cardInCollection;
        }

        protected virtual void SelectCard()
        {
            _enchanceCardCollection.SelectCard(_cardInCollection);
        }
    }
}

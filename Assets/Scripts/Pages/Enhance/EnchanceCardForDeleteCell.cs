using QuestPage.Enhance.Card_Statistic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestPage.Enhance
{
    [RequireComponent(typeof(Button))]
    public class EnchanceCardForDeleteCell : CardCellView
    {
        [SerializeField] private GameObject _selectImage;
        [SerializeField] private Button _button;
        
        private EnchanceCardsForDeleteCollection _enchanceCardForDeleteCollection;
        private CardCell _cardInCollection;
        private Enchance _enchance;

        private bool _isSelect;

        public CardCell CardData => throw new System.NotImplementedException();

        private void OnEnable()
        {
            _button.onClick.AddListener(SwitchSelectionState);
            _selectImage.SetActive(false);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SwitchSelectionState);
        }

        public void Init(EnchanceCardsForDeleteCollection enchanceCardsForDeleteCollection, Enchance enchance, CardCell cardInCollection)
        {
            _enchanceCardForDeleteCollection = enchanceCardsForDeleteCollection;
            _enchance = enchance;
            _cardInCollection = cardInCollection;
        }

        private void SwitchSelectionState()
        {
            if (_isSelect)
            {
                _enchanceCardForDeleteCollection.RetrieveCard(_cardInCollection);
            }
            else
            {
                if (_enchance.UpgradeCard.CardCell.Level + _enchanceCardForDeleteCollection.PossibleLevelUpSlider.HowMuchIncreaseLevel < 25)
                    _enchanceCardForDeleteCollection.AddToDeleteCollection(_cardInCollection);
                else
                    return;
            }

            _isSelect = !_isSelect;
            _selectImage.SetActive(_isSelect);
        }
    }
}

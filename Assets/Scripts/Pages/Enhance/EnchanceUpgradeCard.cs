using QuestPage.Enhance.Card_Statistic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace QuestPage.Enhance
{
    public class EnchanceUpgradeCard : MonoBehaviour
    {    
        [SerializeField] private EnchanceCardsCollection _enhanceCardCollection;
        [SerializeField] private Enchance _enhance;

        [SerializeField] private EnhanceCardForUpgradeStatistic _cardStatistic;

        private CardCell _cardCell;
        public CardCell CardCell => _cardCell;

        public void Set(CardCell card)
        {
            if (card == null) throw new System.ArgumentNullException();

            _cardCell = card;
            _cardStatistic.Render(this);
        }


        private void OpenCardCollection()
        {
            _enhanceCardCollection.UpgradeCard = this;
        }
    }
}

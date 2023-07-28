using System.Collections.Generic;
using UnityEngine;

namespace QuestPage.Enhance
{
    public class EnchanceCardsCollection : CardsPage<ICardViewInEnchance>
    {
        [SerializeField] private Enchance _enchance;
        [SerializeField] private EnchanceCardsForDeleteCollection _enchanceCardsForDeleteCollection;

        public EnchanceUpgradeCard UpgradeCard { get; set; }

        [SerializeField] private CardsPage<ICardViewInEnchance> _cardsCollectionView;

        protected override void OnCardSelect(ICardView cardView)
        {
            _enchance.SetCardForUpgrade(cardView.CardData);
        }

        protected override void RenderAllCards()
        {
            for (int i = 0; i < Cards.Count; i++)
                Cards[i].Render();
        }
    }
}

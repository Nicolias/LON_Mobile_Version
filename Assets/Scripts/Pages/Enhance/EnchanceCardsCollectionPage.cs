using UnityEngine;

namespace QuestPage.Enhance
{
    public class EnchanceCardsCollectionPage : CardsPage<ICardViewInEnchance>
    {
        [SerializeField] private EnchanceWindow _enchance;

        protected override void OnCardClicked(ICardView cardView)
        {
            StatisticWindow.Render(cardView as ICardViewInEnchance);
        }

        protected override void OnCardSelected(ICardView cardView)
        {
            CardsCollection.GiveCard(cardView as CardCellView);
            _enchance.Set(cardView);
        }

        protected override void RenderAllCards()
        {
            for (int i = 0; i < Cards.Count; i++)
                Cards[i].Render();
        }
    }
}

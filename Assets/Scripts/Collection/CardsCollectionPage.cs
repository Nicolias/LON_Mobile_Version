using System.Collections.Generic;

public class CardsCollectionPage : CardsPage<ICardViewInCollection>
{
    protected override void OnCardSelect(ICardView cardView)
    {
        StatisticWindow.Render(cardView as ICardViewInCollection);
    }

    protected override void RenderAllCards()
    {
        foreach (ICardViewInCollection card in Cards)
            card.Render();
    }
}
using System.Collections.Generic;

public class CardsCollectionPage : CardsPage<ICardViewInCollection>
{
    protected override void OnCardClicked(ICardView cardView)
    {
        StatisticWindow.Render(cardView as ICardViewInCollection);
    }

    protected override void OnCardSelected(ICardView cardView)
    {
        throw new System.NotImplementedException();
    }

    protected override void RenderAllCards()
    {
        foreach (ICardViewInCollection card in Cards)
            card.Render();
    }
}
public class CardsCollectionInEvolvePage : EvolveCardsCollection<ICardViewForEvolve>
{
    protected override void OnCardClicked(ICardView cardView)
    {
        StatisticWindow.Render(cardView as ICardViewForEvolve);
    }

    protected override void OnCardSelected(ICardView cardView)
    {
        base.OnCardSelected(cardView);

        EvolutionWindow.Open();

        foreach (ICardView card in Cards)
            UnsubscribeFrom(card);
    }

    protected override void RenderAllCards()
    {
        foreach (ICardViewForEvolve card in Cards)
                card.Render();
    }
}

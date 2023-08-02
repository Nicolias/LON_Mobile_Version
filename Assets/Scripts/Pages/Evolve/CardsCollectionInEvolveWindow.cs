public class CardsCollectionInEvolveWindow : EvolveCardsCollection<ICardViewForEvolve>
{
    protected override void OnCardSelected(ICardView cardView)
    { 
        if (CardForEvolution.CardView != null)
            CardsCollection.TakeCard(CardForEvolution.CardView as CardCellView);

        base.OnCardSelected(cardView);

        EvolutionWindow.EnableEvolveButton();
    }

    protected override void RenderAllCards()
    {
        if (EvolutionWindow.FirstCard.CardView == null)
            throw new System.InvalidOperationException("Первая карта еще не проинициализированна.");

        if (EvolutionWindow.FirstCard.CardView.Statistic.Evolution == EvolutionWindow.FirstCard.CardView.Statistic.MaxEvolution)
            return;

        foreach (ICardViewForEvolve card in Cards)
            if (card.Statistic.UiIcon.name == EvolutionWindow.FirstCard.CardView.Statistic.UiIcon.name)
                card.Render();
    }
}
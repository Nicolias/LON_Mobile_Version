using System.Collections.Generic;
using UnityEngine;

public abstract class EvolveCardsCollection<T> : CardsPage<T> where T : class, ICardView
{
    [SerializeField] private Evolution _evolutionWindow;
    [SerializeField] private EvolutionCard _cardForEvolution;

    public EvolutionCard CardForEvolution => _cardForEvolution;
    protected Evolution EvolutionWindow => _evolutionWindow;

    protected override void OnCardClicked(ICardView cardView)
    {
        StatisticWindow.Render(cardView as ICardViewForEvolve);
    }

    protected override void OnCardSelected(ICardView cardView)
    {
        CardForEvolution.Set(cardView as ICardViewForEvolve);
        CardsCollection.GiveCard(cardView as CardCellView);
    }
}

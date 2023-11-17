public interface ICardViewInEnchance : ICardView
{
    public void Render();
}

public interface ICardViewForEvolve : ICardView
{
    public void Evolve(ICardViewForEvolve cardForEvolve);
    public void Render();
}
public interface ICardViewForDelete : ICardView
{
    public bool IsSelect { get; }

    public void Render();
    public void Select();
    public void Unselect();

}
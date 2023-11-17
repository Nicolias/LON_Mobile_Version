using UnityEngine;

public class EmptyCardCell : CardCell
{
    [SerializeField] private Card _emptyCard;

    public EmptyCardCell() : base(null)
    {
    }
}

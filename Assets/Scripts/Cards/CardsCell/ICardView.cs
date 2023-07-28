using System;
using UnityEngine;

public interface ICardView
{
    public event Action<ICardView> OnSelected;

    public CardCell CardData { get; }

    public CardStatistic Statistic { get; }
    public Card Card => null;

    public Sprite UIIcon { get; }

    public Transform Transform { get; }
}

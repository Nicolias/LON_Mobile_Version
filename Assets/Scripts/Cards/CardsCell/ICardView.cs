using System;
using UnityEngine;

public interface ICardView
{
    public event Action<ICardView> OnSelfButtonClicked;
    public event Action<ICardView> OnSelectButtonClicked;

    public CardCell CardData { get; }

    public CardStatistic Statistic { get; }
    public Card Card => null;

    public Sprite UIIcon { get; }

    public Transform Transform { get; }
}

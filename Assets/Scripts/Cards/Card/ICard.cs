using UnityEngine;

public interface ICard
{  
    public Card Card { get; }
    public Sprite UIIcon { get; }

    public CardStatistic Statistic { get; }
}

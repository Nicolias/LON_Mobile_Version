using System;
using UnityEngine;

public class CardCell 
{
    private Card _card;
    private CardStatistic _statistic;

    private CardEnchencer _cardEnchencer = new(150);

    public Card Card => _card;
    public CardStatistic Statistic => _statistic;

    public int LevelPoint => _cardEnchencer.LevelPoint;
    public int MaxLevelPoint => _cardEnchencer.MaxLevelPoint;
    public float NextMaxLevelPoitnMultiplier => _cardEnchencer.NextMaxLevelPoitnMultiplier;
    public int Level => _statistic.Level;
    public int MaxLevel => _statistic.MaxLevel;

    public CardCell(CardStatistic cardStatistic)
    {
        _statistic = cardStatistic;
    }

    public void LevelUp(CardCell[] cardsForEnhance)
    {
        _cardEnchencer.LevelUp(cardsForEnhance, _statistic);
    }

    public float GetDamageValueAfterResist(float amountDamage)
    {
        System.Random random = new();

        amountDamage -= (float)random.NextDouble() * (_statistic.Defence - _statistic.Defence / 2) + _statistic.Defence / 2;

        if (amountDamage < 0) amountDamage = 0;

        return amountDamage;
    }

    public int GetCardDeletePoint()
    {
        float RacialMultiplier(RarityCard race)
        {
            float multiplier = 1;

            for (int i = 1; i < (int)race; i++)
            {
                multiplier += 0.5f;
            }

            return multiplier;
        }

        return (int)(_statistic.BaseEnhancmentLevelPoint * RacialMultiplier(_statistic.Rarity) + _cardEnchencer.AmountIncreaseLevelPoint * 0.75f);
    }

    public void Evolve(EvolutionCard firstCard, EvolutionCard secondCard)
    {
        _statistic.EvolveCard(firstCard, secondCard);

        _card = firstCard.CardCell.Card;

        _cardEnchencer = new(1000);
    }
}

public class CardEnchencer
{
    public event Action OnLevelUp;

    private int _levelPoint;
    private int _maxLevelPoint;

    private const float _nextMaxLevelPointMultiplier = 1.1f;

    public int LevelPoint => _levelPoint;
    public int MaxLevelPoint => _maxLevelPoint;
    public float NextMaxLevelPoitnMultiplier => _nextMaxLevelPointMultiplier;

    public int AmountIncreaseLevelPoint { get; private set; }

    public CardEnchencer(int maxLevelPoint)
    {
        _maxLevelPoint = maxLevelPoint;
    }

    public void LevelUp(CardCell[] cardsForEnhance, CardStatistic statistic)
    {
        foreach (var card in cardsForEnhance)
        {
            _levelPoint += card.GetCardDeletePoint();
            AmountIncreaseLevelPoint += card.GetCardDeletePoint();
        }

        while (LevelPoint >= MaxLevelPoint && statistic.Level < statistic.MaxLevel)
        {
            _levelPoint -= MaxLevelPoint;
            _maxLevelPoint = (int)(MaxLevelPoint * _nextMaxLevelPointMultiplier);

            statistic.LevelUpCardValue();
            OnLevelUp?.Invoke();
        }
    }
}

public class CardEvolver
{
    public const float ValueIncreaseMultiplier = 1.35f;

    public int GetEvolveUpValue(int firstValue, int secondValue)
    {
        var average = (firstValue + secondValue) / 2;
        var evolveUpValue = average * ValueIncreaseMultiplier;
        return (int)evolveUpValue;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class CardStatistic
{
    public event Action OnLevelUp;

    private int _health;
    private int _attack;
    private int _def;
    private int _level;
    private int _evolution;

    private List<Sprite> _evolutionsSprite;

    public int BaseEnhancmentLevelPoint => 1500;
    private const int _maxLevel = 25;

    
    public const float ValueLevelUpIncreaseMultiplier = 1.15f;

    public int Attack => _attack;
    public int Defence => _def;
    public int Health => _health;

    public int Power => Attack + Health;

    public int Level => _level;
    public int Evolution => _evolution;
    public int MaxLevel => _maxLevel;

    public int BonusAttackSkill => (int)(Attack * 0.17f);
    public int Id { get; set; }

    public RarityCard Rarity { get; private set; }

    public Sprite UiIcon
    {
        get 
        {
            return _evolutionsSprite[_evolution - 1];
        }
    }

    public Sprite SkillIcon { get; private set; }

    public CardStatistic(Card cardData)
    {
        _attack = cardData.Attack;
        _def = cardData.Def;
        _health = cardData.Health;
        _level = cardData.Level;
        _evolution = cardData.Evolution;

        _evolutionsSprite = cardData.EvolutionsSprite;
        Rarity = cardData.Rarity;
        SkillIcon = cardData.SkillIcon;
    }

    public void Render(ICard card)
    {
        _attack = card.Statistic.Attack;
        _def = card.Statistic.Defence;
        _health = card.Statistic.Health;
        _level = card.Statistic.Level;
        _evolution = card.Statistic.Evolution;
    }

    public void LevelUpCardValue()
    {
        _attack = (int)(Attack * ValueLevelUpIncreaseMultiplier);
        _def = (int)(Defence * ValueLevelUpIncreaseMultiplier);
        _health = (int)(Health * ValueLevelUpIncreaseMultiplier);

        _level++;
    }

    public void EvolveCard(EvolutionCard firstCard, EvolutionCard secondCard)
    {
        CardEvolver cardEvolver = new();

        _attack = cardEvolver.GetEvolveUpValue(firstCard.CardCell.Statistic.Attack, secondCard.CardCell.Statistic.Attack);
        _def = cardEvolver.GetEvolveUpValue(firstCard.CardCell.Statistic.Defence, secondCard.CardCell.Statistic.Defence);
        _health = cardEvolver.GetEvolveUpValue(firstCard.CardCell.Statistic.Health, secondCard.CardCell.Statistic.Health);
        Id = firstCard.CardCell.Statistic.Id;
        _evolution = 2;
        _level = 1;
    }
}
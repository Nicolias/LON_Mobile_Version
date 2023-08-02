using System;
using System.Collections.Generic;
using UnityEngine;

public class CardStatistic
{
    public event Action LevelUped;

    private int _health;
    private int _attack;
    private int _def;
    private int _level;
    private int _evolution;

    private List<Sprite> _evolutionsSprite;

    public const float ValueLevelUpIncreaseMultiplier = 1.15f;
    
    private int _maxLevel = 25;

    public int BaseEnhancmentLevelPoint => 1500;
    public int Attack => _attack;
    public int Defence => _def;
    public int Health => _health;

    public int Power => Attack + Health;

    public int Level => _level;
    public int Evolution => _evolution;
    public int MaxLevel => _maxLevel;
    public int MaxEvolution => _evolutionsSprite.Count;

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
        _evolution = card.Statistic.MaxEvolution;
    }

    public void LevelUpCardValue()
    {
        _attack = (int)(Attack * ValueLevelUpIncreaseMultiplier);
        _def = (int)(Defence * ValueLevelUpIncreaseMultiplier);
        _health = (int)(Health * ValueLevelUpIncreaseMultiplier);

        _level++;
    }

    public void EvolveCard(ICardViewForEvolve firstCard, ICardViewForEvolve secondCard)
    {
        CardEvolver cardEvolver = new();

        _attack = cardEvolver.GetEvolveUpValue(firstCard.Statistic.Attack, secondCard.Statistic.Attack);
        _def = cardEvolver.GetEvolveUpValue(firstCard.Statistic.Defence, secondCard.Statistic.Defence);
        _health = cardEvolver.GetEvolveUpValue(firstCard.Statistic.Health, secondCard.Statistic.Health);
        Id = firstCard.Statistic.Id;
        _evolution++;
        _level = 1;
    }
}
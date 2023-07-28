using System.Collections.Generic;
using UnityEngine;

public enum RarityCard
{
    Empty,
    Standart,
    Rare,
    HightRare,
    Epic,
    Legendary
}

public enum RaceCard
{
    Humans,
    Gods, 
    Demons
}

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card")]
public class Card : ScriptableObject, IPrize
{
    [SerializeField] private List<Sprite> _evolutionsSprite;

    [SerializeField] private string _name;
    [SerializeField] private RarityCard _rarity;
    [SerializeField] private RaceCard _race;
    [SerializeField] private int _attack;
    [SerializeField] private int _def;
    [SerializeField] private int _health;
    [SerializeField] private string _effectName;
    [SerializeField] private string _attackSillName;
    [SerializeField] private string _defSkillName;
    [SerializeField] private int _defSkill;

    [Header("Skill Chance Per Procent")]
    [SerializeField] private double _skillChance;
    [SerializeField] private string _discription;
    [SerializeField] private Vector2 _directionView;

    [SerializeField] 
    private ParticleSystem _attackEffect;

    [SerializeField] 
    private ParticleSystem _skillEffect;
    
    [SerializeField] 
    private Sprite _skillIcon;

    private int _level = 1;
    private int _currentLevelPoint;
    private int _maxLevelPoint = 1000;
    private int _evolution = 1;
    
    public int Evolution => _evolution;
    public int LevelPoint => _currentLevelPoint;
    public int MaxLevelPoint => _maxLevelPoint;
    public string Name => _name;
    public RarityCard Rarity => _rarity;
    public RaceCard Race => _race;
    public int Attack => _attack;
    public int Def => _def;
    public int Health => _health;
    public int Level => _level;
    public ParticleSystem AttackEffect => _attackEffect;
    public ParticleSystem SkillEffect => _skillEffect;
    public Sprite SkillIcon => _skillIcon;
    public int BonusAttackSkill => (int)(_attack * 0.17f);
    public string AttackSkillName => _attackSillName;
    public string EffectName => _effectName;
    public float SkillChance => (float)_skillChance;
    public string Discription => _discription;

    public Sprite UIIcon => _evolutionsSprite[0];

    public List<Sprite> EvolutionsSprite => _evolutionsSprite;

    public void TakeItemAsPrize(IIncreaserWalletValueAndCardsCount increaser, int amountValue)
    {
        increaser.CardCollection.AddCard(this);
    }
}
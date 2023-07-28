using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardStatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _attackText;

    [SerializeField] private TextMeshProUGUI _defenseText;

    [SerializeField] private TextMeshProUGUI _healthText;

    [SerializeField] private Image _skillImage;

    private int _health;
    private int _defence;

    public int Defence => _defence;
    public int Health => _health;
    public int DamageAfterRessist { get; private set; }
    public TMP_Text HealthText => _healthText;

    public void Initialize(CardCell card)
    {
        _health = card.Statistic.Health;
        _defence = card.Statistic.Defence;
        _attackText.text = card.Statistic.Attack.ToString();
        _defenseText.text = _defence.ToString();
        _healthText.text = _health.ToString();
        _skillImage.sprite = card.Statistic.SkillIcon;
    }

    public void DecreaseHealth(int damage)
    {
        _health -= GetDamageValueAfterResist(damage);

        if (_health <= 0) _health = 0; ;
        _healthText.text = _health.ToString();
    }

    private int GetDamageValueAfterResist(float amountDamage)
    {
        amountDamage -= Random.Range(_defence / 2, _defence);

        if (amountDamage < 0) amountDamage = 0;

        DamageAfterRessist = (int)amountDamage;

        return DamageAfterRessist;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class EvolutionCard : MonoBehaviour
{
    [SerializeField] private Evolution _evolution;

    [SerializeField] private Image _UIIcon, _emptyImage;

    public bool IsSet => _isSet;
    private bool _isSet = false;

    public CardCell CardCell { get; private set; }

    public void SetCard(CardCell selectCard)
    {
        CardCell = selectCard;
        _UIIcon.sprite = CardCell.Statistic.UiIcon;
        _UIIcon.gameObject.SetActive(true);
        _isSet = true;
        _emptyImage.gameObject.SetActive(false);
    }

    public void Reset()
    {
        CardCell = null;
        _UIIcon.gameObject.SetActive(false);
        _emptyImage.gameObject.SetActive(true);
        _isSet = false;
    }
}

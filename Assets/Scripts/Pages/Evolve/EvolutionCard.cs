using UnityEngine;
using UnityEngine.UI;

public class EvolutionCard : MonoBehaviour
{
    [SerializeField] private Image _UIIcon, _emptyImage;

    private bool _isSet = false;
    public bool IsSet => _isSet;

    public ICardViewForEvolve CardView { get; private set; }

    public void Set(ICardViewForEvolve selectCard)
    {
        CardView = selectCard;
        _isSet = true;
        _UIIcon.sprite = CardView.Statistic.UiIcon;

        RenderAvatar();
    }

    public void Reset()
    {
        CardView = null;
        _isSet = false;
    }

    public void RenderAvatar()
    {
        _UIIcon.gameObject.SetActive(IsSet);
        _emptyImage.gameObject.SetActive(IsSet == false);
    }
}

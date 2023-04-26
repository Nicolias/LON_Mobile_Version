using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FarmPage.Enhance;

public class StatisticWindow : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _atk, _def, _name, _health;
    [SerializeField] private DeckWindow _deckWindow;
    [SerializeField] private Button _setOrUnSetButton;
    [SerializeField] private TMP_Text _setOrUnsetButtonText;
    [SerializeField] private Sprite _setOrUnsetButtonSprite;
    [SerializeField] private Sprite _disarmButtonSprite;

    private Image _setOrUnsetButtonImage;

    private void Awake()
    {
        _setOrUnsetButtonImage = _setOrUnSetButton.image;
    }

    public void Render(CardCellInDeck card)
    {
        _setOrUnSetButton.gameObject.SetActive(true);
        _setOrUnSetButton.onClick.RemoveAllListeners();

        Render((CardCell)card);

        _setOrUnsetButtonImage.sprite = _disarmButtonSprite;
        _setOrUnsetButtonText.text = "remove";

        _setOrUnSetButton.onClick.AddListener(() =>
        {
            _deckWindow.CurrentDeck.UnsetCardInCollection(card, card.transform.GetSiblingIndex());
            gameObject.SetActive(false);
        });
    }

    public void Render(CardCollectionCell card)
    {
        _setOrUnSetButton.gameObject.SetActive(true);
        _setOrUnSetButton.onClick.RemoveAllListeners();

        Render((CardCell)card);

        _setOrUnsetButtonImage.sprite = _setOrUnsetButtonSprite;
        _setOrUnsetButtonText.text = "add";

        _setOrUnSetButton.onClick.AddListener(() =>
        {
            _deckWindow.CurrentDeck.SetCardInDeck(card);
            gameObject.SetActive(false);
        });
    }

    public void Render(EnchanceCardCell enchanceCardCell)
    {
        Render((CardCell)enchanceCardCell);
        _setOrUnSetButton.gameObject.SetActive(false);
    }

    private void Render(CardCell cardCell)
    {
        gameObject.SetActive(true);

        _icon.sprite = cardCell.UIIcon;

        _atk.text =  cardCell.Attack.ToString();
        _def.text =  cardCell.Def.ToString();
        _health.text = cardCell.Health.ToString();
    }
}

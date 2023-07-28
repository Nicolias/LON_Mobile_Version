using TMPro;
using UnityEngine;
using UnityEngine.UI;
using QuestPage.Enhance;
using System;

public class StatisticWindow : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _atk, _def, _name, _health;
    [SerializeField] private Button _setOrUnSetButton;
    [SerializeField] private TMP_Text _setOrUnsetButtonText;
    [SerializeField] private Sprite _setOrUnsetButtonSprite;
    [SerializeField] private Sprite _disarmButtonSprite;

    [SerializeField] private CardsPage<ICardViewInCollection> _cardCollectionView;
    [SerializeField] private DeckWindow _deckWindow;

    private Image _setOrUnsetButtonImage;

    private void Awake()
    {
        _setOrUnsetButtonImage = _setOrUnSetButton.image;
    }

    public void Render(ICardViewInCollection card)
    {
        RenderSetOrUnsetButton(card.CardData, "add",
            () =>
            {
                _deckWindow.CurrentDeck.SetCard(card.CardData);
                //_cardCollectionView.DeleteCardsView(card.CardData);                
            });
    }

    public void Render(DeckSlot deckSlot)
    {
        RenderSetOrUnsetButton(deckSlot.CardData, "remove", 
            () =>
            {
                //_cardCollectionView.CreateCardView(deckSlot.CardData);                
                _deckWindow.CurrentDeck.Reset(deckSlot);
            });
    }

    public void Render(ICardViewInEnchance enchanceCardCell)
    {
        Render(enchanceCardCell.CardData);
        _setOrUnSetButton.gameObject.SetActive(false);
    }

    private void RenderSetOrUnsetButton(CardCell cardData, string textOnButton, Action actionOnButton)
    {
        _setOrUnSetButton.gameObject.SetActive(true);
        _setOrUnSetButton.onClick.RemoveAllListeners();

        Render(cardData);

        _setOrUnsetButtonImage.sprite = _disarmButtonSprite;
        _setOrUnsetButtonText.text = textOnButton;

        _setOrUnSetButton.onClick.AddListener(() =>
        {
            actionOnButton?.Invoke();   
            gameObject.SetActive(false);
        });
    }

    private void Render(CardCell cardCell)
    {
        gameObject.SetActive(true);

        _icon.sprite = cardCell.Statistic.UiIcon;

        _atk.text =  cardCell.Statistic.Attack.ToString();
        _def.text =  cardCell.Statistic.Defence.ToString();
        _health.text = cardCell.Statistic.Health.ToString();
    }
}

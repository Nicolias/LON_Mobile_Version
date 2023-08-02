using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Evolution : MonoBehaviour
{
    public event Action OnEvolvedCard;

    [SerializeField] private CardsCollectionInEvolveWindow _evolveCardsCollectionInEvolveWindow;
    [SerializeField] private CardsCollectionInEvolvePage _evolveCardsCollectionInCollectionPage;

    [SerializeField] private Button _evolveButton;
    [SerializeField] private Button _backButton;

    private CardsCollection _cardsCollection;

    public EvolutionCard FirstCard => _evolveCardsCollectionInCollectionPage.CardForEvolution;
    public EvolutionCard SecondeCard => _evolveCardsCollectionInEvolveWindow.CardForEvolution;

    public Sprite EvolvedCardSprite { get; private set; }

    [Inject]
    public void Construct(CardsCollection cardsCollection)
    {
        _cardsCollection = cardsCollection;
    }

    private void OnEnable()
    {
        FirstCard.RenderAvatar();
        SecondeCard.RenderAvatar();

        _backButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        _evolveButton.onClick.AddListener(EvolveCard);

        _evolveButton.interactable = false;
    }

    private void OnDisable()
    {
        _cardsCollection.TakeCard(FirstCard.CardView as CardCellView);

        if (SecondeCard.CardView != null)
            _cardsCollection.TakeCard(SecondeCard.CardView as CardCellView);

        FirstCard.Reset();
        SecondeCard.Reset();

        _backButton.onClick.RemoveAllListeners();
        _evolveButton.onClick.RemoveListener(EvolveCard);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void EnableEvolveButton()
    {
        _evolveButton.interactable = true;
    }

    private void EvolveCard()
    {
        if (FirstCard.IsSet == false || SecondeCard.IsSet == false)
            throw new InvalidOperationException();

        FirstCard.CardView.Evolve(SecondeCard.CardView);

        EvolvedCardSprite = FirstCard.CardView.Statistic.UiIcon;

        _cardsCollection.DeleteCards(new CardCellView[] { SecondeCard.CardView as CardCellView });
        SecondeCard.Reset();

        OnEvolvedCard?.Invoke();
        _evolveButton.interactable = false;
    }
}
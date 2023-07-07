using System.Collections.Generic;
using UnityEngine;

public class CardInventory : CardCollectionSort<InventoryCardCell>
{
    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private AttackDeck _attackDeck;
    [SerializeField] private Transform _container;
    [SerializeField] private InventoryCardCell _cardCellTemplayte;

    [SerializeField] private InventoryCardStatistic _cardStatistic;

    private void OnEnable()
    {
        _cards.Clear();

        foreach (Transform cell in _container)
        {
            Destroy(cell.gameObject);
        }

        Render(_cardCollection.Cards);
        Render(_attackDeck.Slots);
    }

    private void Render<K>(List<K> cardCells) where K : ICard
    {
        foreach (var card in cardCells)
        {
            if (card.Card != null && card.Card.Rarity != RarityCard.Empty)
            {
                var cell = Instantiate(_cardCellTemplayte, _container);
                cell.Render(card);
                _cards.Add(cell);
            }
        }
    }
}

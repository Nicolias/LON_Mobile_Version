using UnityEngine;

public class DeckWindow : MonoBehaviour
{
    [SerializeField] private AttackDeck _attackDeck;
    [SerializeField] private DefenceDeck _defenceDeck;

    public Deck CurrentDeck { get; private set; }

    private void OnEnable()
    {
        _attackDeck.OnDeckActiveChanged += SwitchCurrentActiveDeck;
        _defenceDeck.OnDeckActiveChanged += SwitchCurrentActiveDeck;

        SwitchCurrentActiveDeck();
    }

    private void OnDisable()
    {
        _attackDeck.OnDeckActiveChanged -= SwitchCurrentActiveDeck;
        _defenceDeck.OnDeckActiveChanged -= SwitchCurrentActiveDeck;
    }

    private void SwitchCurrentActiveDeck()
    {
        CurrentDeck = _attackDeck.gameObject.activeInHierarchy == true ? _attackDeck : _defenceDeck;
    }
}
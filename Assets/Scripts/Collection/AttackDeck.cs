
public class AttackDeck : Deck
{
    public int Power
    {
        get
        {
            int amountPower = 0;

            foreach (DeckSlot deckSlot in _deckSlots)
                amountPower += deckSlot.CardData.Statistic.Power;

            return amountPower;
        }
    }
}
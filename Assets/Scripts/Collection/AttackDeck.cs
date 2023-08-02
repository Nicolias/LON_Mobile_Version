
public class AttackDeck : Deck
{
    public int Power
    {
        get
        {
            int amountPower = 0;

            foreach (DeckSlot deckSlot in _deckSlots)
                amountPower += deckSlot.CardView.Statistic.Power;

            return amountPower;
        }
    }
}
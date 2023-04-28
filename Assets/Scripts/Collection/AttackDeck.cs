
public class AttackDeck : Deck
{
    public int Power
    {
        get
        {
            int amountPower = 0;

            foreach (var card in _deckSlot)
                amountPower += card.Power;

            return amountPower;
        }
    }
}
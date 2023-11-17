using Data;

public interface IIncreaserWalletValueAndCardsCount
{
    public CardsCollection CardCollection { get; }
    public CristalWallet CristalWallet { get; }
    public GoldWallet GoldWallet { get; }
    public Inventory Inventory { get; }   
}
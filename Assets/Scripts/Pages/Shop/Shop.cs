using Data;
using QuestPage.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : MonoBehaviour, IIncreaserWalletValueAndCardsCount
{
    [SerializeField] private ShopCategoryRendering _startCategory;

    [SerializeField] private Inventory _inventory;
    [SerializeField] private CristalWallet _cristalWallet;
    [SerializeField] private CardCollection _cardCollection;
    private ConfirmWindow _confirmWindow;

    public CardCollection CardCollection => _cardCollection;
    public Inventory Inventory => _inventory;
    public GoldWallet GoldWallet => throw new System.NotImplementedException();
    public CristalWallet CristalWallet => _cristalWallet;
    public ConfirmWindow ConfirmWindow => _confirmWindow;

    private void Start()
    {
        _startCategory.GetComponent<Button>().onClick.Invoke();
    }

    public void BuyItem(IShopItem shopItem, ConfirmWindow confirmWindow)
    {
        _confirmWindow = confirmWindow;

        shopItem.Buy(this);
    }
}
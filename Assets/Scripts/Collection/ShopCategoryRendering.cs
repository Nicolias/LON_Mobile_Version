using System.Collections.Generic;
using QuestPage.Shop;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CategorySelection))]
public class ShopCategoryRendering : MonoBehaviour
{
    [SerializeField] private List<ShopItem> _shopItems;
    [SerializeField] private ShopItemCell _shopItemCellTemplate;
    [SerializeField] private Transform _container;
    
    [SerializeField] private ConfirmWindow _confirmWindow;

    [SerializeField] private Button _button;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(SelectCategore);
        _confirmWindow.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void SelectCategore()
    {
        Render();
    }

    private void Render()
    {
        foreach (Transform childs in _container)
            Destroy(childs.gameObject);


        _shopItems.ForEach(item =>
        {
            var cell = Instantiate(_shopItemCellTemplate, _container);
            cell.Init(_confirmWindow);
            cell.Render(item as IShopItem);
        });

        _confirmWindow.gameObject.SetActive(false);
    }
}

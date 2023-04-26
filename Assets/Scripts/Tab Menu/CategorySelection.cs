using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CategorySelection : MonoBehaviour
{
    [SerializeField] private Image _frameImage;
    [SerializeField] private TMP_Text _categoryName;
    [SerializeField] private Color _normalColor, _selectColor;

    [SerializeField] private TabMenu _tabMenu;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => _tabMenu.SelectCategory(this));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void SelectCategore()
    {
        _categoryName.color = _selectColor;
        _frameImage.gameObject.SetActive(true);
    }

    public void UnselectCategory()
    {
        _categoryName.color = _normalColor;
        _frameImage.gameObject.SetActive(false);
    }
}

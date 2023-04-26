using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    [SerializeField] private List<CategorySelection> _categoryButton;

    public void SelectCategory(CategorySelection selectCategoryButton)
    {
        foreach (var categoryButton in _categoryButton)
        {
            categoryButton.UnselectCategory();
        }

        selectCategoryButton.SelectCategore();
    }
}
using TMPro;
using UnityEngine;

public class ExceptionServise : MonoBehaviour
{
    [SerializeField] private TMP_Text _exceptionText;

    public void PrintException(string exceptionText)
    {
        gameObject.SetActive(true);
        _exceptionText.text = exceptionText;
    }
}

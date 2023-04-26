using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _loadProcent;

    [SerializeField] private Sprite[] _cardsImage;
    [SerializeField] private Image _cardImage;

    private AsyncOperation _asyncLoadScene;

    private void OnEnable()
    {       
        StartCoroutine(LoadScene());

        StartCoroutine(ChangeCardsImage());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator LoadScene()
    {
        _slider.value = 0;
        _slider.maxValue = 100;

        while (_slider.value != _slider.maxValue)
        {
            _slider.value++;
            _loadProcent.text = _slider.value + " %";

            yield return new WaitForSeconds(0.1f);
        }

        _asyncLoadScene.allowSceneActivation = true;
    }

    private IEnumerator ChangeCardsImage()
    {

        _asyncLoadScene = SceneManager.LoadSceneAsync("BattleScene");
        _asyncLoadScene.allowSceneActivation = false;

        while (_slider.value != 0)
        {
            _cardImage.sprite = _cardsImage[Random.Range(0, _cardsImage.Length)];

            yield return new WaitForSeconds(3f);

        }
    }
}

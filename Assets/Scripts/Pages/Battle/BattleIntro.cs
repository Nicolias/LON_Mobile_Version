using System.Collections;
using TMPro;
using UnityEngine;

namespace Battle
{
    public class BattleIntro : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _turnText;
        [SerializeField] private float _showTime;

        public IEnumerator PlayButtleIntro()
        {
            yield return ShowIntroText("Battle Start");
        }

        public IEnumerator PlayRoundIntro(int roundNumber)
        {
            yield return ShowIntroText($"Round {roundNumber}");
        }

        private IEnumerator ShowIntroText(string text)
        {
            gameObject.SetActive(true);
            _turnText.text = text;
            yield return new WaitForSeconds(_showTime);
            gameObject.SetActive(false);
        }
    }
}
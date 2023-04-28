using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FarmPage.Battle
{
    public class BattleConfirmWindow : MonoBehaviour
    {
        [SerializeField] private Battel _battle;

        [SerializeField] private AttackDeck _attackDeck;
        [SerializeField] private Energy _energy;

        [SerializeField] private int _battleEnergyPrice;

        private ExceptionServise _exeptionServise;
        private EnemyBattle _enemy;

        private Button _startBattaleButton;
    
        [Inject]
        public void Construct(ExceptionServise exceptionServise)
        {
            _exeptionServise = exceptionServise;
        }

        private void Awake()
        {
            _startBattaleButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _startBattaleButton.onClick.AddListener(StartBattle);
            _startBattaleButton.interactable = false;
        }

        private void OnDisable()
        {
            _startBattaleButton.onClick.RemoveAllListeners();
        }

        public void SelectEnemy(EnemyBattle enemy)
        {
            _enemy = enemy;
            _startBattaleButton.interactable = true;
        }

        private void StartBattle()
        {
            if (_energy.CurrentEnergy <= 0)
            {
                _exeptionServise.PrintException("Not enough energy");
                return;
            }
            
            if (_attackDeck.IsDeckEmpty)
            {
                _exeptionServise.PrintException("You don't have any heroes in your deck");
                return;
            }
                
            _energy.DecreaseEnergy(_battleEnergyPrice);
            _battle.StartFightWith(_enemy);
        }
    }
}

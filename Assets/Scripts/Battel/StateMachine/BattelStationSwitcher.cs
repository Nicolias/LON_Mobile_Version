using Battle;
using FarmPage.Battle;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BattelStationSwitcher : Battel
{
    [SerializeField] private BattelCardsGroup _playerCardsGroup, _enemyCardsGroup;
    [SerializeField] private BattleIntro _battleIntro;

    private BaseState _currentState;
    private List<BaseState> _allState;

    private AttackDeck _playerAttackDeck;

    private CoroutineServise _coroutineServise;

    [Inject]
    public void Construct(AttackDeck attackDeck, CoroutineServise coroutineServise)
    {
        _playerAttackDeck = attackDeck;
        _coroutineServise = coroutineServise;
    }

    public override void Initialize(EnemyBattle enemy)
    {
        gameObject.SetActive(true);

        _allState = new()
        {
            new SetUpBattelState(_playerCardsGroup, _enemyCardsGroup, 
            _playerAttackDeck, enemy, 
            _coroutineServise, _battleIntro, this)
        };

        SwitchState<SetUpBattelState>();
    }

    public void StartFight()
    {
        SwitchState<BaseState>();
    }

    private void SwitchState<T>() where T : BaseState
    {
        if(_currentState != null)
            _currentState.Exit();

        var state = _allState.FirstOrDefault(s => s is T);
        state.Enter();
        _currentState = state;
    }
}

using FarmPage.Battle;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BattelStationSwitcher : Battel
{
    [SerializeField] private BattelCardsGroup _playerCardsGroup, _enemyCardsGroup;

    private BaseState _currentState;
    private List<BaseState> _allState;

    private AttackDeck _playerAttackDeck;

    [Inject]
    public void Construct(AttackDeck attackDeck)
    {
        _playerAttackDeck = attackDeck;
    }

    public override void StartFightWith(EnemyBattle enemy)
    {
        gameObject.SetActive(true);

        StartCoroutine(_playerCardsGroup.Initialize(_playerAttackDeck.CardCellsInDeck));
        StartCoroutine(_enemyCardsGroup.Initialize(enemy.Cards));


        //foreach (var playerCard in _playerCardAnimators)
        //    playerCard.Hide();

        //foreach (var enemyCard in _enemyCardAnimators)
        //    enemyCard.Hide();

        //HideNonActiveCards(_attackDeck.CardsInDeck, _playerCardAnimators);

        //_allState = new()
        //{

        //};
        //_currentState = _allState[0];
        //StartFight();
    }

    public void StartFight()
    {
        SwitchState<BaseState>();
    }

    public void SwitchState<T>() where T : BaseState
    {
        var state = _allState.FirstOrDefault(s => s is T);
        _currentState.Exit();
        state.Enter();
        _currentState = state;
    }
}

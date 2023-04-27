using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattelStationSwitcher : MonoBehaviour
{
    private BaseState _currentState;
    private List<BaseState> _allState;

    private EnemyBattle _enemy;

    public void StartFightWith(EnemyBattle enemy)
    {
        _enemy = enemy;

        //RenderEnemyDefCard();

        //gameObject.SetActive(true);

        //foreach (var playerCard in _playerCardAnimators)
        //    playerCard.Hide();

        //foreach (var enemyCard in _enemyCardAnimators)
        //    enemyCard.Hide();

        //HideNonActiveCards(_attackDeck.CardsInDeck, _playerCardAnimators);

        _allState = new()
        {

        };
        _currentState = _allState[0];
        StartFight();
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

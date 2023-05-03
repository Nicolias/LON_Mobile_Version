using Battle;
using System.Collections;
using UnityEngine;

public class BattelResultState : BaseState
{
    private BattelCardsGroup _playerCardsGroup, _enemyCardsGroup;

    private Window _winWindow, _loseWindow;
    private PrizeWindow _prizeWindow;

    private EnemyBattle _enemy;

    public BattelResultState(Window winWindow, Window loseWindow, PrizeWindow prizeWindow,
        BattelCardsGroup playerCardsGroup, BattelCardsGroup enemyCardsGroup, EnemyBattle enemy) 
        : base(null, null, null, playerCardsGroup, enemyCardsGroup)
    {
        _winWindow = winWindow;
        _loseWindow = loseWindow;
        _prizeWindow = prizeWindow;

        _playerCardsGroup = playerCardsGroup;
        _enemyCardsGroup = enemyCardsGroup;

        _enemy = enemy;
    }

    public override IEnumerator Enter()
    {
        yield return new WaitForSeconds(1);

        if (_playerCardsGroup.CardsInGroup.Count > 0)
        {
            _winWindow.ShowSmooth(_enemy);
            _prizeWindow.Render(_enemy.RandomPrizes);
        }
        else
        {
            _loseWindow.ShowSmooth(_enemy);
        }

        yield break;
    }

    public override void Exit()
    {
        
    }
}
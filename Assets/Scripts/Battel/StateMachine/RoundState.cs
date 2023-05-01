using Battle;
using System.Collections;

public class RoundState : BaseState
{
    private int _currentRound;

    public RoundState(BattelCardsGroup playerCardsGroup, BattelCardsGroup enemyCardsGroup, BattelStationSwitcher battelStationSwitcher, BattleIntro battleIntro,
        CoroutineServise coroutineServise) : base(battelStationSwitcher, coroutineServise, battleIntro, playerCardsGroup, enemyCardsGroup)
    {
    }

    public override IEnumerator Enter()
    {
        _currentRound = BattelStationSwitcher.CurrentRound;

        while (true)
        {
            yield return BattelIntro.PlayRoundIntro(_currentRound);
            yield return _currentRound % 2 != 0 ? PlayerCardsGroup.Turn() : EnemyCardsGroup.Turn();
            yield return false;
        }
    }

    public override void Exit()
    {
        
    }
}
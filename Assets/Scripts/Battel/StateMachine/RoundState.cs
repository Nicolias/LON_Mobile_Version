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
            yield return _currentRound % 2 != 0 ? PlayerCardsGroup.Turn(EnemyCardsGroup) 
                : EnemyCardsGroup.Turn(PlayerCardsGroup);

            if (PlayerCardsGroup.CardsInGroup.Count == 0 || EnemyCardsGroup.CardsInGroup.Count == 0)
                yield break;

            _currentRound++;
        }
    }

    public override void Exit()
    {
        
    }
}
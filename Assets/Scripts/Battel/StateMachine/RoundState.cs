using Battle;
using System.Collections;

public class RoundState : BaseState
{
    private int _currentRound;

    public RoundState(BattelCardsGroup playerCardsGroup, BattelCardsGroup enemyCardsGroup, BattleStationSwitcher battelStationSwitcher, BattleIntro battleIntro,
        CoroutineServise coroutineServise) : base(battelStationSwitcher, coroutineServise, battleIntro, playerCardsGroup, enemyCardsGroup)
    {
    }

    public override IEnumerator Enter()
    {
        _currentRound = BattelStationSwitcher.CurrentRound;

        yield return BattelIntro.PlayRoundIntro(_currentRound);

        yield return _currentRound % 2 != 0 ? Turn(PlayerCardsGroup, EnemyCardsGroup) : Turn(EnemyCardsGroup, PlayerCardsGroup);

        if (PlayerCardsGroup.CardsInGroup.Count != 0 && EnemyCardsGroup.CardsInGroup.Count != 0)
            BattelStationSwitcher.StartNewRound();
        else
            BattelStationSwitcher.SumUpButtel();
    }

    public override void Exit()
    {
        
    }

    private IEnumerator Turn(BattelCardsGroup attacker, BattelCardsGroup defender)
    {
        SwapCardsGroupSibilingIndex(attacker, defender);

        yield return attacker.Turn(defender);
    }

    private void SwapCardsGroupSibilingIndex(BattelCardsGroup sholdeBeUp, BattelCardsGroup sholdeBeDown)
    {
        if (sholdeBeUp.transform.GetSiblingIndex() < sholdeBeDown.transform.GetSiblingIndex())
        {
            int temp = sholdeBeDown.transform.GetSiblingIndex();
            sholdeBeDown.transform.SetSiblingIndex(sholdeBeUp.transform.GetSiblingIndex());
            sholdeBeUp.transform.SetSiblingIndex(temp);
        }
    }
}

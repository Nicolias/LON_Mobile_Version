using Battle;
using System.Collections;

public abstract class BaseState
{
    protected BattelCardsGroup PlayerCardsGroup { get; private set; }
    protected BattelCardsGroup EnemyCardsGroup { get; private set; }

    protected BattelStationSwitcher BattelStationSwitcher { get; private set; }

    protected CoroutineServise CoroutineSevise { get; private set; }
    protected BattleIntro BattelIntro { get; private set; }

    public BaseState(BattelStationSwitcher battelStationSwitcher, CoroutineServise coroutineServise, BattleIntro battleIntro,
        BattelCardsGroup playerCardsGroup, BattelCardsGroup enemyCardsGroup)
    {
        BattelStationSwitcher = battelStationSwitcher;

        CoroutineSevise = coroutineServise;
        BattelIntro = battleIntro;

        EnemyCardsGroup = enemyCardsGroup;
        PlayerCardsGroup = playerCardsGroup;
    }

    public abstract IEnumerator Enter();
    public abstract void Exit();
}

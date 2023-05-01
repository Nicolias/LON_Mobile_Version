
using Battle;
using System.Collections;

public class SetUpBattelState : BaseState
{
    private EnemyBattle _enemy;
    private AttackDeck _playerAttackDeck;

    public SetUpBattelState(BattelCardsGroup playerCardsGroup, BattelCardsGroup enemyCardsGroup, 
        AttackDeck playerAttackDeck, EnemyBattle enemy, 
        CoroutineServise coroutineServise, BattleIntro battleIntro, BattelStationSwitcher battelStationSwitcher) 
        : base(battelStationSwitcher, coroutineServise, battleIntro, playerCardsGroup, enemyCardsGroup)
    {
        _playerAttackDeck = playerAttackDeck;
        _enemy = enemy;        
    }

    public override IEnumerator Enter()
    {
        yield return InitializeCard();
        yield return BattelIntro.PlayButtleIntro();

        BattelStationSwitcher.StartNewRound();
    }

    public override void Exit()
    {

    }

    private IEnumerator InitializeCard()
    {
        if (_playerAttackDeck.CardCellsInDeck.Count > _enemy.Cards.Count)
        {
            CoroutineSevise.StartRoutine(EnemyCardsGroup.Initialize(_enemy.Cards));
            yield return PlayerCardsGroup.Initialize(_playerAttackDeck.CardCellsInDeck);
        }
        else
        {
            CoroutineSevise.StartRoutine(PlayerCardsGroup.Initialize(_playerAttackDeck.CardCellsInDeck));
            yield return EnemyCardsGroup.Initialize(_enemy.Cards);
        }
    }
}
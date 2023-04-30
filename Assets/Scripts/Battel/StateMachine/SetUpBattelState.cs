
using Battle;
using System.Collections;

public class SetUpBattelState : BaseState
{
    private BattelCardsGroup _playerCardsGroup, _enemyCardsGroup;

    private EnemyBattle _enemy;
    private AttackDeck _playerAttackDeck;

    private CoroutineServise _coroutineSevise;
    private BattleIntro _battelIntro;

    public SetUpBattelState(BattelCardsGroup playerCardsGroup, BattelCardsGroup enemyCardsGroup, 
        AttackDeck playerAttackDeck, EnemyBattle enemy, 
        CoroutineServise coroutineServise, BattleIntro battleIntro, BattelStationSwitcher battelStationSwitcher) 
        : base(battelStationSwitcher)
    {
        _playerAttackDeck = playerAttackDeck;

        _enemyCardsGroup = enemyCardsGroup;
        _playerCardsGroup = playerCardsGroup;
        _enemy = enemy;

        _coroutineSevise = coroutineServise;
        _battelIntro = battleIntro;
    }

    public override void Enter()
    {
        _coroutineSevise.StartRoutine(SetUpBattel());
    }

    public override void Exit()
    {

    }

    private IEnumerator SetUpBattel()
    {
        yield return InitializeCard();
        yield return _battelIntro.PlayButtleIntro();

        BattelStationSwitcher.StartFight();
    }

    private IEnumerator InitializeCard()
    {
        if (_playerAttackDeck.CardCellsInDeck.Count > _enemy.Cards.Count)
        {
            _coroutineSevise.StartRoutine(_enemyCardsGroup.Initialize(_enemy.Cards));
            yield return _playerCardsGroup.Initialize(_playerAttackDeck.CardCellsInDeck);
        }
        else
        {
            _coroutineSevise.StartRoutine(_playerCardsGroup.Initialize(_playerAttackDeck.CardCellsInDeck));
            yield return _enemyCardsGroup.Initialize(_enemy.Cards);
        }
    }
}
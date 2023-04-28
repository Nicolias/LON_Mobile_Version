using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestPlayerCards : QuestCollection
{
    public event Action OnValueChanged;

    [SerializeField] private AttackDeck _attackDeck;
    [SerializeField] private PlayerStatisticQuest _playerStatisticQuest;

    [SerializeField] protected Image _blickImage;

    protected override Unit[] GetArrayType()
    {
        return new FarmPage.Quest.Card[_attackDeck.CardCellsInDeck.Count];
    }

    protected override void InitUnit(Unit unit, int position)
    {
        (unit as FarmPage.Quest.Card).Init(_attackDeck.CardCellsInDeck[position], _playerStatisticQuest, _blickImage);
    }
}

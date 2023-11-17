using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestPlayerCards : QuestCollection
{
    public event Action OnValueChanged;

    [SerializeField] private AttackDeck _attackDeck;
    [SerializeField] private PlayerStatisticQuest _playerStatisticQuest;

    [SerializeField] protected Image _blickImage;

    protected override Unit[] GetUnitsArray()
    {
        return new QuestPage.Quest.Card[_attackDeck.Slots.Count];

    }

    protected override void InitUnit(Unit unit, int position)
    {
        (unit as QuestPage.Quest.Card).Init(_attackDeck.Slots[position].CardView.CardData, _playerStatisticQuest, _blickImage);
    }
}

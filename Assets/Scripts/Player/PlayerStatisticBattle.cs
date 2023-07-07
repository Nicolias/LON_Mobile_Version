﻿using System;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace QuestPage.Battle
{
    public class PlayerStatisticBattle : PlayerStatistic
    {
        [SerializeField] private TMP_Text _powerText;

        [SerializeField] private AttackDeck _attackDeck;

        protected override void UpdateDisplay()
        {
            base.UpdateDisplay();

            _powerText.text = _attackDeck.Power.ToString();
        }
    }
}
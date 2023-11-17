using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Infrastructure.Services;
using QuestPage.Shop;
using UnityEngine;
using Zenject;

public class GoldWallet : Wallet
{    
    private void OnEnable()
    {        
        RefreshText();
    }
}
using System;
using Data;
using UnityEngine;
using TMPro;
using Zenject;
using Infrastructure.Services;

public abstract class Wallet : MonoBehaviour
{    
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] protected int _amountMoney;
    
    public int AmountMoney => _amountMoney;

    protected void RefreshText() => 
        _textMoney.text = _amountMoney.ToString();

    virtual public void WithdrawСurrency(int money)
    {
        if (money > _amountMoney)
            throw new InvalidOperationException();
        
        _amountMoney -= money;
        UpdateСurrencyText();
    }

    virtual public void AddСurrency(int countMoney)
    {
        _amountMoney += countMoney;
        UpdateСurrencyText();
    }

    private void UpdateСurrencyText() => 
        _textMoney.text = _amountMoney.ToString();
}

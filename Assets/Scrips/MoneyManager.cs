using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private Text moneyUI;
    public BigInteger Money {  get; private set; }
    public static MoneyManager instance;

    private void UpdateMoneyUI()
    {
        moneyUI.text = string.Format("{0} ", MoneyFormatter.FormatMoney(Money));
    }

    void Start()
    {
        Money = 0;
        UpdateMoneyUI();
        instance = this;
    }

    public bool Buy(BigInteger cost)
    {
        bool isBuyOPSuccessfull = false;
        if (cost <= Money) {
            Money -= cost;
            isBuyOPSuccessfull = true;
        }
        UpdateMoneyUI();
        return isBuyOPSuccessfull;
    }

    public void AddMoney(BigInteger profit)
    {
        if (profit > 0)
        {
            Money += profit;
            UpdateMoneyUI();
        }
    }

    void Update()
    {
        
    }
}

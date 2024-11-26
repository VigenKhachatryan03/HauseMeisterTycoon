using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] private GameObject buildingVisuals;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private string costRepresentation;
    [SerializeField] private GameObject collectProfitButton;

    private Text collectProfitButtonText;

    [SerializeField] private int buildingLvl = 1;
    [SerializeField] private int profitMultiplier = 1;
    [SerializeField] BigInteger Profit;

    [SerializeField] private int upgradeCostMultiplier = 10;

    private BigInteger NextUpgradeCost
    {
        get
        {
            return buildingLvl * upgradeCostMultiplier;
        }
    }

    [SerializeField] private GameObject upgradeButton;
    private Text upgradeButtonText;
    public BigInteger Cost
    {
        get { return BigInteger.Parse(costRepresentation); }
        set { costRepresentation = value.ToString(); }
    }

    private bool isUnlocked = false;

    private Text buyButtonText; 
    
    void Start()
    {
        buyButtonText = buyButton.GetComponentInChildren<Text>();

        buyButtonText.text = MoneyFormatter.FormatMoney(Cost);

        buyButton.SetActive(!isUnlocked);

        buildingVisuals.SetActive(isUnlocked);

        collectProfitButtonText = collectProfitButton.GetComponentInChildren<Text>();

        collectProfitButton.SetActive(isUnlocked);

        upgradeButtonText = upgradeButton.GetComponentInChildren<Text>();

        upgradeButton.SetActive(isUnlocked);
        
        UpdateUpgradeUI();

        StartCoroutine(MakeProfit());
    }

    IEnumerator MakeProfit()
    {
        while (true)
        {
            if (isUnlocked)
            {
                Profit += buildingLvl * profitMultiplier;
                UpdateProfitUI();
            }
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    private void UpdateProfitUI()
    {
        collectProfitButtonText.text = MoneyFormatter.FormatMoney(Profit);
    }

    private void UpdateUpgradeUI()
    {
        upgradeButtonText.text = $"^\nLVL {buildingLvl}\n{MoneyFormatter.FormatMoney(NextUpgradeCost)}";
    }


    public void OnBuyButton()
    {
        if (!isUnlocked)
        {
            if (MoneyManager.instance.Buy(Cost))
            {
                isUnlocked = true;

                buildingVisuals.SetActive(isUnlocked);

                buyButton.SetActive(!isUnlocked);

                collectProfitButton.SetActive(isUnlocked);

                upgradeButton.SetActive(isUnlocked);
            }
        }
    }

    public void OnCollectProfitButton()
    {
        MoneyManager.instance.AddMoney(Profit);
        Profit = 0;
        UpdateProfitUI();
    }

    public void OnUpgradeButton()
    {
        if (MoneyManager.instance.Buy(NextUpgradeCost)) 
        {
            buildingLvl += 1;
            UpdateUpgradeUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

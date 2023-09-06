using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prestige : MonoBehaviour
{
    private static Prestige instance;

    public static Prestige Instance => instance;

    private void Awake()
    {
        // Ensures only one instance of the Prestige exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public void StartPrestige()
    {
        // Calculate the goblin investor bonus based on total money earned
        double goblinBonus = CalculateGoblinInvestorBonus();

        // Calculate the new goblin investors after prestige
        PlayerStats.Instance.goblinInvestors += goblinBonus;

        // Reset certain aspects of the game
        PlayerStats.Instance.sessionEarnings = 0; // Reset total money earned
        GameManager.Instance.coins = 0;

        // Apply the goblin investor bonus to the game's economy for the next playthrough
        ApplygoblinInvestorBonus(goblinBonus);
    }

    public double CalculateGoblinInvestorBonus()
    {
        // Calculate the goblin investor bonus based on the formula:
        float goblinBonus = 150 * Mathf.Sqrt((float)(PlayerStats.Instance.sessionEarnings / 1e12)) - (float)PlayerStats.Instance.goblinInvestors;
        double goblinBonusFloat = (double)goblinBonus;

        // Ensure the goblin investor bonus is non-negative
        if (goblinBonus < 0)
            goblinBonus = 0;

        return goblinBonus;
    }

    private void ApplygoblinInvestorBonus(double goblinBonus)
    {
        // Calculate the profit increase percentage based on the number of goblin investors
        double profitIncreasePercentage = goblinBonus * 0.02;
        UpgradeManager.Instance.UnapplyAllUpgrades();
        ManagersManager.Instance.UnapplyAllManagers();
        // Apply the profit increase to each store
        foreach (Store store in StoreManager.Instance.GetAllStores())
        {
            // Calculate the new profit amount after applying the goblin investor bonus
            double newProfit = store.inititalFirstStoreProfit * (1 + profitIncreasePercentage);
            store.ResetToInitialValues();
            store.firstStoreProfit = newProfit;
            store.nbrOfStores = string.Compare(store.nameOfStore, "Bread", StringComparison.OrdinalIgnoreCase) == 0 ? 1 : 0;
        }
        PlayerStats.Instance.sessionEarnings = 0;
    }
}

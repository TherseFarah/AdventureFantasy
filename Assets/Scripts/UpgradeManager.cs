using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataClasses;

public class UpgradeManager : MonoBehaviour
{
    // Static instance to make it accessible from other scripts
    public static UpgradeManager Instance { get; private set; }

    // List to store all upgrades
    public List<Upgrades> allUpgrades = new List<Upgrades>();


    [SerializeField] Image popup;

    private void Awake()
    {
        // Singleton pattern to ensure there's only one UpgradeManager instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddUpgrade(Upgrades upgrade)
    {
        if (!allUpgrades.Contains(upgrade))
        {
            allUpgrades.Add(upgrade);
        }
    }

    public void RemoveUpgrade(Upgrades upgrade)
    {
        if (allUpgrades.Contains(upgrade))
        {
            allUpgrades.Remove(upgrade);
        }
    }

    private void Start()
    {
        popup.enabled = false;
    }

    private void Update()
    {
        bool canBuyAnyUpgrade = false; // Initialize a flag to check if any upgrade is affordable

        foreach (var upgrade in allUpgrades)
        {
            if (GameManager.Instance.CanBuy(upgrade.price) && !upgrade.isBought)
            {
                canBuyAnyUpgrade = true; // At least one upgrade is affordable
            }
        }

        // Set the popup based on the canBuyAnyUpgrade flag
        popup.enabled = canBuyAnyUpgrade;
    }

    public void UnapplyAllUpgrades()
    {
        // Loop through all upgrades
        foreach (Upgrades upgrade in allUpgrades)
        {
            if (upgrade.isBought)
            {
                upgrade.UnapplyUpgrade();
            }
        }
    }
}

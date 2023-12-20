using UnityEngine;
using System.IO;
using System.Collections.Generic;
using static DataClasses;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    private string savePath;
    public static SaveLoadManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        if (File.Exists(savePath))
        {
            LoadGame();
        }
        else
        {
            Debug.Log("No save data found.");
        }
    }

    public void SaveGame()
    {
        Debug.Log("We are saving the game in path" + savePath);
        SaveData saveData = new SaveData
        {
            gameManagerData = new GameManagerData
            {
                coins = GameManager.Instance.coins,
                multiplier = GameManager.Instance.multiplier,
            },
            upgradeData = new List<UpgradeData>(),
            storeData = new List<StoreData>()
        };

        // Populate UpgradeData
        foreach (Upgrades upgrade in UpgradeManager.Instance.allUpgrades)
        {
            UpgradeData upgradeData = new UpgradeData
            {
                stores = upgrade.stores,
                upgradeValue = upgrade.upgradeValue,
                price = upgrade.price,
                isBought = upgrade.isBought,
            };
            saveData.upgradeData.Add(upgradeData);
        }

        // Populate StoreData
        foreach (Store store in StoreManager.Instance.GetAllStores())
        {
            StoreData storeData = new StoreData
            {
                initialbaseBuyingPrice = store.initialbaseBuyingPrice,
                initialNbrOfStore = store.initialNbrOfStore,
                inititalFirstStoreProfit = store.inititalFirstStoreProfit,
                initialTimer = store.initialTimer,

                nameOfStore = store.nameOfStore,
                baseBuyingPrice = store.baseBuyingPrice,
                priceIncreaseModifier = store.priceIncreaseModifier,
                nbrOfStores = store.nbrOfStores,
                firstStoreProfit = store.firstStoreProfit,
                hasManager = store.hasManager,
                unlockLevels = store.unlockLevels,
                timer = store.timer,
                nextUnlockIndex = store.nextUnlockIndex,
            };
            saveData.storeData.Add(storeData);
        }


        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        string json = File.ReadAllText(savePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        // Load game data from saveData and update your game's objects
        GameManager.Instance.coins = saveData.gameManagerData.coins;
        GameManager.Instance.multiplier = saveData.gameManagerData.multiplier;

        // Populate upgrades after they have been added in Start method
        StartCoroutine(LoadUpgrades(saveData.upgradeData));
        // Populate stores
        StartCoroutine(LoadStores(saveData.storeData));
    }

    private IEnumerator LoadStores(List<StoreData> storeDataList)
    {
        // Wait for one frame to ensure the Start method has been called
        yield return null;

        // Populate upgrades
        foreach (StoreData storeData in storeDataList)
        {
            Store store = StoreManager.Instance.allStores.Find(st => st.nameOfStore == storeData.nameOfStore);

            if (store != null)
            {
                // Update the store data based on the loaded data
                store.initialbaseBuyingPrice = storeData.initialbaseBuyingPrice;
                store.initialNbrOfStore = storeData.initialNbrOfStore;
                store.inititalFirstStoreProfit = storeData.inititalFirstStoreProfit;
                store.initialTimer = storeData.initialTimer;

                store.baseBuyingPrice = storeData.baseBuyingPrice;
                store.priceIncreaseModifier = storeData.priceIncreaseModifier;
                store.nbrOfStores = storeData.nbrOfStores;
                store.firstStoreProfit = storeData.firstStoreProfit;
                store.hasManager = storeData.hasManager;
                store.unlockLevels = storeData.unlockLevels;
                store.timer = storeData.timer;
                store.nextUnlockIndex = storeData.nextUnlockIndex;
                if (store.hasManager)
                {
                    // Find the associated Manager
                    Managers manager = ManagersManager.Instance.allManagers.Find(m => m.store == store);

                    if (manager != null)
                    {
                        // Disable the manager button
                        manager.managerBuy.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.LogWarning($"Manager for store {store.nameOfStore} not found.");
                    }
                }
            }
            else
            {
                Debug.LogWarning($"Store with name {storeData.nameOfStore} not found.");
            }
        }
    }

    private IEnumerator LoadUpgrades(List<UpgradeData> upgradeDataList)
    {
        // Wait for one frame to ensure the Start method has been called
        yield return null;

        // Populate upgrades
        foreach (UpgradeData upgradeData in upgradeDataList)
        {
            Upgrades upgrade = UpgradeManager.Instance.allUpgrades.Find(u => u.price == upgradeData.price);

            if (upgrade != null)
            {
                Debug.Log("The upgrade was found");
                upgrade.isBought = upgradeData.isBought;

                // Find the associated UpgradesDisplay
                UpgradesDisplay associatedDisplay = FindUpgradesDisplay(upgrade);

                if (associatedDisplay != null)
                {
                    Button associatedButton = associatedDisplay.GetComponent<Button>();

                    // Pass the Button reference when applying the upgrade
                    if (upgrade.isBought)
                    {
                        upgrade.ApplyUpgrade(associatedButton);
                    }
                }
            }
            else
            {
                Debug.Log($"Upgrade with price {upgradeData.price} not found.");
            }
        }
    }
    private UpgradesDisplay FindUpgradesDisplay(Upgrades upgrade)
    {
        // Iterate through all UpgradesDisplay objects
        UpgradesDisplay[] allUpgradesDisplays = GameObject.FindObjectsOfType<UpgradesDisplay>();

        foreach (UpgradesDisplay upgradesDisplay in allUpgradesDisplays)
        {
            // Check if the Upgrades object in UpgradesDisplay matches the target upgrade
            if (upgradesDisplay.upgrade == upgrade)
            {
                return upgradesDisplay;
            }
        }

        // If no matching UpgradesDisplay is found
        Debug.Log($"UpgradesDisplay for Upgrades object not found.");
        return null;
    }
}

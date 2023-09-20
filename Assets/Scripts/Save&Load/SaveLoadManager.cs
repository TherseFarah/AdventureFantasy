using UnityEngine;
using System.IO;
using System.Collections.Generic;
using static DataClasses;

public class SaveLoadManager : MonoBehaviour
{
    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            gameManagerData = new GameManagerData
            {
                coins = GameManager.Instance.coins,
                multiplier = GameManager.Instance.multiplier,
            },
            upgradeData = new List<UpgradeData>(),
            managerData = new List<ManagerData>(),
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

        // Populate ManagerData
        foreach (Managers manager in ManagersManager.Instance.allManagers)
        {
            ManagerData managerData = new ManagerData
            {
                store = manager.store,
            };
            saveData.managerData.Add(managerData);
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
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // Load game data from saveData and update your game's objects
            GameManager.Instance.coins = saveData.gameManagerData.coins;
            // Update other game manager variables

            // Populate upgrades, managers, and stores with data from saveData
        }
        else
        {
            Debug.Log("No save data found.");
        }
    }
}

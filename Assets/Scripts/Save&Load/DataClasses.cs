using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DataClasses : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        public GameManagerData gameManagerData;
        public List<UpgradeData> upgradeData;
        public List<ManagerData> managerData;
        public List<StoreData> storeData;
    }

    [System.Serializable]
    public class GameManagerData
    {
        public double coins;
        public int multiplier;
    }

    [System.Serializable]
    public class UpgradeData
    {
        public Store[] stores; // Store names associated with this upgrade
        public double upgradeValue;
        public double price;
        public bool isBought;
    }

    [System.Serializable]
    public class ManagerData
    {
        public Store store;
    }

    [System.Serializable]
    public class StoreData
    {
        public double initialbaseBuyingPrice;
        public int initialNbrOfStore;
        public double inititalFirstStoreProfit;
        public float initialTimer;

        public string nameOfStore;
        public double baseBuyingPrice;
        public float priceIncreaseModifier;
        public int nbrOfStores;
        public double firstStoreProfit;
        public bool hasManager;
        public int[] unlockLevels;
        public float timer;
        public int nextUnlockIndex;
    }


}

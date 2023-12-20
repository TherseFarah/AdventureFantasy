using UnityEngine;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour
{
    private static StoreManager instance;
    public static StoreManager Instance => instance;

    public List<Store> allStores = new List<Store>();

    private void Awake()
    {
        // Ensures only one instance of the StoreManager exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public void AddStore(Store store)
    {
        if (!allStores.Contains(store))
        {
            allStores.Add(store);
        }
    }

    public void RemoveStore(Store store)
    {
        if (allStores.Contains(store))
        {
            allStores.Remove(store);
        }
    }

    public List<Store> GetAllStores()
    {
        return allStores;
    }
}

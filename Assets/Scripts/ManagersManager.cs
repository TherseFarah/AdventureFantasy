using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagersManager : MonoBehaviour
{
    // Static instance to make it accessible from other scripts
    public static ManagersManager Instance { get; private set; }

    // List to store all Managers
    public List<Managers> allManagers = new List<Managers>();


    [SerializeField] Image popup;

    private void Awake()
    {
        // Singleton pattern to ensure there's only one ManagerManager instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddManager(Managers manager)
    {
        if (!allManagers.Contains(manager))
        {
            allManagers.Add(manager);
        }
    }

    public void RemoveManager(Managers manager)
    {
        if (allManagers.Contains(manager))
        {
            allManagers.Remove(manager);
        }
    }

    private void Start()
    {
        popup.enabled = false;
    }

    private void Update()
    {
        bool canBuyAnyManager = false; // Initialize a flag to check if any Manager is affordable

        foreach (var manager in allManagers)
        {
            if (GameManager.Instance.CanBuy(manager.store.managerCost) && !manager.store.hasManager)
            {
                canBuyAnyManager = true; // At least one Manager is affordable
            }
        }

        // Set the popup based on the canBuyAnyManager flag
        popup.enabled = canBuyAnyManager;
    }

    public void UnapplyAllManagers()
    {
        // Loop through all upgrades
        foreach (Managers manager in allManagers)
        {
            if (manager.store.hasManager)
            {
                manager.store.UnapplyManager();
            }
        }
    }
}

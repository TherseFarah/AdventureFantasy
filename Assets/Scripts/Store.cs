using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Tilemaps.TilemapRenderer;

[CreateAssetMenu(fileName ="CreateStore", menuName ="Store")]
public class Store : ScriptableObject
{
    // Original initial values
    // REMOVE WHEN GAME IS READY
    public double initialbaseBuyingPrice;
    private int initialNbrOfStore;
    public double inititalFirstStoreProfit;
    private float initialTimer;

    public string nameOfStore;
    public double managerCost;
    public double baseBuyingPrice;
    public float priceIncreaseModifier;
    public int nbrOfStores;
    public double firstStoreProfit;
    public bool hasManager;

    public int[] unlockLevels;

    public float timer;
    [HideInInspector]
    public float currentTimer = 0f;
    [HideInInspector]
    public bool startTimer = false;

    public int nextUnlockIndex = 0;


    public void BuyStoreButton()
    {
        Debug.Log("Buying Store at "+ CalculatePrice());
        if (GameManager.Instance.CanBuy(CalculatePrice()))
        {
            GameManager.Instance.AddBalance(-CalculatePrice());
            nbrOfStores += GameManager.Instance.multiplier;
            Debug.Log("The number of " + nameOfStore + " is now: " + (nbrOfStores));
            for (int i = 0; i < GameManager.Instance.multiplier; i++)
            {
                baseBuyingPrice *= priceIncreaseModifier;
            }
        }
    }

    public void UnlockStore()
    {
        nbrOfStores = 1;
        GameManager.Instance.AddBalance(-baseBuyingPrice);
        baseBuyingPrice *= priceIncreaseModifier;
    }

    public void BuyManagerButton(Button managerButton)
    {
        if (GameManager.Instance.CanBuy(managerCost))
        {
            hasManager = true; // Set the flag to indicate the manager is purchased
            GameManager.Instance.AddBalance(-managerCost);
            managerButton.gameObject.SetActive(false);

            // Automatically start the timer if the manager is purchased
            if (!startTimer)
            {
                startTimer = true;
                currentTimer = 0f;
            }
        }
    }

    public void StoreClick()
    {
        Debug.Log("Store Click is being clicked");
        if (!startTimer)
        {
            startTimer = true;
            Debug.Log("Timer is true");
        }
    }

    public bool CanUnlockStore()
    {
        if (GameManager.Instance.CanBuy(baseBuyingPrice))
        {
            // If no requirement is set, the store is always unlockable
            return true;
        }
        return false;
    }

    public double CalculatePrice()
    {
        if (ButtonController.buttonValue == -1)
        {
            GameManager.Instance.multiplier = GetMaxAffordableStores(baseBuyingPrice);
        }
        else if(ButtonController.buttonValue == 0)
        {
            if (nextUnlockIndex < unlockLevels.Length)
            {

                GameManager.Instance.multiplier = unlockLevels[nextUnlockIndex] - nbrOfStores;
            }
            else
            {
                GameManager.Instance.multiplier = 1;
            }
        }
        else
        {
            GameManager.Instance.multiplier = ButtonController.buttonValue;
        }

        // Calculate the cumulative price using the formula for the sum of a geometric series and return it
        return baseBuyingPrice * (1 - Mathf.Pow(priceIncreaseModifier, GameManager.Instance.multiplier)) / (1 - priceIncreaseModifier);
    }
    public int GetMaxAffordableStores(double basePrice)
    {
        // Calculate the maximum number of stores that can be bought with the current balance
        int maxAffordableStores = 0;
        double balance = GameManager.Instance.coins;

        while (balance >= basePrice)
        {
            balance -= basePrice;
            basePrice *= priceIncreaseModifier;
            maxAffordableStores++;
        }

        return maxAffordableStores;
    }

    // REMOVE WHEN GAME IS READY
    public void ResetToInitialValues()
    {
        baseBuyingPrice = initialbaseBuyingPrice;
        nbrOfStores = initialNbrOfStore;
        firstStoreProfit = inititalFirstStoreProfit;
        timer = initialTimer;
        currentTimer = 0f;
        nextUnlockIndex = 0;
        startTimer = false;
        hasManager = false;
    }

    public void StoreInitialValues()
    {
        initialbaseBuyingPrice = baseBuyingPrice;
        initialNbrOfStore = nbrOfStores;
        inititalFirstStoreProfit = firstStoreProfit;
        initialTimer = timer;
        hasManager = false;
    }
}

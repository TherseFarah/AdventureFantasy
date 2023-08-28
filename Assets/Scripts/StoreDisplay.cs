using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreDisplay : MonoBehaviour
{
    public Store store;

    public TextMeshProUGUI nbrOfStoreText;
    public TextMeshProUGUI storeCostText;
    public TextMeshProUGUI storeXLevelText;
    public TextMeshProUGUI profitFromStoreText;
    public TextMeshProUGUI nextUnlockText;

    public Button buyButton;
    public Button clickButton;

    public Slider timerSlider;

    public Slider progressSlider;

    bool isInteractable;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners for buttons
        buyButton.onClick.AddListener(() => store.BuyStoreButton());
        clickButton.onClick.AddListener(() => store.StoreClick());

        isInteractable = store.nbrOfStores == 0 ? false : true;

        // Register the store with the StoreManager when it starts
        StoreManager.Instance.AddStore(store);
        nextUnlockText.text = "Next Unlock:" + store.unlockLevels[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInteractable)
        {
            UpdateCanvasInteractability();
        }
        UpdateTimer();
        UpdateProgressSlider();
        UpdateUI();
    }
    private void UpdateCanvasInteractability()
    {
        if (store.nbrOfStores == 1)
        {
            store.nbrOfStores = 1;
            isInteractable = true;
        }
        gameObject.GetComponent<CanvasGroup>().interactable = isInteractable;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = isInteractable;
    }
    void UpdateTimer()
    {
        if (store.hasManager || store.startTimer)
        {
            store.currentTimer += Time.deltaTime;
            if (store.currentTimer > store.timer)
            {
                store.startTimer = false;
                store.currentTimer = 0f;
                GameManager.Instance.AddBalance(store.nbrOfStores * store.firstStoreProfit);
            }
        }
    }

    // TODO: CHANGE THIS TO STORE.CS

    void UpdateProgressSlider()
    {
        if (store.nextUnlockIndex == 0)
        {
            progressSlider.value = (float)store.nbrOfStores / store.unlockLevels[store.nextUnlockIndex];
        }
        else
        {
            float progress;
            if (store.nextUnlockIndex >= store.unlockLevels.Length)
            {
                progress = 1.0f;
            }
            else
            {
                int prevUnlockLevel = store.unlockLevels[store.nextUnlockIndex - 1];
                int currentUnlockLevel = store.unlockLevels[store.nextUnlockIndex];

                int progressFromPrevUnlock = store.nbrOfStores - prevUnlockLevel;
                int progressToNextUnlock = currentUnlockLevel - prevUnlockLevel;

                progress = (float)progressFromPrevUnlock / progressToNextUnlock;
            }
            
            progressSlider.value = progress;
        }

        if (store.nextUnlockIndex < store.unlockLevels.Length)
        {

            if (store.nbrOfStores >= store.unlockLevels[store.nextUnlockIndex])
            {
                if (store.timer > 0.2)
                {
                    store.timer /= 2;
                }
                else
                {
                    store.firstStoreProfit*= 2;
                }
                store.nextUnlockIndex++;
                if (store.nextUnlockIndex >= store.unlockLevels.Length)
                {
                    nextUnlockText.text = "No More Unlocks";
                }
                else
                {
                    nextUnlockText.text = "Next Unlock:" + store.unlockLevels[store.nextUnlockIndex];
                }
            }
        }
    }

    void UpdateUI()
    {
        buyButton.interactable = (GameManager.Instance.CanBuy(store.CalculatePrice()) && GameManager.Instance.multiplier!=0);
        storeCostText.text = "Buy " + GameManager.Instance.FormatMoneyValue(store.CalculatePrice());
        nbrOfStoreText.text = store.nbrOfStores.ToString();
        if (store.timer < 0.2 && store.hasManager)
        {
            timerSlider.value = 1;
        }
        else
        {
            timerSlider.value = store.currentTimer / store.timer;
        }
        profitFromStoreText.text = GameManager.Instance.FormatMoneyValue(store.nbrOfStores * store.firstStoreProfit);
        storeXLevelText.text = "x" + GameManager.Instance.multiplier;
    }

    // REMOVE LATER
    private void Awake()
    {
        // Store the initial values when the game starts
        store.StoreInitialValues();
    }

    private void OnDestroy()
    {
        // Reset the values when the game stops
        store.ResetToInitialValues();

        // Remove the store from the StoreManager when it is destroyed
        StoreManager.Instance.RemoveStore(store);
    }
}

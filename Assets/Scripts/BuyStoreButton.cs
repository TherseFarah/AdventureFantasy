using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyStoreButton : MonoBehaviour
{
    public Button buyStoreButton;
    public GameObject storeCanvas;
    public Store store;

    private TextMeshProUGUI buttonTextMesh;
    private Button prestigeButton;

    private void Start()
    {
        // Get the TextMesh component from the child GameObject of the Button
        buttonTextMesh = buyStoreButton.GetComponentInChildren<TextMeshProUGUI>();

        // Set the initial state of the UI elements
        buyStoreButton.gameObject.SetActive(store.nbrOfStores == 0);
        buyStoreButton.interactable = false;
        storeCanvas.gameObject.SetActive(false);
        storeCanvas.GetComponent<CanvasGroup>().enabled = store.nbrOfStores > 0;

        // Add a listener to the buy store button
        buyStoreButton.onClick.AddListener(() => BuyStore());
        PrestigeDisplay.Instance.prestigeButton.onClick.AddListener(() => OnPrestige());

        // TODO: CHANGE TEXT
        buttonTextMesh.text = "Buy " + store.nameOfStore + " " + GameManager.Instance.FormatMoneyValue(store.baseBuyingPrice);
        
    }

    private void Update()
    {
        if(store.CanUnlockStore())
        {
            buyStoreButton.interactable = true;
        }
        else
        {
            buyStoreButton.interactable = false;
        }
    }

    public void OnPrestige()
    {
        // Set the initial state of the UI elements
        buyStoreButton.gameObject.SetActive(true);
        buyStoreButton.interactable = false;
        storeCanvas.gameObject.SetActive(false);
        storeCanvas.GetComponent<CanvasGroup>().enabled = store.nbrOfStores > 0;
    }

    private void BuyStore()
    {
        if (store.CanUnlockStore())
        {
            // Hide and disable the buy store button
            buyStoreButton.gameObject.SetActive(false);
            buyStoreButton.interactable = false;

            // Show and enable the store canvas
            storeCanvas.gameObject.SetActive(true);
            storeCanvas.GetComponent<CanvasGroup>().enabled = true;

            // Increase the number of stores by 1
            store.UnlockStore();
        }
    }
}

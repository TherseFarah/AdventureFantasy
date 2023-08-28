using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesDisplay : MonoBehaviour
{
    public Upgrades upgrade;
    [SerializeField] Image popup; 

    private Button upgradeButton;



    // Start is called before the first frame update
    void Start()
    {
        upgradeButton = GetComponent<Button>();
        // Add listeners for buttons
        upgradeButton.onClick.AddListener(() => upgrade.ApplyUpgrade(upgradeButton));
        upgradeButton.onClick.AddListener(() => popup.enabled = false);
        if (upgrade.stores.Length == 1)
        {
            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = upgrade.stores[0].nameOfStore + " profit x" + upgrade.upgradeValue +"\nPrice: "+ GameManager.Instance.FormatMoneyValue(upgrade.price);
        }
        else
        {
            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "All Shops profit x" + upgrade.upgradeValue +"\nPrice: "+ GameManager.Instance.FormatMoneyValue(upgrade.price);
        }
        upgradeButton.interactable = false;
        popup.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CanBuy(upgrade.price))
        {
            upgradeButton.interactable = true;
            popup.enabled=true;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    private void OnDestroy()
    {
        // Reset the values when the game stops
        upgrade.isBought = false;
    }
}

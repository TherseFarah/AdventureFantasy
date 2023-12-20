using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesDisplay : MonoBehaviour
{
    public Upgrades upgrade;

    private Button upgradeButton;

    // Start is called before the first frame update
    void Start()
    {
        UpgradeManager.Instance.AddUpgrade(upgrade);
        upgradeButton = GetComponent<Button>();
        // Add listeners for buttons
        upgradeButton.onClick.AddListener(() => upgrade.ApplyUpgrade(upgradeButton));
        if (upgrade.stores.Length == 1)
        {
            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = upgrade.stores[0].nameOfStore + " profit x" + upgrade.upgradeValue +"\nPrice: "+ GameManager.Instance.FormatMoneyValue(upgrade.price);
        }
        else
        {
            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "All Shops profit x" + upgrade.upgradeValue +"\nPrice: "+ GameManager.Instance.FormatMoneyValue(upgrade.price);
        }
        upgradeButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CanBuy(upgrade.price))
        {
            upgradeButton.interactable = true;
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

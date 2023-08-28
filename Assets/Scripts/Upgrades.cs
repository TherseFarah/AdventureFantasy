using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CreateUpgrade", menuName = "Upgrade")]
public class Upgrades : ScriptableObject
{
    public Store [] stores;
    public double upgradeValue;
    public double price;
    public bool isBought;


    public void ApplyUpgrade(Button upgradesButton)
    {
        if (!isBought)
        {
            if (GameManager.Instance.CanBuy(price))
            {
                foreach(Store store in stores)
                {
                    store.firstStoreProfit *= upgradeValue;
                }
                GameManager.Instance.AddBalance(-price);
                upgradesButton.gameObject.SetActive(false);
                isBought = true;
            }
        }
    }
}

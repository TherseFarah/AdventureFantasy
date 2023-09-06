using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public double coins;
    public TextMeshProUGUI coinsText;
    public Canvas gameCanvas;
    public Canvas managerCanvas;
    public Canvas upgradeCanvas;
    public Canvas prestigeCanvas;
    public Canvas unlocksCanvas;
    public int multiplier = 1;

    private void Awake()
    {
        // Check if an instance of GameManager already exists
        if (instance != null && instance != this)
        {
            // If an instance already exists, destroy this duplicate instance
            Destroy(gameObject);
            return;
        }

        // If no instance exists, set this as the instance
        instance = this;
        // TODO: Ensure that the GameManager persists across scenes
        // REMOVE WHEN GAME IS READY
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameCanvas.enabled = true;
        managerCanvas.enabled = false;
        upgradeCanvas.enabled = false;
        prestigeCanvas.enabled = false;
        unlocksCanvas.enabled = false;
        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = FormatMoneyValue(coins);
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    public void AddBalance(double valueToAdd)
    {
        coins += valueToAdd;
        if (valueToAdd > 0)
        {
            PlayerStats.Instance.sessionEarnings += valueToAdd;
            PlayerStats.Instance.totalEarnings += valueToAdd;
        }
    }

    public bool CanBuy(double amount)
    {
        return coins >= amount;
    }

    public void ToggleManagerCanvas()
    {
        managerCanvas.enabled = !managerCanvas.enabled;
        gameCanvas.enabled = !gameCanvas.enabled;
    }

    public void ToggleUpgradesCanvas()
    {
        upgradeCanvas.enabled = !upgradeCanvas.enabled;
        gameCanvas.enabled = !gameCanvas.enabled;
    }

    public void TogglePrestigeCanvas()
    {
        prestigeCanvas.enabled = !prestigeCanvas.enabled;
        gameCanvas.enabled = !gameCanvas.enabled;
    }

    public void ToggleUnlocksCanvas()
    {
        unlocksCanvas.enabled = !unlocksCanvas.enabled;
        gameCanvas.enabled = !gameCanvas.enabled;
    }


    public string FormatMoneyValue(double moneyValue)
    {
        string[] suffixes = { "", "K", "M", "B", "T", "qa", "Qi", "sx", "Sp", "Oc", "No", "De" };

        int suffixIndex = 0;
        while (moneyValue >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            moneyValue /= 1000;
            suffixIndex++;
        }

        string formattedValue = moneyValue.ToString("F2") + suffixes[suffixIndex];
        return formattedValue;
    }

}

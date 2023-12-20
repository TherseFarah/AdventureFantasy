using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public double coins;
    public TextMeshProUGUI coinsText;
    public Canvas gameCanvas;
    public Canvas managerCanvas;
    public Canvas upgradeCanvas;
    public Canvas prestigeCanvas;
    public Canvas unlocksCanvas;
    public int multiplier;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        coins = 0;
        multiplier = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameCanvas.enabled = true;
        managerCanvas.enabled = false;
        upgradeCanvas.enabled = false;
        prestigeCanvas.enabled = false;
        unlocksCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = FormatMoneyValue(coins);
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

    // Example: Call SaveGame when the player quits the game
    void OnApplicationQuit()
    {
        SaveLoadManager.Instance.SaveGame();
        Debug.Log("Game saved on exit");
    }


}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrestigeDisplay : MonoBehaviour
{
    public TextMeshProUGUI goblinsGathered;
    public Button prestigeButton;
    [SerializeField] Image popup;

    public static PrestigeDisplay Instance { get; private set; }

    void Awake()
    {
        // Assign the static instance on Awake
        Instance = this;
    }

    void Start()
    {
        // Add listeners for buttons
        prestigeButton.onClick.AddListener(() => Prestige.Instance.StartPrestige());
        prestigeButton.onClick.AddListener(() => popup.enabled = false);
        popup.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        prestigeButton.interactable = Prestige.Instance.CalculateGoblinInvestorBonus() > 1;
        popup.enabled = prestigeButton.interactable;
        if (Prestige.Instance.CalculateGoblinInvestorBonus() != 0)
        {
            goblinsGathered.text = GameManager.Instance.FormatMoneyValue(Prestige.Instance.CalculateGoblinInvestorBonus()) + " Goblins attracted.";
        }
        else
        {
            goblinsGathered.text = "No Goblins attracted.";
        }
    }
}

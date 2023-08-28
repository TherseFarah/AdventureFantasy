using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Managers : MonoBehaviour
{
    public Store store;
    [SerializeField] Image popup;

    private Button managerBuy;


    // Start is called before the first frame update
    void Start()
    {
        managerBuy = GetComponent<Button>();
        managerBuy.onClick.AddListener(() => store.BuyManagerButton(GetComponent<Button>()));
        managerBuy.onClick.AddListener(() => popup.enabled = false);
        managerBuy.GetComponentInChildren<TextMeshProUGUI>().text = "Buy " + store.nameOfStore + " manager.\n" + GameManager.Instance.FormatMoneyValue(store.managerCost);
        managerBuy.interactable = false;
        popup.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.CanBuy(store.managerCost))
        {
            managerBuy.interactable = true;
            popup.enabled= true;
        }
        else
        {
            managerBuy.interactable = false;
        }
    }
}

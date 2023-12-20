using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Managers : MonoBehaviour
{
    public Store store;

    public Button managerBuy;


    // Start is called before the first frame update
    void Start()
    {
        ManagersManager.Instance.AddManager(this);
        managerBuy = GetComponent<Button>();
        managerBuy.onClick.AddListener(() => store.BuyManagerButton(GetComponent<Button>()));
        managerBuy.GetComponentInChildren<TextMeshProUGUI>().text = "Buy " + store.nameOfStore + " manager.\n" + GameManager.Instance.FormatMoneyValue(store.managerCost);
        managerBuy.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.CanBuy(store.managerCost))
        {
            managerBuy.interactable = true;
        }
        else
        {
            managerBuy.interactable = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats instance;

    public static PlayerStats Instance => instance;

    public double sessionEarnings;
    public double totalEarnings;
    public double goblinInvestors;

    private void Awake()
    {
        // Ensures only one instance of the Awake exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        goblinInvestors = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        sessionEarnings = 0;
        totalEarnings = 0;
    }
}

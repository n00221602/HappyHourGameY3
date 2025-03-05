using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    private CustomerNPC customerNPC;
    public float moneyBalance = 0f;
    public bool moneyGiven = false;
    [SerializeField] TextMeshProUGUI moneyBalanceText;

    void Start()
    {

        customerNPC = GetComponent<CustomerNPC>();  // Assign once in Start()
        
        if (customerNPC == null)
        {
            Debug.LogError("CustomerNPC component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        moneyBalanceText.text = moneyBalance.ToString();

        if (customerNPC == null)
        {
            return; 
        }

        if (customerNPC.CustomerBeer != null && customerNPC.CustomerBeer.activeSelf)
        {
            if (!moneyGiven){
                moneyUpdate();
            }
            
        }
        else
        {
           moneyGiven = false;
        }
    }

    void moneyUpdate()
    {
        Debug.Log("CustomerBeer is active.");
        moneyBalance += 1f;
        Debug.Log("Your Total Balance Is: " + moneyBalance);
        moneyGiven = true;
    }
}


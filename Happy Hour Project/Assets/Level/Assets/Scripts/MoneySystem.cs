using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private CustomerNPC customerNPC;
    public float moneyBalance;
    public bool moneyGiven = false;

    void Start()
    {
        moneyBalance = 0f;
        customerNPC = GetComponent<CustomerNPC>();  // Assign once in Start()
        
        if (customerNPC == null)
        {
            Debug.LogError("CustomerNPC component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (customerNPC == null)
        {
            return; // Prevent null reference errors
        }

        // Debug: Verify if moneyUpdate is being called
        //Debug.Log("Money update is running...");

        if (customerNPC.CustomerBeer != null && customerNPC.CustomerBeer.activeSelf)
        {
            if (moneyGiven == false){
                moneyUpdate();
            }
            
        }
        else
        {
           // Debug.Log("CustomerBeer is not active or is null.");
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

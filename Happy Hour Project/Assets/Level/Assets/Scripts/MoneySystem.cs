using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public CustomerNPC customerNPC;
    public float moneyBalance;

    void Start()
    {
        moneyBalance = 0f;
    }

    void Update()
    {
        // Debug: Verify if moneyUpdate is being called
        Debug.Log("Money update is running...");
        moneyUpdate();
    }

    void moneyUpdate()
    {
        if (customerNPC == null)
        {
            Debug.LogError("customerNPC is NULL! Make sure it's assigned in the Inspector.");
            return;
        }
        else
        {
            Debug.Log("customerNPC is assigned.");
        }

        if (customerNPC.customerBeer == null)
        {
            Debug.LogError("customerBeer is NULL! Make sure it's assigned in NPCCustomer.");
            return;
        }
        else
        {
            Debug.Log("customerBeer is assigned.");
        }

        if (customerNPC.customerBeer.activeSelf)
        {
            Debug.Log("customerBeer is active.");
            moneyBalance += 1f;  // Corrected to use +=
            Debug.Log("Your Total Balance Is: " + moneyBalance);
        }
        else
        {
            Debug.Log("customerBeer is not active.");
        }
    }
}
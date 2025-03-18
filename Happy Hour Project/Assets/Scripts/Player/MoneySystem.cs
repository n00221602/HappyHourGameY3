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
        moneyBalance = 0f;

        customerNPC = FindObjectOfType<CustomerNPC>();
        if (customerNPC == null)
        {
            Debug.LogError("CustomerNPC component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        Debug.Log(moneyBalance);
        moneyBalanceText.text = moneyBalance.ToString();

        if (customerNPC == null)
        {
            return;
        }

        // BEER MONEY
        if (customerNPC.CustomerBeer != null && customerNPC.CustomerBeer.activeSelf)
        {
            if (!moneyGiven)
            {
                beerMoneyAddition();
            }
        }
        else
        {
            moneyGiven = false;
        }

        // RED WINE MONEY
        if (customerNPC.CustomerRedWine != null && customerNPC.CustomerRedWine.activeSelf)
        {
            if (!moneyGiven)
            {
                redWineMoneyAddition();
            }
        }
        else
        {
            moneyGiven = false;
        }

        // WHITE WINE MONEY
        if (customerNPC.CustomerWhiteWine != null && customerNPC.CustomerWhiteWine.activeSelf)
        {
            if (!moneyGiven)
            {
                whiteWineMoneyAddition();
            }
        }
        else
        {
            moneyGiven = false;
        }
    }

    void beerMoneyAddition()
    {
        Debug.Log("CustomerBeer is active.");
        moneyBalance += 5f;
        Debug.Log("Your Total Balance Is: " + moneyBalance);
        moneyGiven = true;
    }

    void redWineMoneyAddition()
    {
        Debug.Log("CustomerRedWine is active.");
        moneyBalance += 8f;
        Debug.Log("Your Total Balance Is: " + moneyBalance);
        moneyGiven = true;
    }

    void whiteWineMoneyAddition()
    {
        Debug.Log("CustomerWhiteWine is active.");
        moneyBalance += 8f;
        Debug.Log("Your Total Balance Is: " + moneyBalance);
        moneyGiven = true;
    }
}
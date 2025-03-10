using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    private CustomerNPC customerNPC;
    public float moneyBalance = 0f;
    public bool beerMoneyGiven = false;
    public bool whiteWineMoneyGiven = false;
    public bool redWineMoneyGiven = false;

    [SerializeField] TextMeshProUGUI moneyBalanceText;
    public string displayedMoney;

    void Start()
    {

        customerNPC = GetComponent<CustomerNPC>();  
        
        if (customerNPC == null)
        {
            Debug.LogError("CustomerNPC component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        moneyBalanceText.text = ("$" + moneyBalance.ToString());
        
        if (customerNPC == null)
        {
            return; 
        }
        //---------------BEER MONEY SYSTEM--------------------
        if (customerNPC.CustomerBeer != null && customerNPC.CustomerBeer.activeSelf)
        {
            if (!beerMoneyGiven){
                beerMoneyUpdate();
            }
            
        }
        else
        {
           beerMoneyGiven = false;
        }

                //---------------WHITE WINE MONEY SYSTEM--------------------

                if (customerNPC.CustomerWhiteWine != null && customerNPC.CustomerWhiteWine.activeSelf)
        {
            if (!whiteWineMoneyGiven){
                whiteWineMoneyUpdate();
            }
            
        }
        else
        {
           whiteWineMoneyGiven = false;
        }

                        //---------------RED WINE MONEY SYSTEM--------------------

                if (customerNPC.CustomerRedWine != null && customerNPC.CustomerRedWine.activeSelf)
        {
            if (!redWineMoneyGiven){
                redWineMoneyUpdate();
            }
            
        }
        else
        {
           redWineMoneyGiven = false;
        }
    }

    void beerMoneyUpdate()
    {
        Debug.Log("CustomerBeer is active.");
        moneyBalance += 5f;
        Debug.Log("Your Total Balance Is: " + moneyBalance);
        beerMoneyGiven = true;
    }

        void whiteWineMoneyUpdate()
    {
        Debug.Log("CustomerWhiteWine is active.");
        moneyBalance += 8f;
        Debug.Log("Your Total Balance Is: " + moneyBalance);
        whiteWineMoneyGiven = true;
    }

        void redWineMoneyUpdate()
    {
        Debug.Log("CustomerRedWine is active.");
        moneyBalance += 8f;
        Debug.Log("Your Total Balance Is: " + moneyBalance);
        redWineMoneyGiven = true;
    }
}


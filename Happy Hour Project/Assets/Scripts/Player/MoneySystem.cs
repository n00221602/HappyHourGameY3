using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    private CustomerNPC customerNPC;
    public float moneyBalance;
    public bool moneyGiven = false;
    [SerializeField] TextMeshProUGUI moneyBalanceText;

    void Start()
    {
        moneyBalanceText = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Debug.Log(moneyBalance);

        //if (customerNPC == null)
        //{
        //    customerNPC = FindObjectOfType<CustomerNPC>();
        //    if (customerNPC == null)
        //    {
        //        Debug.LogError("CustomerNPC component not found.");
        //        return;
        //    }
        //}

        //// BEER MONEY
        //if (customerNPC.CustomerBeer.activeSelf)
        //{
        //    beerMoneyAddition();
        //}

        //// RED WINE MONEY
        //if (customerNPC.CustomerRedWine.activeSelf)
        //{
        //    redWineMoneyAddition();
        //}

        //// WHITE WINE MONEY
        //if (customerNPC.CustomerWhiteWine.activeSelf)
        //{
        //    whiteWineMoneyAddition();
        //}

        //// Reset moneyGiven only when all customers are inactive
        //if (!customerNPC.CustomerBeer.activeSelf && !customerNPC.CustomerRedWine.activeSelf && !customerNPC.CustomerWhiteWine.activeSelf)
        //{
        //    moneyGiven = false;
        //}
    }

    public void beerMoneyAddition()
    {
        if (!moneyGiven)
        {
            Debug.Log("CustomerBeer is active.");
            moneyBalance += 5f;
            Debug.Log("Your Total Balance Is: " + moneyBalance);
            //moneyGiven = true;
            UpdateText();
        }
    }

    public void redWineMoneyAddition()
    {
        if (!moneyGiven)
        {
            Debug.Log("CustomerRedWine is active.");
            moneyBalance += 8f;
            Debug.Log("Your Total Balance Is: " + moneyBalance);
            //moneyGiven = true;
            UpdateText();
        }
    }

    public void whiteWineMoneyAddition()
    {
        if (!moneyGiven)
        {
            Debug.Log("CustomerWhiteWine is active.");
            moneyBalance += 8f;
            Debug.Log("Your Total Balance Is: " + moneyBalance);
            //moneyGiven = true;
            UpdateText();
        }
    }

    void UpdateText()
    {
        moneyBalanceText.text = "$" + moneyBalance;
    }
}


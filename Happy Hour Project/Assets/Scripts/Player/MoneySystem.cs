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
        //sets the players initial money to 0
        moneyBalance = 0f;
        //updates the players money in the UI
        UpdateText();
    }

    //adds money depending on what drink the players serves
    public void beerMoneyAddition()
    {
        if (!moneyGiven)
        {
            moneyBalance += 5f;
            //moneyGiven = true;
            UpdateText();
        }
    }

    public void redWineMoneyAddition()
    {
        if (!moneyGiven)
        {
            moneyBalance += 8f;
            //moneyGiven = true;
            UpdateText();
        }
    }

    public void whiteWineMoneyAddition()
    {
        if (!moneyGiven)
        {
            moneyBalance += 8f;
            //moneyGiven = true;
            UpdateText();
        }
    }

       public void canMoneyAddition()
    {
        if (!moneyGiven)
        {
            moneyBalance += 1f;
            //moneyGiven = true;
            UpdateText();
        }
    }

       public void bottleBeerMoneyAddition()
    {
        if (!moneyGiven)
        {
            moneyBalance += 3f;
            //moneyGiven = true;
            UpdateText();
        }
    }

    //constantly updates the players UI with the appropriate balance
    public void UpdateText()
    {
        moneyBalanceText.text = "$" + moneyBalance;
    }
}


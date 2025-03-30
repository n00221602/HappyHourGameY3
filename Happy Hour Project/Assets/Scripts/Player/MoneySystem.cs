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

    void UpdateText()
    {
        moneyBalanceText.text = "$" + moneyBalance;
    }
}


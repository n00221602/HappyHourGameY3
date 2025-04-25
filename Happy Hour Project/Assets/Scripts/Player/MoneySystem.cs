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
        moneyBalance = 100f;
    }

    private void Update()
    {
       Debug.Log("Bakance is:" + moneyBalance);
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

    public void UpdateText()
    {
        moneyBalanceText.text = "$" + moneyBalance;
    }
}


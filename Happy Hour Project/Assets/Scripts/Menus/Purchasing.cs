using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{

    public GameObject AirHockeyTable;
    public MoneySystem currentMoney;

    void Start()
    {
    AirHockeyTable.SetActive(false);
    currentMoney = GetComponent<MoneySystem>();  
    }

    public void purchaseAirHockeyTable(){
        if(currentMoney.moneyBalance >= 5f)
        {
        AirHockeyTable.SetActive(true);
        currentMoney.moneyBalance -=5f;

        }
    }
}

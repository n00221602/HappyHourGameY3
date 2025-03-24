using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    private CustomerNPC customerNPC;
    public float moneyBalance = 0f;
    [SerializeField] TextMeshProUGUI moneyBalanceText;

    void Start(){
        moneyBalanceText = GameObject.Find("Player/PlayerUi/Money").GetComponent<TextMeshProUGUI>();

    }
    
    void beerMoneyAddition()
    {
        //Debug.Log("CustomerBeer is active.");
        moneyBalance += 5f;
       // Debug.Log("Your Total Balance Is: " + moneyBalance);
    }

    void redWineMoneyAddition()
    {
       // Debug.Log("CustomerRedWine is active.");
        moneyBalance += 8f;
       // Debug.Log("Your Total Balance Is: " + moneyBalance);
    }

    void whiteWineMoneyAddition()
    {
      //  Debug.Log("CustomerWhiteWine is active.");
        moneyBalance += 8f;
       // Debug.Log("Your Total Balance Is: " + moneyBalance);
    }
     void Update()
    {
         moneyBalanceText.text = moneyBalance.ToString();

    }

 
//
//  void Update()
//  {
//      Debug.Log(moneyBalance);
//      moneyBalanceText.text = moneyBalance.ToString();
//
//      if (customerNPC == null)
//      {
//          return;
//      }
//
//      // BEER MONEY
//      if (customerNPC.CustomerBeer != null && customerNPC.CustomerBeer.activeSelf)
//      {
//          if (!moneyGiven)
//          {
//              beerMoneyAddition();
//          }
//      }
//      else
//      {
//          moneyGiven = false;
//      }
//
//      // RED WINE MONEY
//      if (customerNPC.CustomerRedWine != null && customerNPC.CustomerRedWine.activeSelf)
//      {
//          if (!moneyGiven)
//          {
//              redWineMoneyAddition();
//          }
//      }
//      else
//      {
//          moneyGiven = false;
//      }
//
//      // WHITE WINE MONEY
//      if (customerNPC.CustomerWhiteWine != null && customerNPC.CustomerWhiteWine.activeSelf)
//      {
//          if (!moneyGiven)
//          {
//              whiteWineMoneyAddition();
//          }
//      }
//      else
//      {
//          moneyGiven = false;
//      }
//  }

}

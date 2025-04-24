using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchasing : MonoBehaviour
{

    public GameObject AirHockeyTable;
    public GameObject PoolTable;
    public GameObject DartsBoard;
    public GameObject SlotMachines;
    public GameObject Jukebox;
    public GameObject Karaoke;
    public GameObject BeerGarden;
    public GameObject Toilets;

    public MoneySystem currentMoney;
    public PlayerMovement moveSpeed;

    void Start()
    {
    AirHockeyTable.SetActive(false);
    PoolTable.SetActive(false);
    DartsBoard.SetActive(false);
    SlotMachines.SetActive(false);
    Jukebox.SetActive(false);
    Karaoke.SetActive(false);
    BeerGarden.SetActive(true);
    Toilets.SetActive(true);

    currentMoney = FindObjectOfType<MoneySystem>();
    moveSpeed = GetComponent<PlayerMovement>();  

    }

    public void purchaseAirHockeyTable(){

        if( currentMoney != null && currentMoney.moneyBalance >= 5f)
        {
        currentMoney.moneyBalance -=5f;
        currentMoney.UpdateText();

        AirHockeyTable.SetActive(true);
        }

        if(AirHockeyTable.activeSelf)
        {
            Invoke(nameof(PassiveIncome), 5f);
        }
    }

    public void purchasePoolTable(){
      if( currentMoney != null && currentMoney.moneyBalance >= 5f)
        {
        currentMoney.moneyBalance -=5f;
        currentMoney.UpdateText();

      PoolTable.SetActive(true);
        }
        else
        {
      PoolTable.SetActive(false);
            Debug.Log("Not enough money !");

        }
    }

    public void purchaseDartsBoard(){
      if( currentMoney != null && currentMoney.moneyBalance >= 5f)
        {
        currentMoney.moneyBalance -=5f;
        currentMoney.UpdateText();

      DartsBoard.SetActive(true);
        }
        else
        {
      DartsBoard.SetActive(false);
            Debug.Log("Not enough money !");

        }
    }

    public void purchaseSlotMachines(){

          if( currentMoney != null && currentMoney.moneyBalance >= 5f)
        {
        currentMoney.moneyBalance -=5f;
        currentMoney.UpdateText();

      SlotMachines.SetActive(true);
        }
        else
        {
      SlotMachines.SetActive(false);
            Debug.Log("Not enough money !");

        }
    }

    public void purchaseJukebox(){

          if( currentMoney != null && currentMoney.moneyBalance >= 5f)
        {
        currentMoney.moneyBalance -=5f;
        currentMoney.UpdateText();

      Jukebox.SetActive(true);
        }
        else
        {
      Jukebox.SetActive(false);
            Debug.Log("Not enough money !");

        }
    }

    public void purchaseKaraoke(){

       if( currentMoney != null && currentMoney.moneyBalance >= 5f)
        {
        currentMoney.moneyBalance -=5f;
        currentMoney.UpdateText();

      Karaoke.SetActive(true);
        }
        else
        {
      Karaoke.SetActive(false);
            Debug.Log("Not enough money !");    

        }
    }

    public void purchaseBeerGarden(){

      if( currentMoney != null && currentMoney.moneyBalance >= 20f)
        {
        currentMoney.moneyBalance -=20f;
        currentMoney.UpdateText();

      BeerGarden.SetActive(true);
        }
        else
        {
      BeerGarden.SetActive(true);
            Debug.Log("Not enough money !");

        }
    }

      public void purchaseToilets(){

            if( currentMoney != null && currentMoney.moneyBalance >= 15f)
        {
        currentMoney.moneyBalance -=15f;
        currentMoney.UpdateText();

      Toilets.SetActive(true);
        }
        else
        {
      Toilets.SetActive(true);
            Debug.Log("Not enough money !");

        }
      Toilets.SetActive(false);
    }

    public void PassiveIncome(){
        currentMoney.moneyBalance +=10;
    }

    //---------------------PERKS-----------------------

    
}

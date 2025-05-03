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

    public GameObject GameDest1;
    public GameObject GameDest2;
    public GameObject GameDest3;
    public GameObject GameDest4;
    public GameObject GameDest5;
    public GameObject GameDest6;
    public GameObject GameDest7;
    public GameObject GameDest8;
    public GameObject GameDest9;
    public GameObject GameDest10;

    public MoneySystem currentMoney;
    public PlayerMovement moveSpeed;

    public CustomerNPC customerNPC;

    private CustomerSpawner spawner;

    void Start()
    {
        AirHockeyTable.SetActive(false);
        PoolTable.SetActive(false);
        DartsBoard.SetActive(false);
        SlotMachines.SetActive(false);
        //Jukebox.SetActive(false);
        Karaoke.SetActive(false);
        BeerGarden.SetActive(true);
        Toilets.SetActive(true);

        GameDest1.SetActive(false);
        GameDest2.SetActive(false);
        GameDest3.SetActive(false);
        GameDest4.SetActive(false);
        GameDest5.SetActive(false);
        GameDest6.SetActive(false);
        GameDest7.SetActive(false);
        GameDest8.SetActive(false);
        GameDest9.SetActive(false);
        GameDest10.SetActive(false);



        currentMoney = FindObjectOfType<MoneySystem>();
        moveSpeed = GetComponent<PlayerMovement>();

       // customerNPC = FindObjectOfType<CustomerNPC>();
       spawner = FindObjectOfType<CustomerSpawner>();

    }

    private void Update()
    {
    }

    public void purchaseAirHockeyTable(){

        if ( currentMoney != null && currentMoney.moneyBalance >= 5f)
        {
        currentMoney.moneyBalance -=5f;
        currentMoney.UpdateText();

        AirHockeyTable.SetActive(true);
        }
        else
        {
            Debug.Log("Not enough money !");
        }

        if (AirHockeyTable.activeSelf)
        {
            Debug.Log("Air hockey purchased!");
            GameDest6.SetActive(true);
            GameDest7.SetActive(true);
            CustomerNPC.ManageGameDestinations();
            PassiveIncome();
        }
    }

    public void purchasePoolTable(){
      if ( currentMoney != null && currentMoney.moneyBalance >= 5f)
      {
        currentMoney.moneyBalance -=5f;
        currentMoney.UpdateText();
        PoolTable.SetActive(true);
      }
      else
      {
        Debug.Log("Not enough money !");
      }

      if (PoolTable.activeSelf)
      {
          Debug.Log("Pool table purchased!");
          GameDest8.SetActive(true);
          GameDest9.SetActive(true);
          CustomerNPC.ManageGameDestinations();
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

        if (DartsBoard.activeSelf)
        {
            Debug.Log("Dartboard purchased!");
            GameDest10.SetActive(true);
            CustomerNPC.ManageGameDestinations();
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

        if (SlotMachines.activeSelf)
        {
            Debug.Log("Slots purchased!");
            GameDest1.SetActive(true);
            GameDest2.SetActive(true);
            GameDest3.SetActive(true);
            GameDest4.SetActive(true);
            GameDest5.SetActive(true);
            CustomerNPC.ManageGameDestinations();
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

      BeerGarden.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough money !");

        }
    }

      public void purchaseToilets(){

            if( currentMoney != null && currentMoney.moneyBalance >= 15f)
        {
        currentMoney.moneyBalance -=15f;
        currentMoney.UpdateText();

      Toilets.SetActive(false);
        }
        else
        {
      Toilets.SetActive(true);
            Debug.Log("Not enough money !");

        }
    }

    public void PassiveIncome(){
        currentMoney.moneyBalance +=10;
        currentMoney.UpdateText();

    }

    //---------------------PERKS-----------------------

    
}

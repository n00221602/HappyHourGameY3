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

    //Games area destinations
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

    //Beer garden destinations
    public GameObject GardenDest1;
    public GameObject GardenDest2;
    public GameObject GardenDest3;
    public GameObject GardenDest4;
    public GameObject GardenDest5;
    public GameObject GardenDest6;
    public GameObject GardenDest7;
    public GameObject GardenDest8;

    public MoneySystem currentMoney;
    public PlayerMovement moveSpeed;
   
    public CustomerNPC customerNPC;

    public CustomerSpawner spawnRate;

    void Start()
    {
        //sets all purchasable items as inactive on default
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

        //links this script to others that are rewuired for functionallity
        GardenDest1.SetActive(false);
        GardenDest2.SetActive(false);
        GardenDest3.SetActive(false);
        GardenDest4.SetActive(false);
        GardenDest5.SetActive(false);
        GardenDest6.SetActive(false);
        GardenDest7.SetActive(false);
        GardenDest8.SetActive(false);

        currentMoney = FindObjectOfType<MoneySystem>();
        moveSpeed = FindObjectOfType<PlayerMovement>();
        spawnRate = FindObjectOfType<CustomerSpawner>();

       // customerNPC = FindObjectOfType<CustomerNPC>();
       spawnRate = FindObjectOfType<CustomerSpawner>();

    }

    private void Update()
    {
    }

    public void purchaseAirHockeyTable(){
        //checks if you have enough money and if you do, deducts it and sets the product to active
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
            //makes this a location NPC's are able to go to'
            CustomerNPC.ManageGameDestinations();
            //allows the player to gain passive income from this product
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
                      PassiveIncome();

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
                        PassiveIncome();

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
                        PassiveIncome();

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
                    PassiveIncome();

      Karaoke.SetActive(true);
        }
        else
        {
      Karaoke.SetActive(false);
            Debug.Log("Not enough money !");    

        }
    }

    public void purchaseBeerGarden()
    {

        if (currentMoney != null && currentMoney.moneyBalance >= 20f)
        {
            currentMoney.moneyBalance -= 20f;
            currentMoney.UpdateText();
            BeerGarden.SetActive(false);
        }

        if (!BeerGarden.activeSelf)
        {
            Debug.Log("Beer garden purchased!");
            GardenDest1.SetActive(true);
            GardenDest2.SetActive(true);
            GardenDest3.SetActive(true);
            GardenDest4.SetActive(true);
            GardenDest5.SetActive(true);
            GardenDest6.SetActive(true);
            GardenDest7.SetActive(true);
            GardenDest8.SetActive(true);
            CustomerNPC.ManageGardenDestinations();
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

    //the player gains money passively as a result of purchasing certain products
    public void PassiveIncome(){
        currentMoney.moneyBalance +=10;
        currentMoney.UpdateText();

    }

    //---------------------PERKS-----------------------

    //makes the player move faster
        public void purchaseMoveSpeed(){

          if( currentMoney != null && currentMoney.moneyBalance >= 10f)
        {
        currentMoney.moneyBalance -=10f;
        currentMoney.UpdateText();

        moveSpeed.IncreaseSpeedPurchased();
        }
        else
        {
      
            Debug.Log("Not enough money !");

        }
    }
    //increases the amount of customers per day
            public void purchasePopularity(){

          if( currentMoney != null && currentMoney.moneyBalance >= 10f)
        {
        currentMoney.moneyBalance -=10f;
        currentMoney.UpdateText();

        spawnRate.PopularityPurchased();
        }
        else
        {
      
            Debug.Log("Not enough money !");

        }
    }
    
}

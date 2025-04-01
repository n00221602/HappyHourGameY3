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
    BeerGarden.SetActive(true);

    currentMoney = GetComponent<MoneySystem>();  
    moveSpeed = GetComponent<PlayerMovement>();  

    }

    public void purchaseAirHockeyTable(){
      AirHockeyTable.SetActive(true);

        if(currentMoney.moneyBalance >= 5f)
        {
        currentMoney.moneyBalance -=5f;

        }
    }

    public void purchasePoolTable(){
      PoolTable.SetActive(true);
    }

    public void purchaseDartsBoard(){
      DartsBoard.SetActive(true);
    }

    public void purchaseSlotMachines(){
      SlotMachines.SetActive(true);
    }

    public void purchaseJukebox(){
      Jukebox.SetActive(true);
    }

    public void purchaseKaraoke(){
      Karaoke.SetActive(true);
    }

    public void purchaseBeerGarden(){
      BeerGarden.SetActive(false);
    }

      public void purchaseToilets(){
      Toilets.SetActive(false);
    }

    //---------------------PERKS-----------------------

    
}

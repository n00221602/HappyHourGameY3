using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shops : MonoBehaviour
{

   public GameObject DrinksShop;
   public GameObject PassivesShop;
   public GameObject BarmanShop;
   public GameObject ExtrasShop;

   public GameObject PooltableButton;
   public GameObject PooltableState;

   public GameObject AirHockeytableButton;
   public GameObject AirHockeytableState;
   
   public GameObject DartsButton;
   public GameObject DartsState;
   
   public GameObject SlotsButton;
   public GameObject SlotsState;
   
   public GameObject JukeboxButton;
   public GameObject JukeboxState;
   
   public GameObject KaraokeButton;
   public GameObject KaraokeState;

   //sets the default shop page
    void Start()
    {
        DrinksShop.SetActive(false);
        PassivesShop.SetActive(false);
        BarmanShop.SetActive(false);
        ExtrasShop.SetActive(false);
    }
    //all of the following functions set the page the player wants to move to as active and all others as inactive
public void MoveToDrinks()
{
        DrinksShop.SetActive(true);
        PassivesShop.SetActive(false);
        BarmanShop.SetActive(false);
        ExtrasShop.SetActive(false);
}

public void MoveToPassives()
{
        DrinksShop.SetActive(false);
        PassivesShop.SetActive(true);
        BarmanShop.SetActive(false);
        ExtrasShop.SetActive(false);
}

public void MoveToBarman()
{
        DrinksShop.SetActive(false);
        PassivesShop.SetActive(false);
        BarmanShop.SetActive(true);
        ExtrasShop.SetActive(false);
}

public void MoveToExtras()
{
        DrinksShop.SetActive(false);
        PassivesShop.SetActive(false);
        BarmanShop.SetActive(false);
        ExtrasShop.SetActive(true);
}

public void CloseShop(){
        DrinksShop.SetActive(false);
        PassivesShop.SetActive(false);
        BarmanShop.SetActive(false);
        ExtrasShop.SetActive(false);
}


}

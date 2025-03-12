using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shops : MonoBehaviour
{

   public GameObject DrinksShop;
   public GameObject PassivesShop;
   public GameObject BarmanShop;
   public GameObject ExtrasShop;
    void Start()
    {
        DrinksShop.SetActive(false);
        PassivesShop.SetActive(false);
        BarmanShop.SetActive(false);
        ExtrasShop.SetActive(false);
    }

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
}

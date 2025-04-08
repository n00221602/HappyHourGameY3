using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRayCast : MonoBehaviour
{
    //Player Hand
    private GameObject FullHand;

    //Beer game objects
    private GameObject PlayerPint;
    private GameObject FullPlayerPint;
    private GameObject BeerFlow;
    public GameObject PourPint;

    //Wine game objects
    private GameObject PlayerWineGlass;
    private GameObject FullPlayerRedWine;
    private GameObject FullPlayerWhiteWine;
    private GameObject RedWine;
    private GameObject WhiteWine;
    private GameObject PouringRed;
    private GameObject PouringWhite;
    private GameObject RedWineLiquid;
    private GameObject WhiteWineLiquid;
    private GameObject PourWineGlass;

    //Soft Drink game objects
    private GameObject PlayerCan;
    public GameObject PickupCan;

    //Bottle Beer game objects
    private GameObject PlayerBottleBeer;
    public GameObject bottleBeerPickup;

    public float drinksInterval;

    //Customer game objects
    private GameObject CustomerBeer;
    private GameObject CustomerRedWine;
    private GameObject CustomerWhiteWine;
    private GameObject CustomerCan;
    private GameObject CustomerBottleBeer;

    //CustomerNPC script
    private CustomerNPC customerNPC;

    //Progress Bar script
    public ProgressBar BeerProgressBar;
    public ProgressBar WineProgressBar;

    //Money System script
    public MoneySystem moneySystem;

    // Start is called before the first frame update
    void Start()
    {
        //Player Hand
        FullHand = GameObject.Find("FullHand");
        //Beer
        PlayerPint = GameObject.Find("PlayerPintGlass");
        FullPlayerPint = GameObject.Find("FullPlayerPintGlass");
        BeerFlow = GameObject.Find("flowingBeer");
        PourPint = GameObject.Find("PlaceholderPint");

        //Wine
        PlayerWineGlass = GameObject.Find("PlayerWineGlass");
        FullPlayerRedWine = GameObject.Find("FullPlayerWineGlassRed");
        FullPlayerWhiteWine = GameObject.Find("FullPlayerWineGlassWhite");
        RedWine = GameObject.Find("Red Wine");
        WhiteWine = GameObject.Find("White Wine");
        PouringRed = GameObject.Find("PouringRed");
        PouringWhite = GameObject.Find("PouringWhite");
        RedWineLiquid = GameObject.Find("RedWineLiquidPouring");
        WhiteWineLiquid = GameObject.Find("WhiteWineLiquidPouring");
        PourWineGlass = GameObject.Find("PlaceholderWineGlass");

        //Soft Drinks
        PlayerCan = GameObject.Find("PlayerCan");
        PickupCan = GameObject.Find("PickupCan");
        
        //Bottle Beer
        PlayerBottleBeer = GameObject.Find("PlayerBottle");
        bottleBeerPickup = GameObject.Find("bottleBeerPickup");

        //Setting player active hand to false
        if (FullHand != null) FullHand.SetActive(false);

        //Setting the beer game objects to false
        if (PlayerPint != null) PlayerPint.SetActive(false);
        if (FullPlayerPint != null) FullPlayerPint.SetActive(false);
        if (BeerFlow != null) BeerFlow.SetActive(false);
        if (PourPint != null) PourPint.SetActive(false);

        //Setting the wine game objects to false
        if (PlayerWineGlass != null) PlayerWineGlass.SetActive(false);
        if (FullPlayerRedWine != null) FullPlayerRedWine.SetActive(false);
        if (FullPlayerWhiteWine != null) FullPlayerWhiteWine.SetActive(false);
        if (RedWine != null) RedWine.SetActive(true);
        if (WhiteWine != null) WhiteWine.SetActive(true);
        if (PouringRed != null) PouringRed.SetActive(false);
        if (PouringWhite != null) PouringWhite.SetActive(false);
        if (RedWineLiquid != null) RedWineLiquid.SetActive(false);
        if (WhiteWineLiquid != null) WhiteWineLiquid.SetActive(false);
        if (PourWineGlass != null) PourWineGlass.SetActive(false);

        //Setting the soft drink game objects to false
        if (PlayerCan != null) PlayerCan.SetActive(false);

        //Setting the bottle beer game objects to false
        if (PlayerBottleBeer != null) PlayerBottleBeer.SetActive(false);

        //progressBar = GameObject.Find("Progress Bar").GetComponent<ProgressBar>();



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
            {

                // BEER
                if (hit.collider.name == "PickupPint")
                {
                    HandleBeer();
                }
                if (hit.collider.name == "PourPint" && PlayerPint.activeSelf)
                {
                    PourBeer();
                }

                // WINE
                if (hit.collider.name == "PickupWine")
                {
                    HandleWine();
                }

                if (hit.collider.name == "Red Wine" && PlayerWineGlass.activeSelf)
                {
                    PourRedWine();
                }

                if (hit.collider.name == "White Wine" && PlayerWineGlass.activeSelf)
                {
                    PourWhiteWine();
                }

                // Soft Drinks
                if (hit.collider.name == "PickupCan")
                {
                    HandleCan();
                }

                // Bottle Beer
                if (hit.collider.name == "bottleBeerPickup")
                {
                    HandleBottleBeer();
                    Debug.Log("Collider is working");
                }
        

                //CUSTOMERS
                if (hit.collider.CompareTag("Customer"))
                {
                    HandleCustomer(hit.collider);
                }
            }
        }
    }

    //Beer Functions
    private void HandleBeer()
    {
        if (PlayerPint != null && !FullHand.activeSelf)
        {
            PlayerPint.SetActive(true);
            FullHand.SetActive(true);
        }
    }

    private void PourBeer()
    {
        if (PlayerPint != null && PlayerPint.activeSelf)
        {
            PourPint.SetActive(true);
            PlayerPint.SetActive(false);
            BeerFlow.SetActive(true);
            drinksInterval = 4f;
            Invoke(nameof(CompleteBeerPour), drinksInterval);
            BeerProgressBar.FillProgressBar();
        }
    }

    void CompleteBeerPour()
    {
        PourPint.SetActive(false);
        PlayerPint.SetActive(false);
        FullPlayerPint.SetActive(true);
        BeerFlow.SetActive(false);
    }

    //Wine Functions
    private void HandleWine()
    {
        if (PlayerWineGlass != null && !FullHand.activeSelf)
        {
            PlayerWineGlass.SetActive(true);
            FullHand.SetActive(true);
        }
    }

    private void PourRedWine()
    {
        if (PlayerWineGlass != null && PlayerWineGlass.activeSelf)
        {
            PourWineGlass.SetActive(true);
            PlayerWineGlass.SetActive(false);
            PouringRed.SetActive(true);
            RedWineLiquid.SetActive(true);
            RedWine.SetActive(false);
            drinksInterval = 2.5f;
            Invoke(nameof(CompleteRedWinePour), drinksInterval);
            WineProgressBar.FillProgressBar();
        }
    }

    void CompleteRedWinePour()
    {
        PourWineGlass.SetActive(false);
        FullPlayerRedWine.SetActive(true);
        PouringRed.SetActive(false);
        RedWineLiquid.SetActive(false);
        RedWine.SetActive(true);
    }

    private void PourWhiteWine()
    {
        if (PlayerWineGlass != null && PlayerWineGlass.activeSelf)
        {
            PourWineGlass.SetActive(true);
            PlayerWineGlass.SetActive(false);
            PouringWhite.SetActive(true);
            WhiteWineLiquid.SetActive(true);
            WhiteWine.SetActive(false);
            drinksInterval = 2.5f;
            Invoke(nameof(CompleteWhiteWinePour), drinksInterval);
            WineProgressBar.FillProgressBar();
        }
    }

    void CompleteWhiteWinePour()
    {
        PourWineGlass.SetActive(false);
        FullPlayerWhiteWine.SetActive(true);
        PouringWhite.SetActive(false);
        WhiteWineLiquid.SetActive(false);
        WhiteWine.SetActive(true);
    }

        //Soft Drink Functions
    private void HandleCan()
    {
        if (PlayerCan != null && !FullHand.activeSelf)
        {
            PlayerCan.SetActive(true);
            FullHand.SetActive(true);
        }
    }

    //Bottle Beer Functions
    private void HandleBottleBeer()
    {
        if (PlayerBottleBeer != null && !FullHand.activeSelf)
        {
            PlayerBottleBeer.SetActive(true);
            FullHand.SetActive(true);
        }
    }
    public void HandleCustomer(Collider customerCollider)
    {
        CustomerNPC customerNPC = customerCollider.GetComponent<CustomerNPC>();
        if (customerNPC == null)
        {
            Debug.LogError("customerNPC is null");
            return;
        }

        // Handle Drinks
        if (FullPlayerPint.activeSelf && customerNPC.iconBeer.activeSelf)
        {
            customerNPC.CustomerBeer.SetActive(true);
            FullPlayerPint.SetActive(false);
            FullHand.SetActive(false);
            moneySystem.beerMoneyAddition();
        }

        if (FullPlayerRedWine.activeSelf && customerNPC.iconRedWine.activeSelf)
        {
            customerNPC.CustomerRedWine.SetActive(true);
            FullPlayerRedWine.SetActive(false);
            FullHand.SetActive(false);
            moneySystem.redWineMoneyAddition();
        }

        if (FullPlayerWhiteWine.activeSelf && customerNPC.iconWhiteWine.activeSelf)
        {
            customerNPC.CustomerWhiteWine.SetActive(true);
            FullPlayerWhiteWine.SetActive(false);
            FullHand.SetActive(false);
            moneySystem.whiteWineMoneyAddition();
        }

        if (PlayerCan.activeSelf && customerNPC.iconCan.activeSelf)
        {
            customerNPC.CustomerCan.SetActive(true);
            PlayerCan.SetActive(false);
            FullHand.SetActive(false);
            moneySystem.canMoneyAddition();
        }

        if (PlayerBottleBeer.activeSelf && customerNPC.iconBottleBeer.activeSelf)
        {
            customerNPC.CustomerBottleBeer.SetActive(true);
            PlayerBottleBeer.SetActive(false);
            FullHand.SetActive(false);
            moneySystem.bottleBeerMoneyAddition();
        }
    }
}

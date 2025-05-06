using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRayCast : MonoBehaviour
{
    public Animator animator;

    //Player Hand
    public GameObject FullHand;

    //Sink - Dispose of unwanted drink
    private GameObject Sink;

    //Sound effects
    public AudioSource moneySound;
    public AudioSource putDownSound;
    public AudioSource pouringSound;
    public AudioSource swingSound;


    //Beer game objects
    public GameObject PlayerPint;
    public GameObject FullPlayerPint;
    public GameObject BeerFlow;
    public GameObject PourPint;

    //Wine game objects
    public GameObject PlayerWineGlass;
    public GameObject FullPlayerRedWine;
    public GameObject FullPlayerWhiteWine;
    public GameObject RedWine;
    public GameObject WhiteWine;
    public GameObject PouringRed;
    public GameObject PouringWhite;
    public GameObject RedWineLiquid;
    public GameObject WhiteWineLiquid;
    public GameObject PourWineGlass;

    //Soft Drink game objects
    public GameObject PlayerCan;
    public GameObject PickupCan;

    //Bottle Beer game objects
    public GameObject PlayerBottleBeer;
    public GameObject bottleBeerPickup;

    public float drinksInterval;
    public float progressInterval;

    //Baseball bat
    public GameObject BaseballBat;
    public GameObject PlayerBaseballBat;

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
    private HoldProgressBar MessyTableProgressBar;

    //Money System script
    public MoneySystem moneySystem;

    //Customer Spawner script
    private CustomerSpawner customerSpawner;
    public GameObject OpenSign;

    //Player Movement script
    public PlayerMovement playerMovement;

    //Messy table prefab
    private GameObject currentMess;

   public bool isMessyTable;

    // Start is called before the first frame update
    void Start()
    {
        
        customerSpawner = OpenSign.GetComponent<CustomerSpawner>();


        //Soft Drinks
        PickupCan = GameObject.Find("PickupCan");
        
        //Bottle Beer
        PlayerBottleBeer = GameObject.Find("PlayerBottle");
        bottleBeerPickup = GameObject.Find("bottleBeerPickup");

        //Setting player active hand to false
        if (FullHand != null) FullHand.SetActive(false);
        if (PlayerBaseballBat != null) PlayerBaseballBat.SetActive(false);

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
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
        {
            //Handles left mouse clicks
            if (Input.GetMouseButtonDown(0))
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
                }
        

                //CUSTOMERS
                //Handles picking up the baseball bat
                if (hit.collider.name == "BaseballBatDisplay")
                {
                    HandleBaseballBat();
                }

                //Handles MessyEvent
                if (hit.collider.CompareTag("MessyTable"))
                {
                    isMessyTable = true;
                    MessyTableProgressBar = null;

                    //This finds the ProgressBar component unique to this MessyTable prefab
                    Transform tableCanvas = hit.collider.transform.Find("TableCanvas");
                    if (tableCanvas != null)
                    {
                        Transform tableProgressBar = tableCanvas.Find("TableProgressBar");
                        if (tableProgressBar != null)
                        {
                            MessyTableProgressBar = tableProgressBar.GetComponent<HoldProgressBar>();
                        }
                    }
                    else
                    {
                        Debug.LogError("Progress bar not found");
                    }
                    currentMess = hit.collider.gameObject;
                    HandleMessyEvent();
                }

                //If the player lets go of the left mouse button, they will stop cleaning the table
                if (Input.GetMouseButtonUp(0))
                {
                    isMessyTable = false;
                    CancelInvoke(nameof(CompleteMessyEvent));
                }

                //Handles the customer interaction
                if (hit.collider.CompareTag("Customer"))
                {
                    HandleCustomer(hit.collider);
                }

                //Sink
                if (hit.collider.name == "Sink")
                {
                    DisposeOfDrink();
                }
            }

           

            //Handles the Timer
            if (Input.GetKeyDown(KeyCode.E) && !customerSpawner.timerRunning && hit.collider.name == "OpenSign")
            {
                HandleTimer();
            }
        }

        //Right click inputs
        if (Input.GetMouseButtonDown(1) && PlayerBaseballBat.activeSelf)
        {
            if (animator != null)
            {
                animator.SetTrigger("SwingBat");
                swingSound.Play();
                
            }
            
            //Handles FightEvent
            if (hit.collider != null && hit.collider.CompareTag("Fighter"))
            {
                Debug.Log("Fighter Found");
                hit.collider.gameObject.tag = "FighterHit";
                HandleFightEvent();
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
            progressInterval = 4f;
            playerMovement.moveSpeed = 0f;
            Invoke(nameof(CompleteBeerPour), progressInterval);
            BeerProgressBar.FillProgressBar();
            putDownSound.Play();
            pouringSound.Play();
        }
    }

    void CompleteBeerPour()
    {
        playerMovement.moveSpeed = 5f;
        PourPint.SetActive(false);
        PlayerPint.SetActive(false);
        FullPlayerPint.SetActive(true);
        BeerFlow.SetActive(false);
        pouringSound.Stop();
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
            progressInterval = 3f;
            playerMovement.moveSpeed = 0f;
            Invoke(nameof(CompleteRedWinePour), progressInterval);
            WineProgressBar.FillProgressBar();
            putDownSound.Play();
            pouringSound.Play();
        }
    }

    void CompleteRedWinePour()
    {
        playerMovement.moveSpeed = 5f;
        PourWineGlass.SetActive(false);
        FullPlayerRedWine.SetActive(true);
        PouringRed.SetActive(false);
        RedWineLiquid.SetActive(false);
        RedWine.SetActive(true);
        pouringSound.Stop();
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
            progressInterval = 3f;
            playerMovement.moveSpeed = 0f;
            Invoke(nameof(CompleteWhiteWinePour), progressInterval);
            WineProgressBar.FillProgressBar();
            putDownSound.Play();
            pouringSound.Play();
        }
    }

    void CompleteWhiteWinePour()
    {
        playerMovement.moveSpeed = 5f;
        PourWineGlass.SetActive(false);
        FullPlayerWhiteWine.SetActive(true);
        PouringWhite.SetActive(false);
        WhiteWineLiquid.SetActive(false);
        WhiteWine.SetActive(true);
        FullHand.SetActive(true);
        pouringSound.Stop();
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
        customerNPC = customerCollider.GetComponent<CustomerNPC>();
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
            moneySound.Play();
        }

        if (FullPlayerRedWine.activeSelf && customerNPC.iconRedWine.activeSelf)
        {
            customerNPC.CustomerRedWine.SetActive(true);
            FullPlayerRedWine.SetActive(false);
            FullHand.SetActive(false);
            moneySystem.redWineMoneyAddition();
            moneySound.Play();
        }

        if (FullPlayerWhiteWine.activeSelf && customerNPC.iconWhiteWine.activeSelf)
        {
            customerNPC.CustomerWhiteWine.SetActive(true);
            FullPlayerWhiteWine.SetActive(false);
            FullHand.SetActive(false);
            moneySystem.whiteWineMoneyAddition();
            moneySound.Play();
        }

        if (PlayerCan.activeSelf && customerNPC.iconCan.activeSelf)
        {
            customerNPC.CustomerCan.SetActive(true);
            PlayerCan.SetActive(false);
            FullHand.SetActive(false);
            moneySystem.canMoneyAddition();
            moneySound.Play();
        }

        if (PlayerBottleBeer.activeSelf && customerNPC.iconBottleBeer.activeSelf)
        {
            customerNPC.CustomerBottleBeer.SetActive(true);
            PlayerBottleBeer.SetActive(false);
            FullHand.SetActive(false);
            moneySystem.bottleBeerMoneyAddition();
            moneySound.Play();
        }
    }

     //Sink
    private void DisposeOfDrink()
    {
        FullHand.SetActive(false);
        PlayerPint.SetActive(false);
        FullPlayerPint.SetActive(false);
        PlayerWineGlass.SetActive(false);
        FullPlayerRedWine.SetActive(false);
        FullPlayerWhiteWine.SetActive(false);
        PlayerCan.SetActive(false);
        PlayerBottleBeer.SetActive(false);
    }
    private void HandleMessyEvent()
    {
        if (isMessyTable == true)
        {
            progressInterval = 3f;
            Invoke(nameof(CompleteMessyEvent), progressInterval);
            MessyTableProgressBar.FillProgressBarHold();
        }

    }

    void CompleteMessyEvent()
    {
        Destroy(currentMess);
        customerNPC.table.gameObject.tag = "Clean";
    }

    private void HandleFightEvent()
    {
        Debug.Log("FIGHT EVENT");
        
    }

    private void HandleBaseballBat()
    {
        if (!FullHand.activeSelf && !PlayerBaseballBat.activeSelf)
        {
            BaseballBat.SetActive(false);
            PlayerBaseballBat.SetActive(true);
            FullHand.SetActive(true);
        }

        else
        {
            BaseballBat.SetActive(true);
            PlayerBaseballBat.SetActive(false);

            // This resets the rotation of the baseball bat, so that when the player picks it back up it's at its base position
            PlayerBaseballBat.transform.localRotation = Quaternion.Euler(-8, -177.68f, 25);
            FullHand.SetActive(false);
        }
    }

    //Handles the current day and the legnth of the current day from the CustomerSpawner script.
    private void HandleTimer()
    {
        Debug.Log("Timer started");
        OpenSign.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        customerSpawner.currentDay = customerSpawner.currentDay + 1f;
        customerSpawner.timerRunning = true;
    }

}

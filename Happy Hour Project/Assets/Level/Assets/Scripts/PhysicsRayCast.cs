using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRayCast : MonoBehaviour
{  
    public GameObject PlayerPint;
    private GameObject FullPlayerPint;
    private GameObject CustomerBeer;
    private GameObject BeerFlow;
    private GameObject PourPint;

    private GameObject PlayerWineGlass;
    private GameObject FullPlayerWineGlassRed;
    private GameObject FullPlayerWineGlassWhite;
    private GameObject RedWine;
    private GameObject WhiteWine;
    private GameObject PouringRed;
    private GameObject PouringWhite;
    private GameObject RedWineLiquid;
    private GameObject WhiteWineLiquid;
    private GameObject PourWineGlass;
    // Start is called before the first frame update
    void Start()
    {
        //Beer
        PlayerPint = GameObject.Find("PlayerPintGlass");
        FullPlayerPint = GameObject.Find("FullPlayerPintGlass");
        CustomerBeer = GameObject.Find("CustomerBeerFull");
        BeerFlow = GameObject.Find("flowingBeer");
        PourPint = GameObject.Find("PlaceholderPint");

        //Wine
        PlayerWineGlass = GameObject.Find("PlayerWineGlass");
        FullPlayerWineGlassRed = GameObject.Find("FullPlayerWineGlassRed");
        FullPlayerWineGlassWhite = GameObject.Find("FullPlayerWineGlassWhite");
        RedWine = GameObject.Find("Red Wine");
        WhiteWine = GameObject.Find("White Wine");
        PouringRed = GameObject.Find("PouringRed");
        PouringWhite = GameObject.Find("PouringWhite");
        RedWineLiquid = GameObject.Find("RedWineLiquidPouring");
        WhiteWineLiquid = GameObject.Find("WhiteWineLiquidPouring");
        PourWineGlass = GameObject.Find("PlaceholderWineGlass");

        if (PlayerPint != null) PlayerPint.SetActive(false);
        if (FullPlayerPint != null) FullPlayerPint.SetActive(false);
        if (CustomerBeer != null) CustomerBeer.SetActive(false);
        if (BeerFlow != null) BeerFlow.SetActive(false);
        if (PourPint != null) PourPint.SetActive(false);

        if (PlayerWineGlass != null) PlayerWineGlass.SetActive(false);
        if (FullPlayerWineGlassRed != null) FullPlayerWineGlassRed.SetActive(false);
        if (FullPlayerWineGlassWhite != null) FullPlayerWineGlassWhite.SetActive(false);
        if (RedWine != null) RedWine.SetActive(true);
        if (WhiteWine != null) WhiteWine.SetActive(true);
        if (PouringRed != null) PouringRed.SetActive(false);
        if (PouringWhite != null) PouringWhite.SetActive(false);
        if (RedWineLiquid != null) RedWineLiquid.SetActive(false);
        if (WhiteWineLiquid != null) WhiteWineLiquid.SetActive(false);
        if (PourWineGlass != null) PourWineGlass.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
            {
                Debug.Log(hit.collider.name);

                // BEER
                if (hit.collider.name == "PickupPint")
                {
                    Debug.Log("Picked up pint");
                    HandleBeer();
                }
                if (hit.collider.name == "PourPint" && PlayerPint.activeSelf)
                {
                    Debug.Log("POURING");
                    PourBeer();
                }

                // WINE
                if (hit.collider.name == "PickupWine")
                {
                    Debug.Log("Picked up wine glass");
                    HandleWine();
                }

                if (hit.collider.name == "Red Wine" && PlayerWineGlass.activeSelf)
                {
                    Debug.Log("Pouring red");
                    PourRedWine();
                }

                if (hit.collider.name == "White Wine" && PlayerWineGlass.activeSelf)
                {
                    Debug.Log("Pouring white");
                    PourWhiteWine();
                }


                //CUSTOMERS
                if (hit.collider.name == "StateTest")
                {
                    HandleCustomer();
                }
            }
            // Debug.DrawLine(transform.position, transform.forward * 10f);
        }
    }

    //Beer Functions
    private void HandleBeer()
    {
        if (PlayerPint != null && !PlayerPint.activeSelf)
        {
            Debug.Log("PICKED");
            PlayerPint.SetActive(true);
            
        }
    }

    private void PourBeer()
    {
        if (PlayerPint != null && PlayerPint.activeSelf)
        {
            PourPint.SetActive(true);
            PlayerPint.SetActive(false);
            BeerFlow.SetActive(true);
            Invoke(nameof(CompleteBeerPour), 4f);

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
        if (PlayerWineGlass != null && !PlayerWineGlass.activeSelf)
        {
            Debug.Log("PICKED");
            PlayerWineGlass.SetActive(true);
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
            Invoke(nameof(CompleteRedWinePour), 4f);
        }
    }

    void CompleteRedWinePour()
    {
        PourWineGlass.SetActive(false);
        FullPlayerWineGlassRed.SetActive(true);
        PouringRed.SetActive(false);
        RedWineLiquid.SetActive(false);
    }

    private void PourWhiteWine()
    {
        if (PlayerWineGlass != null && PlayerWineGlass.activeSelf)
        {
            PourWineGlass.SetActive(true);
            PlayerWineGlass.SetActive(false);
            PouringWhite.SetActive(true);
            WhiteWineLiquid.SetActive(true);
            Invoke(nameof(CompleteWhiteWinePour), 4f);
        }
    }

    void CompleteWhiteWinePour()
    {
        PourWineGlass.SetActive(false);
        FullPlayerWineGlassWhite.SetActive(true);
        PouringWhite.SetActive(false);
        WhiteWineLiquid.SetActive(false);
    }
    private void HandleCustomer()
    {
        if (CustomerBeer != null && FullPlayerPint.activeSelf)
        {
            Debug.Log("HERES YOUR BEEEEER");
            CustomerBeer.SetActive(true);
            FullPlayerPint.SetActive(false);
        }
    }
}

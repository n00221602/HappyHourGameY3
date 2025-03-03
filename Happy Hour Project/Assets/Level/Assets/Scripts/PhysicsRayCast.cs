using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRayCast : MonoBehaviour
{  
    public GameObject PlayerPint;
    private GameObject FullPlayerPint;
    private GameObject CustomerBeer;
    private GameObject BeerFlow;
    private GameObject PourCup;
    // Start is called before the first frame update
    void Start()
    {
        //Beer
        PlayerPint = GameObject.Find("PlayerPintGlass");
        FullPlayerPint = GameObject.Find("FullPlayerPintGlass");
        CustomerBeer = GameObject.Find("CustomerBeerFull");
        BeerFlow = GameObject.Find("flowingBeer");
        PourCup = GameObject.Find("PlaceholderPint");

        if (PlayerPint != null) PlayerPint.SetActive(false);
        if (FullPlayerPint != null) FullPlayerPint.SetActive(false);
        if (CustomerBeer != null) CustomerBeer.SetActive(false);
        if (BeerFlow != null) BeerFlow.SetActive(false);
        if (PourCup != null) PourCup.SetActive(false);


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
                    Debug.Log("HITTTTT");
                    HandleBeer();
                }
                if (hit.collider.name == "PourPint")
                {
                    Debug.Log("POURING");
                    PourBeer();
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
            PourCup.SetActive(true);
            PlayerPint.SetActive(false);
            BeerFlow.SetActive(true);
            Invoke(nameof(CompletePour), 4f);

        }  
    }
    void CompletePour()
    {
        PourCup.SetActive(false);
        PlayerPint.SetActive(false);
        FullPlayerPint.SetActive(true);
        BeerFlow.SetActive(false);
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

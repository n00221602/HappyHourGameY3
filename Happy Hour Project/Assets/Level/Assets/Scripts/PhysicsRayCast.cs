using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRayCast : MonoBehaviour
{
    
    private GameObject CustomerBeer;

    private GameObject playerBeer;

    // Start is called before the first frame update
    void Start()
    {
        CustomerBeer = GameObject.Find("CustomerBeerFull");
        playerBeer = GameObject.Find("FullPlayerPintGlass");
        if (CustomerBeer != null)
        {
            CustomerBeer.SetActive(false);
        }
        else
        {
            Debug.LogError("Object not found");
        }
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
                if (hit.collider.name == "StateTest")
                {
                    Debug.Log("HERES YOUR BEEEEER");
                    CustomerBeer.SetActive(true);
                    playerBeer.SetActive(false);
                }
            }
            //Debug.DrawLine(transform.position, transform.forward * 10f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PintPickup : MonoBehaviour
{
    //gives the effect of picking up the glass, really sets the glass that is already in the hand but invisible as visible
public GameObject PintOnPlayer;
    void Start()
    {
        PintOnPlayer.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetMouseButtonDown(1))
            {
                
                PintOnPlayer.SetActive(true);
            }
        }
    }


}

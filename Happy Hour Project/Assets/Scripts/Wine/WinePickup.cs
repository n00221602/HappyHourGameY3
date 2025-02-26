using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinePickup : MonoBehaviour
{
  public GameObject FullRedWineOnPlayer;
  public GameObject FullWhiteWineOnPlayer;
  public GameObject RedWineBottle;
  public GameObject WhiteWineBottle;




public GameObject WineOnPlayer;
    void Start()
    {
        WineOnPlayer.SetActive(false);
        FullRedWineOnPlayer.SetActive(false);
        FullWhiteWineOnPlayer.SetActive(false);
        RedWineBottle.SetActive(true);
        WhiteWineBottle.SetActive(true);



    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetMouseButtonDown(1))
            {
                
                WineOnPlayer.SetActive(true);
            }
        }
    }


}
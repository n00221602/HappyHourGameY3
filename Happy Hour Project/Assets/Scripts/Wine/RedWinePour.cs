using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWinePour : MonoBehaviour
{
    public GameObject FlowingRedWine;
    public GameObject WineOnPlayer;
    public GameObject FullRedWineOnPlayer;
    public GameObject PlaceholderRedWineBottle;
    public GameObject PlaceholderWineGlass;
    public GameObject RedWineBottle;



    void Start()
    {
    this.enabled = true; 

        RedWineBottle.SetActive(true);
        FlowingRedWine.SetActive(false);
        WineOnPlayer.SetActive(false);
        FullRedWineOnPlayer.SetActive(false);
        PlaceholderRedWineBottle.SetActive(false);
        PlaceholderWineGlass.SetActive(false);


    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.CompareTag("Player")) && WineOnPlayer.activeSelf)  
        {
            if (Input.GetMouseButtonDown(0))
            {
                RedWineBottle.SetActive(false);
                WineOnPlayer.SetActive(false);
                PlaceholderWineGlass.SetActive(true);
                PlaceholderRedWineBottle.SetActive(true);
                FlowingRedWine.SetActive(true);
                Invoke(nameof(CompletePour), 3f); 
            }
        }
    }

    void CompletePour()
    {
        FlowingRedWine.SetActive(false);
        WineOnPlayer.SetActive(false);
        PlaceholderRedWineBottle.SetActive(false);
        PlaceholderWineGlass.SetActive(false);
        FullRedWineOnPlayer.SetActive(true);
        RedWineBottle.SetActive(true);

    }
}
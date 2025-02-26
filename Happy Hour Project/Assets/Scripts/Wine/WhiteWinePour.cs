using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteWinePour : MonoBehaviour
{
    public GameObject FlowingWhiteWine;
    public GameObject WineOnPlayer;
    public GameObject FullWhiteWineOnPlayer;
    public GameObject PlaceholderWhiteWineBottle;
    public GameObject PlaceholderWineGlass;
    public GameObject WhiteWineBottle;



    void Start()
    {
    this.enabled = true;  

        WhiteWineBottle.SetActive(true);
        FlowingWhiteWine.SetActive(false);
        WineOnPlayer.SetActive(false);
        FullWhiteWineOnPlayer.SetActive(false);
        PlaceholderWhiteWineBottle.SetActive(false);
        PlaceholderWineGlass.SetActive(false);


    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.CompareTag("Player")) && WineOnPlayer.activeSelf)  
        {
            if (Input.GetMouseButtonDown(0))
            {
                WhiteWineBottle.SetActive(false);
                WineOnPlayer.SetActive(false);
                PlaceholderWineGlass.SetActive(true);
                PlaceholderWhiteWineBottle.SetActive(true);
                FlowingWhiteWine.SetActive(true);
                Invoke(nameof(CompletePour), 3f); 
            }
        }
    }

    void CompletePour()
    {
        FlowingWhiteWine.SetActive(false);
        WineOnPlayer.SetActive(false);
        PlaceholderWhiteWineBottle.SetActive(false);
        PlaceholderWineGlass.SetActive(false);
        FullWhiteWineOnPlayer.SetActive(true);
        WhiteWineBottle.SetActive(true);

    }
}
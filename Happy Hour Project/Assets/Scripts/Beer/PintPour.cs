using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PintPour : MonoBehaviour
{
    public GameObject FlowingBeer;
    public GameObject PintOnPlayer;
    public GameObject PlaceholderPint;
    public GameObject FullPintOnPlayer;
    public float pintCountdown = 4f;

    void Start()
    {
        FlowingBeer.SetActive(false);
        PintOnPlayer.SetActive(false);
        FullPintOnPlayer.SetActive(false);
        PlaceholderPint.SetActive(false);

    }

    public void IncreacePourSpeedPurchased(){
        pintCountdown /= 1.5f;
    }

    //this is the same logic as the beer pouring script
    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.CompareTag("Player")) && PintOnPlayer.activeSelf)  
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceholderPint.SetActive(true);
                PintOnPlayer.SetActive(false);
                FlowingBeer.SetActive(true);
                Invoke(nameof(CompletePour), pintCountdown); 
            }
        }
    }

    void CompletePour()
    {
        PlaceholderPint.SetActive(false);
        PintOnPlayer.SetActive(false);
        FullPintOnPlayer.SetActive(true);
        FlowingBeer.SetActive(false);
    }
}
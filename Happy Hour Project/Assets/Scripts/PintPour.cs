using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PintPour : MonoBehaviour
{
    public GameObject FlowingBeer;
    public GameObject PintOnPlayer;
    public GameObject PlaceholderPint;
    public GameObject FullPintOnPlayer;

    void Start()
    {
        FlowingBeer.SetActive(false);
        PintOnPlayer.SetActive(false);
        FullPintOnPlayer.SetActive(false);
        PlaceholderPint.SetActive(false);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))  
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceholderPint.SetActive(true);
                PintOnPlayer.SetActive(false);
                FlowingBeer.SetActive(true);
                Invoke(nameof(CompletePour), 5f); 
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
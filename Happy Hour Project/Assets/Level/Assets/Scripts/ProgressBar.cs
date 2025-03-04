using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private float progressTime;
    public float progressInterval = 4f;
    public Image mask;
    private GameObject beerProgressBar;


    // Start is called before the first frame update
    void Start()
    {
        beerProgressBar = GameObject.Find("BeerProgressBar");
        beerProgressBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
            FillBeerProgressBar();
    }

    public void FillBeerProgressBar()
    {
        beerProgressBar.SetActive(true);
        progressTime += Time.deltaTime;
        float fillAmount = progressTime / progressInterval;
        mask.fillAmount = fillAmount;

        if (progressTime >= progressInterval)
        {
            progressTime = 0f;
            mask.fillAmount = 0f;
            beerProgressBar.SetActive(false);
        }

    }
}

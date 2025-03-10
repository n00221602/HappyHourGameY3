using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerTimer : MonoBehaviour
{
    private float maximum = 30f * 2;
    private float customerTime;
    private GameObject customerTimer;
    public Image mask;

    //public CustomerNPC customerNPC;

    void Start()
    {
        customerTimer = this.gameObject;
        customerTimer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
    }

    public void StartTimer()
    {
    
        customerTimer.SetActive(true);
        customerTime += Time.deltaTime;
        float fillAmount = customerTime / maximum;
        mask.fillAmount = fillAmount;

    }
}

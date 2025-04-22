using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    //private float realSeconds = 12f;

    private Transform clockHand;
    private float day;

    private CustomerSpawner customerSpawner;

    private void Start()
    {
        clockHand = transform.Find("ClockHand");
        customerSpawner = FindObjectOfType<CustomerSpawner>();
    }

    public void StartClock()
    {

        
        day += Time.deltaTime / customerSpawner.currentDayTimer;
        Debug.Log(day);

        //float dayNormal = day % 1f;

        clockHand.eulerAngles = new Vector3(0, 0, -day * 360f);

        if (day >= 1f)
        {
            // Reset the day to 0 when it reaches 1
            StopClock();
        }
    }

    void StopClock()
    {
        // Stop the clock hand rotation
        clockHand.eulerAngles = new Vector3(0, 0, 0);
    }


}

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

    //Starts the clock hand rotation in the clock UI
    public void StartClock()
    {
        day += Time.deltaTime / customerSpawner.currentDaySeconds;
       // float dayNormal = day % 1f;

        clockHand.eulerAngles = new Vector3(0, 0, -day * 360f);

        if (day >= 1f)
        {
            StopClock();
        }
    }

    // Reset the clock after it completes a full rotation
    void StopClock()
    {
        // Stop the clock hand rotation
        clockHand.eulerAngles = new Vector3(0, 0, 0);
    }


}

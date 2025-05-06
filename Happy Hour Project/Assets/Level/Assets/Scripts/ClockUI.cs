using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
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

        clockHand.eulerAngles = new Vector3(0, 0, -day * 360f);

        if (day >= 1f)
        {
            StopClock();
        }
    }

    //Resets the clock after it completes a full rotation
    void StopClock()
    {
        clockHand.eulerAngles = new Vector3(0, 0, 0);
        day = 0f;
    }


}

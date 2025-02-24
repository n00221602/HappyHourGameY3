using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    float elapsedTime = 10f;
    bool timerRunning = false;

    public GameObject timecube;
    void Start()
    {
        timecube.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !timerRunning) // Ensure the timer doesn't start again if it's already running
        {
            timecube.SetActive(false);
            timerRunning = true; // Start the timer
        }

        if (timerRunning)
        {
            StartTimer();
        }

    }

    void StartTimer()
    {
        
        if (elapsedTime > 0f)
        {
            elapsedTime -= Time.deltaTime; 
            int seconds = Mathf.FloorToInt(elapsedTime % 60f); 
            Debug.Log("Time: " + seconds); 
        }
        else
        {
            NoTimer();
        }
    }
    void NoTimer()
    {
        Debug.Log("Timer ended");
        timecube.SetActive(true);
        elapsedTime = 10f;
        timerRunning = false;
    }
}

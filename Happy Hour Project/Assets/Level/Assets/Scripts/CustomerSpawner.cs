using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customers;
    public GameObject timecube;


    public float currentDay = 0f;

    //Day timer set in inspector
    public float currentDaySeconds;
    public float currentDayTimer;

    private float spawnTime = 0f;
    private float spawnRate;

    public bool timerRunning = false;

    private ClockUI clockUI;

    private void Start()
    {
        clockUI = FindObjectOfType<ClockUI>();
        currentDaySeconds = 140f;
        currentDayTimer = currentDaySeconds;
    }
    
    void Update()
    {
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
        if (customers.Length >= 6)
        {
            spawnTime = 0f;
            Debug.Log("Too many customers!");

        }

        //Starts the timer on input. The bool is used so that the timer only starts once
        //if (Input.GetKeyDown(KeyCode.E) && !timerRunning)
        //{
        //    timecube.SetActive(false);
        //    timerRunning = true;
        //}

        if (timerRunning)
        {
            StartTimer();
            clockUI.StartClock();
        }

        

    }


    void StartTimer()
    {

        //Each statement defines the current day. Each day has a differnent spawn rate. It begins at day 0 which is the tutorial day.
        if (currentDay == 0f) { spawnRate = 12f; }

        if (currentDay == 1f) { spawnRate = 12f; }

        if (currentDay == 2f) { spawnRate = 12f; }

        if (currentDay == 3) { spawnRate = 12f; }

        if (currentDay == 4) { spawnRate = 12f; }

        if (currentDay == 5) { spawnRate = 10f; }

        if (currentDay == 6) { spawnRate = 10f; }

        if (currentDay == 7) { spawnRate = 10f; }

        if (currentDay == 8) { spawnRate = 10f; }

        if (currentDay == 9) { spawnRate = 8f; }

        if (currentDay == 10f) { spawnRate = 8f; }

        spawnTime += Time.deltaTime;

        // Randomly spawns a customer from the customer index array once the interval is reached
        if (spawnTime >= spawnRate)
        {
            spawnTime = 0f;
            int randomIndex = Random.Range(0, customers.Length);
            Vector3 spawnPosition = new Vector3(24, 0, 51);
            Instantiate(customers[randomIndex], spawnPosition, Quaternion.identity);

            Debug.Log("Customer Spawned!");
        }

        if (currentDayTimer >= 0f)
        {
            currentDayTimer -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(currentDayTimer);
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
        Debug.Log("current day: " + currentDay);
        //timecube.SetActive(true);
        currentDayTimer = currentDaySeconds;
        timerRunning = false;
    }
}

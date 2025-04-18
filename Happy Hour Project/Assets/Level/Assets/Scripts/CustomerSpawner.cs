using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customers;
    public GameObject timecube;

    

    float currentDayTimer = 140f;

    private float spawnTime = 0f;
    private float spawnLimit = 10f;

    public bool timerRunning = false;

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
        }

        //if (currentDayTimer >= spawnLimit)
        //{
        //    // Resets the timer to 0 seconds
        //    currentDayTimer = 0f;

        //    // Spawns a customer
        //    SpawnCustomer();
        //}

        //if (Input.GetKeyDown(KeyCode.O) && customers.Length < 6)
        //{
        //    SpawnCustomer();
        //}

    }


    void StartTimer()
    {
        spawnTime += Time.deltaTime;

        // Randomly spawns a customer from the customer index array once the interval is reached
        if (spawnTime >= spawnLimit)
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
        timecube.SetActive(true);
        currentDayTimer = 10f;
        timerRunning = false;
    }
}

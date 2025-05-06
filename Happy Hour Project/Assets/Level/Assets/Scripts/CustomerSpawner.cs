using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customers;
    public GameObject OpenSign;


    public float currentDay = 0f;

    //Day timer set in inspector
    public float currentDaySeconds;
    public float currentDayTimer;

    private float spawnTime = 5f;
    private float spawnRate;

    public bool timerRunning = false;

    private ClockUI clockUI;
    private Purchasing purchases;
    private bool popularityUpgraded = false;

    public DayUI dayUI;

    private void Start()
    {
        clockUI = FindObjectOfType<ClockUI>();
        purchases = FindObjectOfType<Purchasing>();
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

        if (timerRunning)
        {
            StartTimer();
            clockUI.StartClock();
        }
    }


    void StartTimer()
    {

        //Each statement defines the current day. Each day has a differnent spawn rate.
        if (currentDay == 1f) { spawnRate = 11f; }

        if (currentDay == 2f) { spawnRate = 11f; }

        if (currentDay == 3) { spawnRate = 10f; }

        if (currentDay == 4) { spawnRate = 9.5f; }

        if (currentDay == 5) { spawnRate = 9f; }

        if (currentDay == 6) { spawnRate = 9f; }

        if (currentDay == 7) { spawnRate = 8.5f; }

        if (currentDay == 8) { spawnRate = 8f; }

        if (currentDay == 9) { spawnRate = 8f; }

        if (currentDay == 10f) { spawnRate = 7.5f; }


        if (popularityUpgraded)
        {
            spawnRate *= 0.5f;
            Debug.Log("Spawnrate halved");
        }
        


        spawnTime += Time.deltaTime;

        //Randomly spawns a customer from the customer index array once the interval is reached
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
        OpenSign.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        currentDayTimer = currentDaySeconds;
        timerRunning = false;
        dayUI.transition = false;
    }

    void GainPassiveIncome() {
        if(currentDaySeconds == 0f && purchases.AirHockeyTable.activeSelf)
        {
            purchases.PassiveIncome();
        }
    }

    public void PopularityPurchased() {
        popularityUpgraded = true;
        Debug.Log("Popularity Purchased!!");
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customers;
    private float elapsedTime = 0f; // Start from 0
    private float spawnInterval = 20f; // Time between spawns

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= spawnInterval)
        {
            // Resets the timer to 0 seconds
            elapsedTime = 0f;

            // Spawns a customer
            SpawnCustomer();
        }

        

        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
        if (customers.Length >= 6)
        {
            elapsedTime = 0f;
            Debug.Log("Too many customers!");

        }
         else if (elapsedTime >= spawnInterval)
        {
            // Resets the timer to 0 seconds
            elapsedTime = 0f;

            // Spawns a customer
            SpawnCustomer();
        }

        if (Input.GetKeyDown(KeyCode.O) && customers.Length < 6)
        {
            SpawnCustomer();
        }

    }

    private void SpawnCustomer()
    {
        // Randomly spawns a customer from the customer index array
        int randomIndex = Random.Range(0, customers.Length);
        Vector3 spawnPosition = new Vector3(24, 0, 51); // Sets the XYZ spawn position
        Instantiate(customers[randomIndex], spawnPosition, Quaternion.identity);

        Debug.Log("Customer Spawned!");
    }
}

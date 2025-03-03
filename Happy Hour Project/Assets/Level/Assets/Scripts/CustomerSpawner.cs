using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customers;
    private float elapsedTime = 0f; // Start from 0
    private float spawnInterval = 4000f; // Time between spawns (4 seconds)

    void Update()
    {
        // Adds elapsed time by the time passed between frames
        elapsedTime += Time.deltaTime;

        // Check if 4 seconds have passed
        if (elapsedTime >= spawnInterval)
        {
            // Resets the timer to 0 seconds
            elapsedTime = 0f;

            // Randomly spawns a customer from the customer index
            int randomIndex = Random.Range(0, customers.Length);
            Vector3 spawnPosition = new Vector3(20, 1, 51); // Sets the XYZ spawn position
            Instantiate(customers[randomIndex], spawnPosition, Quaternion.identity);

            Debug.Log("Customer Spawned!");
        }
    }
}

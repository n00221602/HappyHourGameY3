using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customers;
    private float elapsedTime = 0f; // Start from 0
    private float spawnInterval = 4f; // Time between spawns (4 seconds)

    void Update()
    {
        // Increment elapsed time by the time passed between frames
        elapsedTime += Time.deltaTime;

        // Check if 4 seconds have passed
        if (elapsedTime >= spawnInterval)
        {
            // Reset the timer
            elapsedTime = 0f;

            // Spawn a customer
            int randomIndex = Random.Range(0, customers.Length);
            Vector3 spawnPosition = new Vector3(20, 1, 51); // Adjust spawn position as needed
            Instantiate(customers[randomIndex], spawnPosition, Quaternion.identity);

            Debug.Log("Customer Spawned!");
        }
    }
}

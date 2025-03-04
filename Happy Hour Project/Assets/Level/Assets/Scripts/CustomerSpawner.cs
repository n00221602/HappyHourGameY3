using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customers;
    private float elapsedTime = 0f; // Start from 0
    private float spawnInterval = 4000f; // Time between spawns

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

        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnCustomer();
        }
    }

    private void SpawnCustomer()
    {
        // Randomly spawns a customer from the customer index array
        int randomIndex = Random.Range(0, customers.Length);
        Vector3 spawnPosition = new Vector3(20, 1, 51); // Sets the XYZ spawn position
        Instantiate(customers[randomIndex], spawnPosition, Quaternion.identity);

        Debug.Log("Customer Spawned!");
    }
}

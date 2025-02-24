using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customers;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        { int randomIndex=Random.Range(0, customers.Length);
            Vector3 spawnPosition = new Vector3(20, 1, 51);

            Instantiate(customers[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}

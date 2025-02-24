using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRayCast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
            {
            Debug.Log(hit.collider.name);
            }
        
            //Debug.Log("Clicked on: " + hit.collider.name);
        
            //if (hit.collider.CompareTag("Timer")) 
            //{
            //    hit.collider.GetComponent<LevelTimer>().startTimer();
            //}

        Debug.DrawLine(transform.position, transform.forward * 10f);

        }
    }
}

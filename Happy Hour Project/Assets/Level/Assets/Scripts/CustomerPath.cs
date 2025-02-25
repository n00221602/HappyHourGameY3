using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerPath : MonoBehaviour
{
    Transform destination;

    NavMeshAgent agent;
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.Log("NavMesh componant not attached");
        }
        else
        {
            SetDestination();
        }
    }
    void Update()
    {
        
    }

    private void SetDestination()
    {
        if (destination == null)
        {
            destination = GameObject.Find("npcDestination").transform;
        }
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            agent.SetDestination(targetVector);
        }
    }
}

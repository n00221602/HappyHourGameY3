using UnityEngine;
using UnityEngine.AI;

public class CustomerNPC : MonoBehaviour
{
    public enum State { Moving, Waiting, Leaving }
    public State currentState;

    private NavMeshAgent agent;
    Transform destination;
    public Vector3 exitPosition;
    public float waitTime = 5f;
    private float waitTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Moving;  // Start in moving state
        MoveToCounter();
    }

    void Update()
    {
        // Handle state transitions based on current state
        switch (currentState)
        {
            case State.Moving:
                MoveToCounter();
                break;
            case State.Waiting:
                WaitAtCounter();
                break;
            case State.Leaving:
                LeaveCounter();
                break;
        }
    }

    void MoveToCounter()
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

        // If the NPC is close to the counter, switch to the waiting state
        if (Vector3.Distance(agent.transform.position, destination.transform.position) < 1f)
        {
            currentState = State.Waiting;
            waitTimer = 0f;  // Reset the waiting timer
        }
    }

    void WaitAtCounter()
    {
        // Increment the wait timer
        waitTimer += Time.deltaTime;

        // If the wait time is over, switch to leaving state
        if (waitTimer >= waitTime)
        {
            currentState = State.Leaving;
        }
    }

    void LeaveCounter()
    {
        // Start moving towards the exit
        agent.SetDestination(exitPosition);

        // Once the NPC has left the area, you can stop or reset the behavior
        if (Vector3.Distance(agent.transform.position, exitPosition) < 1f)
        {
            // The NPC has left the area. You can implement any cleanup or reset logic here.
            Debug.Log("Customer has left.");
            // Optionally, you can reset the state machine to restart the cycle or disable the NPC.
        }
    }
}
using UnityEngine;
using UnityEngine.AI;

public class CustomerNPC : MonoBehaviour
{
    public enum State { Moving, Waiting, Leaving }
    public State currentState;

    private NavMeshAgent agent;
    Transform destination;
    Transform exit;
    public float waitTime = 5f;
    private float waitTimer;

    private GameObject customerBeer;
    private GameObject playerBeer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Moving;  // Start in moving state
        MoveToCounter();

        // Find the CustomerBeer GameObject
        customerBeer = GameObject.Find("CustomerBeerFull");
        playerBeer = GameObject.Find("FullPlayerPintGlass");
        
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
        // Assigns destination to the position of the npcDestination object
        if (destination == null)
        {
            destination = GameObject.Find("npcDestination").transform;
            if (destination == null)
            {
                Debug.LogError("npcDestination GameObject not found!");
                return;
            }
        }

        // If destination is assigned, move towards the destination position
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            agent.SetDestination(targetVector);
        }

        // If the NPC reaches the counter, switch to the waiting state
        if (Vector3.Distance(agent.transform.position, destination.transform.position) < 1f)
        {
            currentState = State.Waiting;
            waitTimer = 0f;  // Sets the waiting timer to 0
        }
    }

    void WaitAtCounter()
    {
        // Increment the wait timer
        waitTimer += Time.deltaTime;

        // Check if CustomerBeer is active
        if (customerBeer.activeSelf)
        {
            Debug.Log("YEAHHHHHHHHHHHHH");
            currentState = State.Leaving;
        }

        // If the wait time hits 0, switch to leaving state
        if (waitTimer >= waitTime)
        {
            currentState = State.Leaving;
        }
    }

    void LeaveCounter()
    {
        // Assigns exit to the position of the npcExit object
        if (exit == null)
        {
            exit = GameObject.Find("npcExit").transform;
        }

        // If exit is assigned, move towards the exit position
        if (exit != null)
        {
            Vector3 targetVector = exit.transform.position;
            agent.SetDestination(targetVector);
        }

        // if the exit position is reached, the customer is despawned
        if (Vector3.Distance(agent.transform.position, exit.transform.position) < 1f)
        {
            Debug.Log("BYEEEEE");
            Destroy(gameObject);
        }
    }
}

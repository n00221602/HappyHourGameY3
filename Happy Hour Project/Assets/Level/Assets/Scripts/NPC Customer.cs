using UnityEngine;
using UnityEngine.AI;

public class CustomerNPC : MonoBehaviour
{
    public enum State { Moving, Waiting, Leaving }
    public State currentState;

    private NavMeshAgent agent;
    Transform destination;
    Transform exit;
    private float waitTime = 30f;
    private float waitTimer;

    public GameObject CustomerBeer;
    public GameObject CustomerRedWine;
    public GameObject CustomerWhiteWine;

    public GameObject allIcons;
    public GameObject iconBeer;
    public GameObject iconRedWine;
    public GameObject iconWhiteWine;

    private string[] drinks = { "Beer", "RedWine", "WhiteWine" };
    private string selectedDrink;

    private string[] destinations = { "npcDestination 1", "npcDestination 2", "npcDestination 3", "npcDestination 4", "npcDestination 5"};
    //private string selectedDestination;
   

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Moving;  // Start in moving state
        MoveToCounter();

        // Customer Drink Objects
        CustomerBeer = transform.Find("CustomerBeerFull").gameObject;
        CustomerRedWine = transform.Find("CustomerRedWineFull").gameObject;
        CustomerWhiteWine = transform.Find("CustomerWhiteWineFull").gameObject;

        // Icon Objects
        allIcons = transform.Find("DrinkIcons").gameObject;
        iconBeer = transform.Find("DrinkIcons/BeerIcon").gameObject;
        iconRedWine = transform.Find("DrinkIcons/RedWineIcon").gameObject;
        iconWhiteWine = transform.Find("DrinkIcons/WhiteWineIcon").gameObject;

        if (iconBeer != null) iconBeer.SetActive(false);
        if (iconRedWine != null) iconRedWine.SetActive(false);
        if (iconWhiteWine != null) iconWhiteWine.SetActive(false);
        if (CustomerBeer != null) CustomerBeer.SetActive(false);
        if (CustomerRedWine != null) CustomerRedWine.SetActive(false);
        if (CustomerWhiteWine != null) CustomerWhiteWine.SetActive(false);

        // Decide the drink once at the start
        DecideDrink();
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
                OrderDrink();
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
            foreach (string dest in destinations)
            {
                GameObject destObject = GameObject.Find(dest);

                if (destObject != null)
                {
                    Vector3 destPosition = destObject.transform.position;

                    // Check if any customer is already at this destination
                    GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
                    Debug.Log("Number of customers found: " + customers.Length);

                    bool isTaken = false;
                    foreach (GameObject customer in customers)
                    {
                        if (Vector3.Distance(customer.transform.position, destPosition) < 1.2f)
                        {
                            isTaken = true;
                            break;
                        }
                    }

                    if (!isTaken)
                    {
                        destination = destObject.transform;
                        break;
                    }
                }
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


    void DecideDrink()
    {
        // Randomly select a drink from the array
        int randomIndex = Random.Range(0, drinks.Length);
        selectedDrink = drinks[randomIndex];

    }

    void OrderDrink()
    {
        if (selectedDrink == "Beer")
        {
            iconBeer.SetActive(true);
        }
        if (selectedDrink == "RedWine")
        {
            iconRedWine.SetActive(true);
        }
        if (selectedDrink == "WhiteWine")
        {
            iconWhiteWine.SetActive(true);
        }

        Debug.Log("Gimme some " + selectedDrink);
        // Increment the wait timer
        waitTimer += Time.deltaTime;

        // Checks if a drink is handed to the customer, then switch to leaving state
        if (CustomerBeer.activeSelf)
        {
            iconBeer.SetActive(false);
            currentState = State.Leaving;
        }

        if (CustomerRedWine.activeSelf)
        {
            iconRedWine.SetActive(false);
            currentState = State.Leaving;
        }

        if (CustomerWhiteWine.activeSelf)
        {
            iconWhiteWine.SetActive(false);
            currentState = State.Leaving;
        }

        // If the wait time hits 0, switch to leaving state
        if (waitTimer >= waitTime)
        {
            //allIcons.SetActive(false);
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


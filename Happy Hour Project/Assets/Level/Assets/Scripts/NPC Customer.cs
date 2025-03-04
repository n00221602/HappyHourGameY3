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

    private GameObject destinationTrigger1;
    private GameObject destinationTrigger2;
    private GameObject destinationTrigger3;
    private GameObject destinationTrigger4;
    private GameObject destinationTrigger5;

    private string[] drinks = { "Beer", "RedWine", "WhiteWine" };
    private string selectedDrink;

    void Start()
    {
        //Destinations
        //destinationTrigger1 = GameObject.Find("destinationTrigger1");
        //destinationTrigger2 = GameObject.Find("destinationTrigger2");
        //destinationTrigger3 = GameObject.Find("destinationTrigger3");

        //destinationTrigger1.SetActive(false);
        //destinationTrigger2.SetActive(false);
        //destinationTrigger3.SetActive(false);


        agent = GetComponent<NavMeshAgent>();
        currentState = State.Moving;  // Start in moving state
        MoveToCounter();

        // Customer Drink Objects
        CustomerBeer = GameObject.Find("CustomerBeerFull");
        CustomerRedWine = GameObject.Find("CustomerRedWineFull");
        CustomerWhiteWine = GameObject.Find("CustomerWhiteWineFull");

        // Icon Objects
        allIcons = GameObject.Find("DrinkIcons");
        iconBeer = GameObject.Find("BeerIcon");
        iconRedWine = GameObject.Find("RedWineIcon");
        iconWhiteWine = GameObject.Find("WhiteWineIcon");

        //if (allIcons != null) allIcons.SetActive(false);
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
            destination = GameObject.Find("npcDestination 1").transform;
            
        }
        //if (destinationTrigger1.activeSelf) {
        //    destination = GameObject.Find("npcDestination 2").transform;
        //    destinationTrigger2.SetActive(true);
        //}
        //if (destinationTrigger2.activeSelf)
        //{
        //    destination = GameObject.Find("npcDestination 3").transform;
        //    destinationTrigger3.SetActive(true);
        //}

        //if (Vector3.Distance(agent.transform.position, destination.transform.position) < 1f)
        //{
        //    destination = GameObject.Find("npcDestination 2").transform;
        //}

        // If destination is assigned, move towards the destination position
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            agent.SetDestination(targetVector);
        }

        // If the NPC reaches the counter, switch to the waiting state
        if (Vector3.Distance(agent.transform.position, destination.transform.position) < 1f)
        {
            //destinationTrigger1.SetActive(true);
            currentState = State.Waiting;
            waitTimer = 0f;  // Sets the waiting timer to 0
        }
    }

    void DecideDrink()
    {
        // Randomly select a drink from the array
        int randomIndex = Random.Range(0,2);
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


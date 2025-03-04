using UnityEngine;
using UnityEngine.AI;

public class CustomerNPC : MonoBehaviour
{
    public enum State { Moving, Waiting, Leaving }
    public State currentState;

    private NavMeshAgent agent;
    Transform destination;
    Transform exit;
<<<<<<< HEAD
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
=======
    private float waitTime = 10f;
    private float waitTimer;

    private GameObject customerBeer;
    private GameObject iconBeer;
>>>>>>> parent of 2d444de (tried money system(failed))

    private string[] drinks = { "Beer", "RedWine", "WhiteWine" };
    private string selectedDrink;

    void Start()
    {
<<<<<<< HEAD
        //Destinations
        //destinationTrigger1 = GameObject.Find("destinationTrigger1");
        //destinationTrigger2 = GameObject.Find("destinationTrigger2");
        //destinationTrigger3 = GameObject.Find("destinationTrigger3");

        //destinationTrigger1.SetActive(false);
        //destinationTrigger2.SetActive(false);
        //destinationTrigger3.SetActive(false);


=======
>>>>>>> parent of 2d444de (tried money system(failed))
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Moving;  // Start in moving state
        MoveToCounter();

<<<<<<< HEAD
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
=======
        // Find the CustomerBeer GameObject
        customerBeer = GameObject.Find("CustomerBeerFull");
        iconBeer = GameObject.Find("BeerUI");
        iconBeer.SetActive(false);
>>>>>>> parent of 2d444de (tried money system(failed))

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
<<<<<<< HEAD
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
=======
            destination = GameObject.Find("npcDestination").transform;
            if (destination == null)
            {
                Debug.LogError("npcDestination GameObject not found!");
                return;
            }
        }
>>>>>>> parent of 2d444de (tried money system(failed))

        // If destination is assigned, move towards the destination position
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            agent.SetDestination(targetVector);
        }

        // If the NPC reaches the counter, switch to the waiting state
        if (Vector3.Distance(agent.transform.position, destination.transform.position) < 1f)
        {
<<<<<<< HEAD
            //destinationTrigger1.SetActive(true);
=======
>>>>>>> parent of 2d444de (tried money system(failed))
            currentState = State.Waiting;
            waitTimer = 0f;  // Sets the waiting timer to 0
        }
    }

    void DecideDrink()
    {
        // Randomly select a drink from the array
<<<<<<< HEAD
        int randomIndex = Random.Range(0,2);
=======
        int randomIndex = Random.Range(0,0);
>>>>>>> parent of 2d444de (tried money system(failed))
        selectedDrink = drinks[randomIndex];
        
    }

    void OrderDrink()
    {
<<<<<<< HEAD
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
=======
        iconBeer.SetActive(true);
        Debug.Log("I WANT " + selectedDrink);
        // Increment the wait timer
        waitTimer += Time.deltaTime;

        // Check if CustomerBeer is active
        if (customerBeer.activeSelf)
        {
            iconBeer.SetActive(false);
            Debug.Log("YEAHHHHHHHHHHHHH");
>>>>>>> parent of 2d444de (tried money system(failed))
            currentState = State.Leaving;
        }

        // If the wait time hits 0, switch to leaving state
        if (waitTimer >= waitTime)
        {
<<<<<<< HEAD
            //allIcons.SetActive(false);
=======
            iconBeer.SetActive(false);
>>>>>>> parent of 2d444de (tried money system(failed))
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


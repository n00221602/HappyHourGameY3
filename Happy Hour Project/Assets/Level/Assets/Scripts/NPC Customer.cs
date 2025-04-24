using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class CustomerNPC : MonoBehaviour
{
    public enum State { Moving, Waiting, Searching, Leaving, Drinking, MoveToGame, Neutral, FightEvent, SpillEvent, FinishedEvent }
    public State currentState;
    public float eventTime = 0f;

    private NavMeshAgent agent;
    private Animator animator;
    Transform bar;
    Transform table;
    Transform game;
    Transform exit;
    Transform randomVictim;
    public float waitTime = 30f;
    private float waitTimer;

    public GameObject CustomerBeer;
    public GameObject CustomerRedWine;
    public GameObject CustomerWhiteWine;
    public GameObject CustomerCan;
    public GameObject CustomerBottleBeer;


    public GameObject allIcons;
    public GameObject iconBeer;
    public GameObject iconRedWine;
    public GameObject iconWhiteWine;
    public GameObject iconCan;
    public GameObject iconBottleBeer;

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    public GameObject Star4;
    public GameObject Star5;
    private bool reputationLost = false;

    public GameObject puddlePrefab;
    private GameObject nearestTable;
    private GameObject cleanDestinationsParent;

    private string[] drinks = { "Beer", "RedWine", "WhiteWine", "Can", "BottleBeer" };
    private string selectedDrink;

    private string[] barDestinations = { "BarDest1", "BarDest2", "BarDest3", "BarDest4", "BarDest5", "BarDest6" };
    private string[] cleanDestinations;
    private string[] dirtyDestinations = { };
    private string[] gameDestinations = { "GameDest1", "GameDest2", "GameDest3", "GameDest4", "GameDest5", "GameDest6", "GameDest7", "GameDest8", "GameDest9", "GameDest10" };

  

    public CustomerTimer customerTimer;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = State.Moving;  // Starts the customer in the Moving state
        MoveToCounter();

        // Customer Drink Objects
        CustomerBeer = transform.Find("CustomerBeerFull").gameObject;
        CustomerRedWine = transform.Find("CustomerRedWineFull").gameObject;
        CustomerWhiteWine = transform.Find("CustomerWhiteWineFull").gameObject;
        CustomerCan = transform.Find("CustomerCan").gameObject;
        CustomerBottleBeer = transform.Find("CustomerBottleBeer").gameObject;


        // Icon Objects
        allIcons = transform.Find("DrinkIcons").gameObject;
        iconBeer = transform.Find("DrinkIcons/BeerIcon").gameObject;
        iconRedWine = transform.Find("DrinkIcons/RedWineIcon").gameObject;
        iconWhiteWine = transform.Find("DrinkIcons/WhiteWineIcon").gameObject;
        iconCan = transform.Find("DrinkIcons/CanIcon").gameObject;
        iconBottleBeer = transform.Find("DrinkIcons/BottleBeerIcon").gameObject;

        // Reputation Objects
        Star1 = GameObject.Find("Player/PlayerUi/PlayerHealth/Star1").gameObject;
        Star2 = GameObject.Find("Player/PlayerUi/PlayerHealth/Star2").gameObject;
        Star3 = GameObject.Find("Player/PlayerUi/PlayerHealth/Star3").gameObject;
        Star4 = GameObject.Find("Player/PlayerUi/PlayerHealth/Star4").gameObject;
        Star5 = GameObject.Find("Player/PlayerUi/PlayerHealth/Star5").gameObject;

        if (iconBeer != null) iconBeer.SetActive(false);
        if (iconRedWine != null) iconRedWine.SetActive(false);
        if (iconWhiteWine != null) iconWhiteWine.SetActive(false);
        if (iconCan != null) iconCan.SetActive(false);
        if (iconBottleBeer != null) iconBottleBeer.SetActive(false);


        if (CustomerBeer != null) CustomerBeer.SetActive(false);
        if (CustomerRedWine != null) CustomerRedWine.SetActive(false);
        if (CustomerWhiteWine != null) CustomerWhiteWine.SetActive(false);
        if (CustomerCan != null) CustomerCan.SetActive(false);
        if (CustomerBottleBeer != null) CustomerBottleBeer.SetActive(false);


        // Decide the drink once at the start.
        DecideDrink();

        // Dynamically fill the cleanDestinations array
        cleanDestinationsParent = GameObject.Find("TableDestinations");
        cleanDestinations = new string[cleanDestinationsParent.transform.childCount];
        for (int i = 0; i < cleanDestinationsParent.transform.childCount; i++)
        {
            cleanDestinations[i] = cleanDestinationsParent.transform.GetChild(i).name;
        }


    }


    void Update()
    {
        // Update animator parameters based on NavMeshAgent's velocity
        if (animator != null && agent != null)
        {
            animator.SetBool("isMoving", agent.velocity.sqrMagnitude > 0);
        }

        // Handles the states of the customer.
        switch (currentState)
        {
            case State.Moving:
                MoveToCounter();
                break;
            case State.Waiting:
                OrderDrink();
                break;
            case State.Searching:
                FindCleanDestination();
                break;
            case State.MoveToGame:
                FindGame();
                break;
            case State.Drinking:
                PickEvent();
                break;
            //case State.Neutral:
            //    Neutral();
            //    break;
            case State.FightEvent:
                StartFight();
                break;
            case State.SpillEvent:
                Spill();
                break;
            case State.FinishedEvent:
                FinishedDrink();
                break;
            case State.Leaving:
                LeaveBar();
                break;
        }

    }

    //Runs in the start method. Randomly selects a drink for the customer to order.
    void DecideDrink()
    {
        // Randomly select a drink from the array
        int randomIndex = Random.Range(0, drinks.Length);
        selectedDrink = drinks[randomIndex];

    }

    //Runs during the Moving state. Moves the customer towards the bar counter.
    void MoveToCounter()
    {
        // Assigns destination to the position of the npcDestination object
        if (bar == null)
        {
            for (int i = 0; i < barDestinations.Length; i++)
            {
                // The initial destination is set to the current loop. This destination is not guaranteed to be the final destination
                Transform initialDestination = GameObject.Find(barDestinations[i]).transform;
                bool isTaken = false;

                // Creates a temporary array containing all the customers in the scene
                GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");

                // For each customer, check if the destination is taken
                foreach (GameObject customer in customers)
                {
                    var customerNPC = customer.GetComponent<CustomerNPC>();
                    if (customer != this.gameObject && customerNPC != null && customerNPC.bar == initialDestination)
                    {
                        
                        isTaken = true;
                        break;
                    }
                }

                // If the destination is not taken, assign it to the customer
                if (!isTaken)
                {
                    bar = initialDestination;
                    break;
                }
            }
        }

        // If destination is assigned, move towards the destination position
        if (bar != null)
        {
            Vector3 targetVector = bar.transform.position;
            agent.SetDestination(targetVector);
        }

        // If the customer reaches the counter, rotate it towards the counter and switch to the waiting state
        if (bar != null && Vector3.Distance(agent.transform.position, bar.transform.position) < 1f)
        {
            transform.Rotate(0, -90, 0);
            currentState = State.Waiting;
            waitTimer = 0f;  // Sets the waiting timer to 0
        }
    }


    //Runs during the Waiting state. The customer orders the drink and a timer is started.
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
        if (selectedDrink == "Can")
        {
            iconCan.SetActive(true);
        }
        if (selectedDrink == "BottleBeer")
        {
            iconBottleBeer.SetActive(true);
        }
        Debug.Log("Gimme some " + selectedDrink);
        // Increment the wait timer
        waitTimer += Time.deltaTime;
        customerTimer.StartTimer();

        // Checks if a drink is handed to the customer, then switch to drinking state
        if (CustomerBeer.activeSelf || CustomerRedWine.activeSelf || CustomerWhiteWine.activeSelf || CustomerCan.activeSelf || CustomerBottleBeer.activeSelf)
        {
            allIcons.SetActive(false);
            this.gameObject.tag = "Drinker"; // Changes the Customer tag to Drinker
            currentState = State.Searching;
            
        }

        // If the wait time hits 0, switch to leaving state
        if (waitTimer >= waitTime)
        {
            allIcons.SetActive(false);
            currentState = State.Leaving;
        }
    }

    //Runs during the Searching state. Makes the customer find a random table that is clean.
    void FindCleanDestination()
    {
        if (table == null)
        {

            //If all destinations are taken and have a Dirty tag, switch to the leaving state
            if (cleanDestinations.All(dest => GameObject.Find(dest).tag == "Dirty"))
            {
                Debug.Log("THERES NOWHERE TO SIT");
                currentState = State.Leaving;
            }

            //This goes through the cleanDestinations array and assigns a random destination to the customer    
            for (int i = 0; i < cleanDestinations.Length; i++)
            {
                int randomTable = Random.Range(0, cleanDestinations.Length); ;

                //The initial table is set to the current loop. This table is not guaranteed to be the final table
                Transform initialTable = GameObject.Find(cleanDestinations[randomTable]).transform;
                bool isTaken = false;

                // Creates a temporary array containing all the customers who have been served in the scene
                GameObject[] drinkers = GameObject.FindGameObjectsWithTag("Drinker");

                // For each drinker, check if the table is taken
                foreach (GameObject drinker in drinkers)
                {
                    // If the drinker is not the current customer and the drinker's table is the same as the initial table (.table is a Transform variable that is unique to each customer.)
                    if (drinker != this.gameObject && drinker.GetComponent<CustomerNPC>().table == initialTable)
                    {
                        isTaken = true;
                        break;
                    }
                }

                //If the table is not taken and is clean, assign it to the customer
                if (!isTaken && initialTable.tag == "Clean")
                {
                    table = initialTable;
                    break;
                }

            }
        }

        // If table is assigned, move towards the table position
        if (table != null)
        {
            Vector3 targetVector = table.transform.position;
            agent.SetDestination(targetVector);
        }

        // If the NPC reaches the destination, check for the nearest table asset and rotate towards it. Then switch to the waiting state
        if (table != null && Vector3.Distance(agent.transform.position, table.transform.position) < 0.1f)
        {
        


            //THIS SECTION IS FOR FINDING THE TABLE THAT THE CUSTOMER IS SITTING AT
            Vector3 assignedTable = table.position;
            GameObject[] tables = GameObject.FindGameObjectsWithTag("Table");

            //The minimum distance is first set to Mathf.Infinity, which is the largest possible value for a float.
            float minDistance = Mathf.Infinity;

            //Checks each distance between the table and the cuestomer's destination. Assigns nearestTable to the table with the shortest distance
            foreach (GameObject table in tables)
            {
                float distance = Vector3.Distance(assignedTable, table.transform.position);

                //Since minDistance is set to Mathf.Infinity, the first table will always be set to nearestTable. The loop contiues and compares the distance to the current nearestTable, assigning a new nearestTable if the distance is shorter.
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTable = table;
                }

            }

            //If the nearest table is found, rotate the customer to look at it
            if (nearestTable != null)
            {
                Vector3 nearestTablePosition = nearestTable.transform.position;
                transform.LookAt(nearestTablePosition);

                Vector3 offset = new Vector3(0f, 0f, 0f);
                string orderedDrink = selectedDrink;
                if(orderedDrink == "Beer")
                {
                    CustomerBeer.transform.position = nearestTablePosition + offset;
                }
                if (orderedDrink == "RedWine")
                {
                    CustomerRedWine.transform.position = nearestTablePosition + offset;
                }
                if (orderedDrink == "WhiteWine")
                {
                    CustomerWhiteWine.transform.position = nearestTablePosition + offset;
                }
                if (orderedDrink == "Can")
                {
                    CustomerCan.transform.position = nearestTablePosition + offset;
                }
                if (orderedDrink == "BottleBeer")
                {
                    CustomerBottleBeer.transform.position = nearestTablePosition + offset;
                }

            }

            currentState = State.Drinking;
        }
    }

    //Runs during the MoveToGame state. Finds a destination in the games area.
    void FindGame()
    {
        if (game == null)
        {
            for (int i = 0; i < gameDestinations.Length; i++)
            {
                int randomGame = Random.Range(0, gameDestinations.Length); ;

                //The initial game is set to the current loop. This game is not guaranteed to be the final game
                Transform initialGame = GameObject.Find(gameDestinations[randomGame]).transform;
                bool isTaken = false;

                // Creates a temporary array containing all the customers in the scene
                GameObject[] drinkers = GameObject.FindGameObjectsWithTag("Drinker");

                // For each drinker, check if the game is taken
                foreach (GameObject drinker in drinkers)
                {
                    if (drinker != this.gameObject && drinker.GetComponent<CustomerNPC>().game == initialGame)
                    {
                        isTaken = true;
                        break;
                    }
                }

                //If the game is not taken, assign it to the customer
                if (!isTaken)
                {
                    game = initialGame;
                    break;
                }
            }
        }

        // If game is assigned, move towards the game position
        if (game != null)
        {
            Vector3 targetVector = game.transform.position;
            agent.SetDestination(targetVector);
        }

        // If the NPC reaches the game, switch to the waiting state
        if (game != null && Vector3.Distance(agent.transform.position, game.transform.position) < 1f)
        {
            //currentState = State.Waiting;
            Debug.Log("Enjoy your drink!");
        }
    }

    //Runs during the Leaving state. Makes the customer leave the bar.
    void LeaveBar()
    {
        // Assigns exit to the position of the npcExit object
        if (exit == null)
        {
            exit = GameObject.Find("npcExit").transform;
        }

        // If exit is assigned, move towards the exit position
        if (exit != null)
        {
            bar = null;
            Vector3 targetVector = exit.transform.position;
            agent.SetDestination(targetVector);
        }

        // if the exit position is reached, the customer is despawned
        if (Vector3.Distance(agent.transform.position, exit.transform.position) < 1f)
        {
            Debug.Log("BYEEEEE");
            Destroy(gameObject);
        }
        if (currentState == State.Leaving && !reputationLost)
        {
         Debug.Log($"NPC {gameObject.name} is about to lose reputation.");
         BarReputation();
        }
    }


    //Runs during the Drinking state. Picks a random event while the customer is at its table.
    void PickEvent()
    {
        Debug.Log("DRINKING");
        eventTime += Time.deltaTime;
        if (eventTime >= 20f)
        {
            //eventTime = 0f;
            //int eventInterval = 100;
            //int randomChoice = Random.Range(1, eventInterval);

            //if (randomChoice <= 40) //40% chance for a customer to leave the bar
            //{
            //    currentState = State.Leaving;
            //}

            //if (randomChoice > 40 && randomChoice <= 65) //25% chance for a customer to leave the bar with a messy table
            //{
            //    currentState = State.FinishedEvent;
            //}
            //if (randomChoice > 65 && randomChoice <= 80) //15% chance for a customer to spill their drink
            //{
            //    currentState = State.SpillEvent;
            //}
            //else //20% chance for a customer to start a fight
            //{
            //    currentState = State.FightEvent;
            //}

            eventTime = 0f;
            currentState = State.FinishedEvent;
        }

    }

    //Runs during the Fighting state. This makes the customer choose a random victim and approach them.
    void StartFight()
    {
        Debug.Log("FIGHT!");
        this.gameObject.tag = "Fighter";

        GameObject[] drinkers = GameObject.FindGameObjectsWithTag("Drinker");

        for (int i = 0; i < drinkers.Length; i++)
        {
            int randomIndex = Random.Range(0, drinkers.Length);

            if (drinkers[randomIndex] != this.gameObject)
            {
                Debug.Log("VICTIM FOUND AT INDEX" + randomIndex);
                randomVictim = drinkers[randomIndex].transform;
                break;
            }
        }

        if (randomVictim != null)
        {
            Vector3 targetVector = randomVictim.position;
            agent.SetDestination(targetVector);
        } 
        else
        {
            Debug.Log("No victim found");
            this.gameObject.tag = "Drinker";
            currentState = State.Neutral;
            return;
        }
    }

    //Runs during the FinishedEvent state. This makes the customer leave the table with a mess and marks it as dirty.
    void FinishedDrink()
    {
        Debug.Log("Drink finished");

        //This uses the nearestTable variable determined in FindCleanDestination()
            if (nearestTable != null)
            {
                Vector3 nearestTablePosition = nearestTable.transform.position;
                Vector3 offset = new Vector3(0f, 1.057f, 0f);

                // Copys the puddlePrefab at the nearest table's position
                if (puddlePrefab != null)
                {
                    Instantiate(puddlePrefab, nearestTablePosition + offset, Quaternion.identity);
                    table.tag = "Dirty"; // Changes the table tag to Dirty
                }
                else
                {
                    Debug.LogWarning("Puddle prefab not found");
                }

            }
            else
            {
                Debug.LogWarning("No table found with the 'Table' tag");
            }
        currentState = State.Leaving;
    }

    //Runs during the SpillEvent state. This makes the customer spill their drink at their feet.
    void Spill()
    {
        Debug.Log("SPILL!");
        Vector3 offset = new Vector3(0f, -1.02f, 0f);
        Vector3 spillPosition = transform.position + offset;

        // Spawns puddle prefab at the "Table XYZ" position
        if (puddlePrefab != null)
        {

            Instantiate(puddlePrefab, spillPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Puddle prefab not assigned");
        }

        //If a table is near a spill, change the destination tag to Dirty
        for (int i = 0; i < cleanDestinations.Length; i++)
        {
            Transform destination = GameObject.Find(cleanDestinations[i]).transform;
            if (Vector3.Distance(destination.position, spillPosition) < 1)
            {
                //change the destination tag to Dirty
                destination.tag = "Dirty";


            }
        }
        currentState = State.Leaving;
    }

     void BarReputation()
    {
    Debug.Log($"NPC {gameObject.name} entering BarReputation. reputationLost={reputationLost}");

        if( !reputationLost && currentState == State.Leaving && !CustomerBeer.activeSelf && !CustomerRedWine.activeSelf  && !CustomerWhiteWine.activeSelf && !CustomerCan.activeSelf && !CustomerBottleBeer.activeSelf )
        {
            Debug.Log($"NPC {gameObject.name} is losing reputation.");

            if( Star1.activeSelf)
            {
                Star1.SetActive(false);
                Debug.Log("Star1 deactivated.");

            }

            else if(Star2.activeSelf )
            {
                Star2.SetActive(false);
                Debug.Log("Star2 deactivated.");

            }

            else if(Star3.activeSelf)
            {
                Star3.SetActive(false);
            }

             else if(Star4.activeSelf)
            {
                Star4.SetActive(false);
            }

             else if(Star5.activeSelf)
            {
                Star5.SetActive(false);
                Invoke(nameof(GameOverMover), 1f); 
            }
            reputationLost = true;
            Debug.Log($"NPC {gameObject.name} reputationLost set to true.");

        }
    }

         void GameOverMover()
    {
        SceneManager.LoadScene("Game Over"); 
    }

    //NEUTRAL STATE. MIGHT NOT BE NEEDED?
    //void Neutral()
    //{
    //    Debug.Log("Neutral");
    //    eventTime += Time.deltaTime;
    //    if (eventTime >= 20) 
    //    {
    //        eventTime = 0f;
    //        currentState = State.Drinking;
    //    }

    //}
}


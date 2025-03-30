using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CustomerNPC : MonoBehaviour
{
    public enum State { Moving, Waiting, Searching, Leaving, Drinking, MoveToGame, Neutral, FightEvent, SpillEvent }
    public State currentState;
    public float eventTime = 0f;

    private NavMeshAgent agent;
    Transform bar;
    Transform table;
    Transform game;
    Transform exit;
    public float waitTime = 30f;
    private float waitTimer;

    public GameObject CustomerBeer;
    public GameObject CustomerRedWine;
    public GameObject CustomerWhiteWine;

    public GameObject allIcons;
    public GameObject iconBeer;
    public GameObject iconRedWine;
    public GameObject iconWhiteWine;

    public GameObject puddlePrefab;

    private string[] drinks = { "Beer", "RedWine", "WhiteWine" };
    private string selectedDrink;

    private string[] barDestinations = { "BarDest1", "BarDest2", "BarDest3", "BarDest4", "BarDest5", "BarDest6" };
    private string[] cleanDestinations = { "TableDest1", "TableDest2", "TableDest3", "TableDest4", "TableDest5", "TableDest6", "TableDest7"};
    private string[] dirtyDestinations = { };
    private string[] gameDestinations = { "GameDest1", "GameDest2", "GameDest3", "GameDest4", "GameDest5", "GameDest6", "GameDest7", "GameDest8", "GameDest9", "GameDest10" };

  

    public CustomerTimer customerTimer;


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
            case State.Searching:
                FindCleanDestination();
                break;
            //case State.MovingToTable:
            //    FindTable();
            //    break;
            case State.MoveToGame:
                FindGame();
                break;
            case State.Drinking:
                PickEvent();
                break;
            case State.Neutral:
                Neutral();
                break;
            case State.FightEvent:
                StartFight();
                break;
            case State.SpillEvent:
                Spill();
                break;
            case State.Leaving:
                LeaveCounter();
                break;
        }

    }

    //Runs in the start method
    void DecideDrink()
    {
        // Randomly select a drink from the array
        int randomIndex = Random.Range(0, drinks.Length);
        selectedDrink = drinks[randomIndex];

    }

    //Runs during the Moving state
    void MoveToCounter()
    {
        // Assigns destination to the position of the npcDestination object
        if (bar == null)
        {
            for (int i = 0; i < barDestinations.Length; i++)
            {
                //The initial destination is set to the current loop. This destination is not guaranteed to be the final destination
                Transform initialDestination = GameObject.Find(barDestinations[i]).transform;
                bool isTaken = false;

                // Creates a temporary array containing all the customers in the scene
                GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");

                // For each customer, check if the destination is taken
                foreach (GameObject customer in customers)
                {
                    if (customer != this.gameObject && customer.GetComponent<CustomerNPC>().bar == initialDestination)
                    {
                        isTaken = true;
                        break;
                    }
                }

                //If the destination is not taken, assign it to the customer
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

        // If the NPC reaches the counter, switch to the waiting state
        if (Vector3.Distance(agent.transform.position, bar.transform.position) < 1f)
        {
            currentState = State.Waiting;
            waitTimer = 0f;  // Sets the waiting timer to 0
        }
    }

    //Runs during the Waiting state
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
        customerTimer.StartTimer();

        // Checks if a drink is handed to the customer, then switch to drinking state
        if (CustomerBeer.activeSelf || CustomerRedWine.activeSelf || CustomerWhiteWine.activeSelf)
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

    //Runs during the Searching state
    //void FindDestination()
    //{
    //    //// Randomly choose between table and game destinations
    //    //int randomChoice = Random.Range(0, 2); // 0 for table, 1 for game

    //    //if (randomChoice == 0)
    //    //{
    //    //    currentState = State.MovingToTable;
    //    //}
    //    //else
    //    //{
    //    //    currentState = State.MoveToGame;
    //    //}
    //}

    //Runs during the MovingToTable state
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

            for (int i = 0; i < cleanDestinations.Length; i++)
            {
                int randomTable = Random.Range(0, cleanDestinations.Length); ;

                //The initial table is set to the current loop. This table is not guaranteed to be the final table
                Transform initialTable = GameObject.Find(cleanDestinations[randomTable]).transform;
                bool isTaken = false;

                // Creates a temporary array containing all the customers in the scene
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

        // If the NPC reaches the table, switch to the waiting state
        if (table != null && Vector3.Distance(agent.transform.position, table.transform.position) < 1f)
        {
            currentState = State.Drinking;
            //Debug.Log("Enjoy your drink!");
        }
    }
    //Runs during the MoveToGame state
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

    //Runs during the Leaving state
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
    }

    //Runs during the Neutral state
    void PickEvent()
    {
        Debug.Log("DRINKING");
        eventTime += Time.deltaTime;
        if (eventTime >= 10f)
        {
            //int randomChoice = Random.Range(0, 3);

            //if (randomChoice == 0)
            //{
            //    currentState = State.FightEvent;
            //}
            //if (randomChoice == 1)
            //{
            //    currentState = State.SpillEvent;
            //}
            //else
            //{
            //    currentState = State.Neutral;
            //}

            currentState = State.SpillEvent;
        }

    }

    void Neutral()
    {
        Debug.Log("Neutral");
    }

    //Runs during the Fighting state
    void StartFight()
    {
        Debug.Log("FIGHT!");
    }

    void Spill()
    {
        Debug.Log("SPILL!");

        if (table != null)
        {
            //table refers to the destination that the customer was assigned to in FindCLeanDestination()
            Vector3 assignedTable = table.position;

            // Gathers all tables into an array.
            GameObject[] tables = GameObject.FindGameObjectsWithTag("Table");
            GameObject nearestTable = null;

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

                // If a table is near a spill, change the destination tag to Dirty
                //foreach (string cleanDest in cleanDestinations)
                //{
                //    Transform destination = GameObject.Find(cleanDest).transform;
                //    if (Vector3.Distance(destination.position, nearestTablePosition) < 1)
                //    {
                //        // Change the destination tag to Dirty
                //        destination.tag = "Dirty";
                //    }
                //}
            }
            else
            {
                Debug.LogWarning("No table found with the 'Table' tag");
            }
        }
        else
        {
            Debug.LogWarning("No table assigned to the customer");
        }

        currentState = State.Leaving;
    }



    //void Spill()
    //{
    //    Debug.Log("SPILL!");
    //    Vector3 offset = new Vector3(0f, -1.02f, 0f);
    //    Vector3 spillPosition = transform.position + offset;

    //    // Spawns puddle prefab at the "Table XYZ" position
    //    if (puddlePrefab != null)
    //    {

    //        Instantiate(puddlePrefab, spillPosition, Quaternion.identity);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Puddle prefab not assigned");
    //    }

    //    //If a table is near a spill, change the destination tag to Dirty
    //    for (int i = 0; i < cleanDestinations.Length; i++)
    //    {
    //        Transform destination = GameObject.Find(cleanDestinations[i]).transform;
    //        if (Vector3.Distance(destination.position, spillPosition) < 1)
    //        {
    //            //change the destination tag to Dirty
    //            destination.tag = "Dirty";


    //        }
    //    }
    //    currentState = State.Leaving;
    //}
}


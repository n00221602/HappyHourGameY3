using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

using UnityEngine.UIElements;

public class CustomerNPC : MonoBehaviour
{
    public enum State { Moving, Waiting, Searching, Leaving, Drinking, MoveToGame, Neutral, FightEvent, SpillEvent, MessyEvent, Victim }
    public State currentState;

    private float eventTime = 0f;
    private float elapsedTime = 0f;
    private float fightTime = 0f;

    private NavMeshAgent agent;
    private Animator animator;

    Transform bar;
    Transform table;
    Transform initialTable;
    Transform game;
    Transform exit;
    Transform randomVictim;
    Transform facingBar;
    

    public float waitTime = 30f;
    private float waitTimer;

    public GameObject CustomerBeer;
    public GameObject CustomerRedWine;
    public GameObject CustomerWhiteWine;
    public GameObject CustomerCan;
    public GameObject CustomerBottleBeer;

    public GameObject CustomerDrinks;

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
    public GameObject iconFight;

    private GameObject messyDrink;
    private GameObject nearestTable;
    private GameObject cleanDestinationsParent;

    //Static is shared across all customers
    public static GameObject gameDestinationsParent;

   
    public GameObject MessyBeer;
    public GameObject MessyRedWine;
    public GameObject MessyWhiteWine;
    public GameObject puddlePrefab;

    private string[] drinks = { "Beer", "RedWine", "WhiteWine", "Can", "BottleBeer" };
    private string selectedDrink;

    private string[] barDestinations = { "BarDest1", "BarDest2", "BarDest3", "BarDest4", "BarDest5", "BarDest6" };
    private string[] lookAtBar = { "LookAt1", "LookAt2", "LookAt3", "LookAt4", "LookAt5", "LookAt6"};
    private string[] cleanDestinations;
    private static string[] gameDestinations;

    public CustomerTimer customerTimer;

   // private bool victimFound = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = State.Moving;  // Starts the customer in the Moving state
        MoveToCounter();

        // Reputation Objects
        Star1 = GameObject.Find("Player/PlayerUi/PlayerHealth/Star1").gameObject;
        Star2 = GameObject.Find("Player/PlayerUi/PlayerHealth/Star2").gameObject;
        Star3 = GameObject.Find("Player/PlayerUi/PlayerHealth/Star3").gameObject;
        Star4 = GameObject.Find("Player/PlayerUi/PlayerHealth/Star4").gameObject;
        Star5 = GameObject.Find("Player/PlayerUi/PlayerHealth/Star5").gameObject;

        // Icon Objects
        allIcons = transform.Find("Icons/DrinkIcons").gameObject;
        iconBeer = transform.Find("Icons/DrinkIcons/BeerIcon").gameObject;
        iconRedWine = transform.Find("Icons/DrinkIcons/RedWineIcon").gameObject;
        iconWhiteWine = transform.Find("Icons/DrinkIcons/WhiteWineIcon").gameObject;
        iconFight = transform.Find("Icons/FightIcon").gameObject;

        if (iconBeer != null) iconBeer.SetActive(false);
        if (iconRedWine != null) iconRedWine.SetActive(false);
        if (iconWhiteWine != null) iconWhiteWine.SetActive(false);
        if (iconCan != null) iconCan.SetActive(false);
        if (iconBottleBeer != null) iconBottleBeer.SetActive(false);


        if (iconFight != null) iconFight.SetActive(false);
        if (CustomerBeer != null) CustomerBeer.SetActive(false);
        if (CustomerRedWine != null) CustomerRedWine.SetActive(false);
        if (CustomerWhiteWine != null) CustomerWhiteWine.SetActive(false);
        if (CustomerCan != null) CustomerCan.SetActive(false);
        if (CustomerBottleBeer != null) CustomerBottleBeer.SetActive(false);

        gameDestinationsParent = GameObject.Find("GamesDestinations");

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
        // Handles the customer walking animation
        if (animator != null)
        {
            animator.SetBool("isMoving", agent.velocity.sqrMagnitude > 0);
        }

        if (facingBar != null && Vector3.Distance(transform.position, facingBar.position) < 1f)
        {
            transform.LookAt(facingBar);
        }

        if (this.gameObject.tag == "Victim")
        {
            currentState = State.Victim;
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
            case State.FightEvent:
                StartFight();
                break;
            case State.SpillEvent:
                Spill();
                break;
            case State.MessyEvent:
                MessyDrink();
                break;
            case State.Victim:
                ManageVictim();
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
        //animator.SetBool("isMoving", true);

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
                    facingBar = GameObject.Find(lookAtBar[i]).transform;
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
            currentState = State.Waiting;
            waitTimer = 0f;  // Sets the waiting timer to 0
        }
    }


    //Runs during the Waiting state. The customer orders the drink and a timer is started.
    void OrderDrink()
    {
        //animator.SetBool("isMoving", false);
        if (selectedDrink == "Beer")
        {
            iconBeer.SetActive(true);
            messyDrink = MessyBeer;
        }
        if (selectedDrink == "RedWine")
        {
            iconRedWine.SetActive(true);
            messyDrink = MessyRedWine;
        }
        if (selectedDrink == "WhiteWine")
        {
            iconWhiteWine.SetActive(true);
            messyDrink = MessyWhiteWine;
        }
        if (selectedDrink == "Can")
        {
            iconCan.SetActive(true);
            messyDrink = MessyRedWine;
        }
        if (selectedDrink == "BottleBeer")
        {
            iconBottleBeer.SetActive(true);
            messyDrink = MessyWhiteWine;
        }
        // Increment the wait timer
        waitTimer += Time.deltaTime;
        customerTimer.StartTimer();

        // Checks if a drink is handed to the customer, then switch to drinking state
        if (CustomerBeer.activeSelf || CustomerRedWine.activeSelf || CustomerWhiteWine.activeSelf || CustomerCan.activeSelf || CustomerBottleBeer.activeSelf)
        {
            allIcons.SetActive(false);
            this.gameObject.tag = "Served"; // Changes the Customer tag to Served
            currentState = State.MoveToGame;
            facingBar = null;

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
            if (cleanDestinations.All(dest => GameObject.Find(dest).tag != "Clean"))
            {
                Debug.Log("THERES NOWHERE TO SIT");
                currentState = State.Leaving;
            }

            //This goes through the cleanDestinations array and assigns a random destination to the customer    
            for (int i = 0; i < cleanDestinations.Length; i++)
            {
                int randomTable = Random.Range(0, cleanDestinations.Length); ;

                //The initial table is set to the current loop. This table is not guaranteed to be the final table
                initialTable = GameObject.Find(cleanDestinations[randomTable]).transform;
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
                    table.gameObject.tag = "Taken"; // Changes the table tag to Taken
                    break;
                }

                //Switches the table back to clean once the customer is finished
                //if (table == null && initialTable.tag == "Taken" && table.tag != "Dirty")
                //{
                //    table = initialTable;
                //    table.gameObject.tag = "Clean";
                //}
                

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
                Vector3 tablePosition = table.transform.position;

                transform.LookAt(nearestTablePosition);

                // The drink is positioned a third of the way between the table object and the table destination
                Vector3 drinkPosition = nearestTablePosition + (tablePosition - nearestTablePosition) * 0.33f;

                Vector3 offset = new Vector3(0f, 1.018f, 0f);
                string orderedDrink = selectedDrink;
                if (orderedDrink == "Beer")
                {
                    CustomerBeer.transform.position = drinkPosition + offset;
                }
                if (orderedDrink == "RedWine")
                {
                    CustomerRedWine.transform.position = drinkPosition + offset;
                }
                if (orderedDrink == "WhiteWine")
                {
                    CustomerWhiteWine.transform.position = drinkPosition + offset;
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

    //This static fucntion is shared by all customers.
    public static void ManageGameDestinations()
    {
        if (gameDestinationsParent == null)
        {
            gameDestinationsParent = GameObject.Find("GamesDestinations");
        }
        gameDestinations = new string[gameDestinationsParent.transform.childCount];
        for (int i = 0; i < gameDestinationsParent.transform.childCount; i++)
        {
            gameDestinations[i] = gameDestinationsParent.transform.GetChild(i).name;
        }
    }

    //Runs during the Leaving state. Makes the customer leave the bar.
    void LeaveBar()
    {
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        facingBar = null;

        if (this.gameObject.tag == "Drinker")
        {
            this.gameObject.tag = "Drunk";
            CustomerDrinks.SetActive(false);
            agent.speed = 0.8f;
        }

        if (exit == null)
        {
            exit = GameObject.Find("npcExit").transform;
        }

        if (exit != null)
        {
            bar = null;
            table = null;
            if (initialTable != null)
            {
                initialTable.tag = "Clean"; // Changes the table tag to Clean
                initialTable = null;
            }
            Vector3 targetVector = exit.transform.position;
            agent.SetDestination(targetVector);
        }

        // if the exit position is reached, the customer is despawned
        if (Vector3.Distance(agent.transform.position, exit.transform.position) < 1f)
        {
            Debug.Log("BYEEEEE");
            Destroy(gameObject);
        }
        if (currentState == State.Leaving && !reputationLost && this.gameObject.tag == "Customer")
        {
         Debug.Log($"NPC {gameObject.name} is about to lose reputation.");
         BarReputation();
        }
    }


    //Runs during the Drinking state. Picks a random event while the customer is at its table.
    void PickEvent()
    {
        this.gameObject.tag = "Drinker";
        animator.SetBool("isDrunk", true);
        eventTime += Time.deltaTime;
        if (eventTime >= 20f)
        {
            eventTime = 0f;
            int eventInterval = 100;
            int randomChoice = Random.Range(80, eventInterval);
            Debug.Log("Random choice: " + randomChoice);

            if (randomChoice <= 40) //40% chance for a customer to leave the bar
            {
                currentState = State.Leaving;
            }

            if (randomChoice > 40 && randomChoice <= 80) //40% chance for a customer to leave the bar with a messy table
            {
                currentState = State.MessyEvent;
            }
            if (randomChoice > 80) //20% chance for a customer to start a fight
            {
                currentState = State.FightEvent;
            }
        }
    }

    //Runs during the Fighting state. This makes the customer choose a random victim and approach them.
    void StartFight()
    {
        //Checks if there is an existing fighter in the scene. If there is, the customer will not start a fight.
        GameObject existingFighter = GameObject.FindGameObjectWithTag("Fighter");
        if (existingFighter != null && existingFighter != this.gameObject)
        {
            currentState = State.MessyEvent;
            return;
        }

        CustomerDrinks.SetActive(false);
        if(this.gameObject.tag == "Drinker")
        {
            this.gameObject.tag = "Fighter";
            iconFight.SetActive(true);
        }

        GameObject[] drinkers = GameObject.FindGameObjectsWithTag("Drinker");
       
        //If there are no drinkers to fight, the fighter will leave the bar
        if (drinkers.Length == 0 && randomVictim == null && this.gameObject.tag == "Fighter")
        {
            Debug.Log("No drinkers found to fight with.");
            iconFight.SetActive(false);
            this.gameObject.tag = "Drinker";
            currentState = State.Leaving;
            return;
        }

        //Picks a random drinker from the array. This drinker will become the fighter's victim
        foreach (GameObject drinker in drinkers)
    {
        if (drinker != this.gameObject && randomVictim == null)
        {
            randomVictim = drinker.transform;
            break;
        }
    }

        //If a victim is found, the fighter moves towards them
        if (randomVictim != null)
        {
            randomVictim.gameObject.tag = "Victim";
            animator.SetBool("isRunning", true);
            Vector3 targetVector = randomVictim.transform.position;
            agent.SetDestination(targetVector);
            agent.acceleration = 100f;
            //agent.speed = 3f;
        }

        //Once the fighter is in range of the victim, the fighter will stop moving and start fighting
        if (randomVictim != null && Vector3.Distance(this.transform.position, randomVictim.transform.position) < 1f)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isFighting", true);
            agent.isStopped = true;
            transform.LookAt(randomVictim.transform.position);

            elapsedTime += Time.deltaTime;

            //Once the fight hits x seconds, the fighter will stop fighting and replay the fight state
            if (elapsedTime >= 18f)
            {
                Debug.Log($"{gameObject.name} fight timer complete.");
                Debug.Log("Fight timer complete ");
                animator.SetBool("isFighting", false);
                agent.isStopped = false;
                randomVictim = null;
                elapsedTime = 0f;
            }
        }

        //Once the fighters done, it finds a new victim
        if (randomVictim == null)
        {
            GameObject[] newDrinkers = GameObject.FindGameObjectsWithTag("Drinker");
            foreach (GameObject newDrinker in newDrinkers)
            {
                if (newDrinker != this.gameObject && randomVictim == null)
                {
                    randomVictim = newDrinker.transform;
                    break;
                }
            }

            // If there is no victim, the fighter will leave the bar
            if (randomVictim == null)
            {
                Debug.Log("No victim found");
                this.gameObject.tag = "Drinker";
                iconFight.SetActive(false);
                currentState = State.Leaving;
            }
        }

        //If the player hits the fighter, they stop fighting.
        if (this.gameObject.tag == "FighterHit")
        {
            Debug.Log("FIGHTER HAS BEEN HIT BY PLAYER");
            agent.isStopped = false;
            animator.SetBool("isFighting", false);
            animator.SetBool("isRunning", false);
            randomVictim.gameObject.tag = "Drinker";
            randomVictim = null;
            elapsedTime = 0f;
        }

        //After being hit, the fighter leaves the bar
        if (randomVictim == null && this.gameObject.tag == "FighterHit")
        {
            Debug.Log("FIGHTER IS LEAVING");
            iconFight.SetActive(false);
            this.gameObject.tag = "Drinker";
            currentState = State.Leaving;
        }
    }

    void ManageVictim()
    {
        if (this.gameObject.tag == "Victim")
        {
            GameObject fighter = GameObject.FindGameObjectWithTag("Fighter");
            if (fighter != null && Vector3.Distance(this.transform.position, fighter.transform.position) < 1f)
            {
                animator.SetBool("isVictim", true);
                CustomerDrinks.SetActive(false);

                transform.LookAt(fighter.transform.position);
                fightTime += Time.deltaTime;
                 int elapsedfight = Mathf.FloorToInt(fightTime);
                if (fightTime >= 17.8f)
                {
                    float randomSpin = Random.Range(-20f, 20f);
                    transform.Rotate(0, randomSpin, 0);
                    animator.SetBool("isKnockedOut", true);
                    //initialTable.tag = "Clean"; // This makes the table free for other customers to walk to
                    fightTime = 0f;
                    agent.enabled = false;
                }
            }
        }

        if (this.gameObject.tag == "Drinker")
        {
            Debug.Log("return to drinking state");
            Debug.Log("VICTIM HAS BEEN SAVED");

            animator.SetBool("isKnockedOut", false);
            animator.SetBool("isVictim", false);
            fightTime = 0f;

            //Drink is returned to the table, and the customer looks back to their table
            CustomerDrinks.SetActive(true);
            Vector3 nearestTablePosition = nearestTable.transform.position;
            transform.LookAt(nearestTablePosition);

            currentState = State.Drinking;
        }
    }



    //Runs during the MessyEvent state. This makes the customer leave the table with a mess and marks it as dirty.
    void MessyDrink()
    {
        CustomerDrinks.SetActive(false);
        Debug.Log("Drink finished");

        // This uses the nearestTable variable determined in FindCleanDestination()
        if (nearestTable != null)
        {
            Vector3 nearestTablePosition = nearestTable.transform.position;
            Vector3 offset = new Vector3(0f, 1.047f, 0f); // offset the y so prefab spawns on table

            // Copy the messyDrink at the nearest table's position
            if (messyDrink != null)
            {
                GameObject messyDrinkPrefab = Instantiate(messyDrink, nearestTablePosition + offset, Quaternion.identity);

                // Calculate the direction to the table
                Vector3 directionToTable = table.position - messyDrinkPrefab.transform.position;
                directionToTable.y = 0; // Keep only the horizontal direction

                // Set the rotation to face the table on the y-axis
                //if (directionToTable != Vector3.zero)
                //{
                    messyDrinkPrefab.transform.rotation = Quaternion.LookRotation(directionToTable);
                // }

                if (table != null)
                {
                    table.gameObject.tag = "Dirty";
                    Debug.Log("Table tag set to Dirty: " + table.name);
                }
            }
            else
            {
                Debug.LogWarning("Prefab not found");
            }
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


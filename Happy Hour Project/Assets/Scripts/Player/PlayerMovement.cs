using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public GameObject ExitMenu;
    public GameObject ShopMenu;
    bool readyToJump;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode exitKey = KeyCode.Escape;
    public KeyCode shopKey = KeyCode.Q;


    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

   public GameObject DrinksShop;
   public GameObject PassivesShop;
   public GameObject BarmanShop;
   public GameObject ExtrasShop;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        ExitMenu.SetActive(false);
        ShopMenu.SetActive(false);

    }

     private void Update()
    {
        //checks if the player is touching the ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        //constantly checks if the player is attempting to move and also measures speed
      MyInput();  
      SpeedControl();
      //NoMenu();

      //creates friction
      if(grounded)
            rb.drag = groundDrag;
      else
        rb.drag = 0;

    }

    private void FixedUpdate()
    {
      MovePlayer();  
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //allows access to the exit/settings menu
        if (Input.GetKeyDown(exitKey) && !ShopMenu.activeSelf)
        {
            if(!ExitMenu.activeSelf)
            {
                ExitMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //This freezes the scene
                Time.timeScale = 0;
            }
            else
            {
                ExitMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                //This resumes the scene
                Time.timeScale = 1;
            }
        }
                //allows access to the shop menu

        if (Input.GetKeyDown(shopKey) && !ExitMenu.activeSelf)
        {
            if (!ShopMenu.activeSelf)
            {
                ShopMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                ShopMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        // if the player is grounded then allows acccess to the jumping function

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        //if the player is grounded multiplies their speed, allowing for sprinting
        if(Input.GetKey(sprintKey) && grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * 1.1f, ForceMode.Force);
        }

     }
     //if the menu is already active, wont let the player open another menu
     private void NoMenu()
     {
                
        if(Input.GetKeyUp(exitKey) && ExitMenu.activeSelf)
        {
            ExitMenu.SetActive(false);
        }
     }
     //checks what direction the player is moving and then propells them in the appropriate direction relevant to that orientation
     private void MovePlayer()
     {
         moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

         if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

         else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
     }
     //makes sure the player is moving at the appropriate speed
     private void SpeedControl()
     {
         Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

         if(flatVel.magnitude > moveSpeed)
         {
             Vector3 limitedVel = flatVel.normalized * moveSpeed;
             rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
         }
     }

     //adds velocity in the y direction, resulting in jumping
     private void Jump(){
         rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

         rb.AddForce(transform.up * jumpForce, ForceMode.Impulse) ;
     }

     private void ResetJump()
     {
         readyToJump = true;
     }


     //public void Escape()
     //{
     //  if(Input.GetKey(exitKey))
     //   {
     //       SceneManager.LoadScene("MainMenu"); 
     //   }
         
     //}

     //public void ShoppingMenu()
     //{
     //  if(Input.GetKey(shopKey))
     //   {
     //   SceneManager.LoadScene("ShopBook"); 
     //   DrinksShop.SetActive(true);
     //   PassivesShop.SetActive(false);
     //  BarmanShop.SetActive(false);
     //   ExtrasShop.SetActive(false);
     //   }
         
     //}

     //if player has purchased the speed upgrade from the shop, this increased their speed
     public void IncreaseSpeedPurchased()
     {
         //invalid
         moveSpeed *= 1.5f;
     }
         




}

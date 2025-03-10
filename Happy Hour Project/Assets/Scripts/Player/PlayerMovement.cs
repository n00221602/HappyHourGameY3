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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

      MyInput();  
      SpeedControl();
      NoMenu();

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

        if(Input.GetKey(exitKey) && !ExitMenu.activeSelf)
        {
            ExitMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

        }

         if(Input.GetKey(shopKey) && !ShopMenu.activeSelf)
        {
            ShopMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

        }



        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(Input.GetKey(sprintKey) && grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * 1.1f, ForceMode.Force);
        }

     }

     private void NoMenu()
     {
                
        if(Input.GetKeyUp(exitKey) && ExitMenu.activeSelf)
        {
            ExitMenu.SetActive(false);
        }
     }

     private void MovePlayer()
     {
         moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

         if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

         else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
     }

     private void SpeedControl()
     {
         Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

         if(flatVel.magnitude > moveSpeed)
         {
             Vector3 limitedVel = flatVel.normalized * moveSpeed;
             rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
         }
     }

     private void Jump(){
         rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

         rb.AddForce(transform.up * jumpForce, ForceMode.Impulse) ;
     }

     private void ResetJump()
     {
         readyToJump = true;
     }


     public void Escape()
     {
       if(Input.GetKey(exitKey))
        {
            SceneManager.LoadScene("MainMenu"); 
        }
         
     }

     public void ShoppingMenu()
     {
       if(Input.GetKey(shopKey))
        {
            SceneManager.LoadScene("ShopBook"); 
        }
         
     }
         




}

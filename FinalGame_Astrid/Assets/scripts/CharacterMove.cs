using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    //character movements
    private CharacterController controller;
    //store our movement input
    private Vector3 playerMovementInput;
    //store velocity for gravity and jumping on y axis
    private Vector3 velocity;
    public float walkSpeed = 5f;
    public bool isRunning;
    public float runningSpeed;
    //private bool canRun;
    public bool isCouching = false;
    //magic gravity number
    private float gravity = -9.81f;
    public bool isGrounded;
    public float jumpForce;
    private float currentSpeed;

    //camera/mouse settings
    public float mouseSensitivity;
    //ref to our camera
    public Transform cameraTransform;
    //tracking camera vertical and hortizontal movement
    private float yrotation = 0;
    private float xrotation = 0;

    //ground checking
    //layer for ground detection
    public LayerMask groundLayer;
    //empty gameobject at the players feet
    public Transform groundCheck;
    //raycast distance for ground detection
    private float groundDistance = .3f;




    //stamina
    private bool useStamina = true;
    private float maxStamina = 50;
    private float staminaUseMultiplier = 2;
    private float StaminaRegen = 1;
    private float staminaincreased = 1;
    private float staminaTime = 5f;
    private float currentStamina;
    private Coroutine regenStamina;
    public static Action<float> OnStaminaChange;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //locking cursor to middle of screen
       // Cursor.lockState = CursorLockMode.Locked;
        //no cursor
       // Cursor.visible = false;
       //Add custom cursor to make it easier to access menu and objects
        currentStamina = maxStamina;
    }
    void FixedUpdate()
    {
        //is more reliable for physics than collision enter and exit
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance);
        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, Color.red);


    }
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //zInput gets player's w or s input which is -1 or 1
        float zInput = Input.GetAxis("Vertical");
        //for character controller, you can just call velocity, this direvtly helps with movement 
        velocity = transform.forward * zInput * currentSpeed + transform.right * horizontalInput * currentSpeed + Vector3.up * velocity.y;


        // always call functions!!!!! ;-;
        MovePlayer();


        if (Input.GetKeyDown(KeyCode.Z))
        {
            //always on = we can go back and forth
            // ! = no
            isCouching = !isCouching;
            Crouch();
        }

        if (Input.GetKeyDown(KeyCode.Q) && isGrounded)
        {
            isRunning = !isRunning;


            Debug.Log("running");
        }
        if (useStamina)
        {
            HandleStamina();
        }
    }
    private void CameraLook()
    {
        //getting and assigning mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //when the mouse moves horizontally
        //we rotate around the y axis to look left and right
        yrotation += mouseX;
        //rotate the player left/right on y axis rotation
       // transform.rotation = Quaternion.Euler(0f, yrotation, 0);
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);

        //decrease xRotation when moving mouse up so camera tilts up
        //increase x rotation when moving cam down so cam tilts down
        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -90, 90); //prevents flipping

        cameraTransform.localRotation = Quaternion.Euler(xrotation, 0, 0);


       
        }
        private void MovePlayer()
    {
        isGrounded = controller.isGrounded;

        //if the player is on the ground reset gravity
        if (isGrounded)
        {
            //small downward force to keep us grounded
            velocity.y = -1;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        if (isRunning)
        {
            currentSpeed = runningSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        //move player on the x and z
        controller.Move(playerMovementInput * currentSpeed * Time.deltaTime);
        //apply vertical movement (gravity and jumping)
        controller.Move(velocity * Time.deltaTime);

    }

    private void Crouch()
    {
        if (isCouching)
        {
            transform.localScale = new Vector3(1, .5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void HandleStamina()
    {
        if (isRunning)
        {

            // if (regenStamina != null)
            //  {
            // StopCoroutine(regenStamina);
            //   regenStamina = null;
            // }
            currentStamina -= staminaUseMultiplier * Time.deltaTime;

            if (currentStamina < 0)
            {
                currentStamina = 0;
            }
            OnStaminaChange?.Invoke(currentStamina);
            if (currentStamina <= 0)
            {
                isRunning = false;
                Debug.Log("stamina being used");
            }

        }
        if (!isRunning && currentStamina < maxStamina)
        {
            regenStamina = StartCoroutine(RegenerateStamina());
        }

    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(StaminaRegen);
        WaitForSeconds timeToWait = new WaitForSeconds(staminaincreased);

        while (currentStamina < maxStamina)
        {
            if (currentStamina > 0)
            {
                isRunning = true;
            }
            currentStamina += staminaTime;

            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
            OnStaminaChange?.Invoke(currentStamina);
            yield return timeToWait;
        }
        regenStamina = null;
    }
}

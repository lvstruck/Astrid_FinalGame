using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("Base Setup")]
    public float walkingSpeed = 4f;
    public float runningSpeed = 7f;
    public float jumpSpeed = 6f;
    public float gravity = 20f;
    public float turnSpeed = 10f;

    CharacterControl characterControl;
    Vector3 moveDirection = Vector3.zero;

    bool canMove = true;

    private Vector2 input;
    private Quaternion freeRotation;
    private Vector3 targetDirection;
    private bool isGrounded;

    private void Start()
    {
        characterControl = GetComponent<CharacterControl>();

        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Checking if running
        bool isRunning = false;
        isRunning = Input.GetKey(KeyCode.LeftShift);

        //Populate the input Vector2 used for the player rotation
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        //Calculating variables needed for the movement
        float curSpeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) : 0;
        float movementDirectionY = moveDirection.y;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (vertical < 0)
            vertical *= -1;
        if (horizontal < 0)
            horizontal *= -1;
       float move = horizontal + vertical;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        moveDirection = forward * move;
        moveDirection.Normalize();
        moveDirection *= curSpeed;

        //Updating the rotation needed for the player
        UpdateTargetDirection();


        if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
        {
            Vector3 lookDirection = targetDirection.normalized;

            freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0)
                eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * Time.deltaTime);
        }

            //Jumping
            if (Input.GetKeyDown(KeyCode.Space) && canMove && characterControl.isGrounded)
            { moveDirection.y = jumpSpeed; }
            else
            { moveDirection.y = movementDirectionY; }

            if (!characterControl.isGrounded)
                moveDirection.y -= gravity * Time.deltaTime;

        //Applying the movement and jumping calculations
        //if (input != Vector2.zero)
               // characterController.move(moveDirection * Time.deltaTime);
        }

    public void UpdateTargetDirection()
        {
            var forward = Camera.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            var right = Camera.main.transform.TransformDirection(Vector3.right);

            targetDirection = input.x * right + input.y * forward;
        }

    }


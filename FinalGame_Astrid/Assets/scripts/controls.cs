using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float moveSpeed;

    private CharacterController controller;

    //magic gravity number
    private float gravity = 9.81f;

    private Vector2 moveInput;
    void Start()
    {
        //locking cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        //no cursor
         Cursor.visible = false;

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //since we want the character to move with the camera
        Vector3 moveVelocity = (cam.transform.right * moveInput.x + cam.transform.forward * moveInput.y + Vector3.down*gravity) * Time.deltaTime * moveSpeed;
        controller.Move(moveVelocity);
        moveVelocity.y = 0f; //to prevent the character from just staring at ground
        Rotate(moveVelocity);
    }

    private void Rotate(Vector3 target)
    {
        //using gameobject on head to identify where to look
        transform.LookAt(transform.position + target);
    }

    //calling the input system as I downloaded it from the package manager
    public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}

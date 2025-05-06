using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float moveSpeed;

    private CharacterController controller;

    private float gravity = 9.81f;

    private Vector2 moveInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveVelocity = (cam.transform.right * moveInput.x + cam.transform.forward * moveInput.y + Vector3.down * gravity) * Time.deltaTime * moveInput;
        print(moveInput);
    controller.Move(moveVelocity);
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}

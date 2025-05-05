using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // NOTES TO ADD: Camera smoothing 

    // CHARACTER CONTROLLER
    private CharacterController controller;

    // MOVEMENT AND INPUTS
    // Script reference for move vector
    private Controls input;

    // Movements
   // [SerializeField] float walkSpeed = 3f;

    // GROUNDING
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    public bool isGrounded;
    // Layer for ground detection
    public LayerMask groundLayer;
    // Empty location at players feet
    public Transform groundCheck;
    // Raycast distance for ground detection
    private float groundDistance = .4f;


    // CAMERA
    // Camera Reference
    [SerializeField] GameObject mainCam;
    // Transform that is tracked for camera movement
    [SerializeField] Transform cameraFollowTarget;
    // Rotation and Sensitivity Inputs
    float xRotation;
    float yRotation;
    public float mouseSensitivity = 20f;




    // Start is called before the first frame update
    void Start()
    {

        // Grab Player Input Manager Script
        input = GetComponent<Controls>();
        // Grab Character Controller
        controller = GetComponent<CharacterController>();

        // Lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Will need to make visible for options/inventory type screens, etc..



    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0;
        // Target direction based of the x and z inputs
        Vector3 inputDir = new Vector3(input.moveInput.x, 0, input.moveInput.y);
        // Target rotation -- player rotates to direction input
        float targetRotation = 0;



      

        if (input.moveInput != Vector2.zero)
        {

            targetRotation = Quaternion.LookRotation(inputDir).eulerAngles.y + mainCam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
            // Smooths movement rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);



        }
        Vector3 targetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        controller.Move(targetDirection * speed * Time.deltaTime);

        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector3.down, Color.yellow);


    }


    private void LateUpdate()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        // Uses mouse input from x to rotate on y
        // -= to invert mouse look
        xRotation -= input.look.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -20, 60);
        // Uses mouse input from y to rotate on x
        yRotation += input.look.x * mouseSensitivity * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        cameraFollowTarget.rotation = rotation;
    }


}

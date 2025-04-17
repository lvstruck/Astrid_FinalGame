using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObjects : MonoBehaviour
{
    public GameObject offset;
    GameObject targetObject;
    private CharacterController characterController;

    public bool isExamining = false;

    public Canvas canva;

    public GameObject tableObject;

    private Vector3 lastMousePosition;

    private Transform examinedObject; // Store the currently examined object


    //List of position and rotation of the interactble objects 
    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();

    // Start is called before the first frame update
    void Start()
    {
        canva.enabled = false;
        targetObject = GameObject.Find("Main Camera");
        characterController = targetObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // it performs a raycast from the camera to the mouse position and checks if it hits an object tagged as "Object."
        // If it does, it toggles the examination state and stores the examined object's original position and rotation.
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            ToggleExamination();

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("object"))
                {


                    // Store the currently examined object and its original position and rotation
                    if (isExamining)
                    {

                        examinedObject = hit.transform;
                        originalPositions[examinedObject] = examinedObject.position;
                        originalRotations[examinedObject] = examinedObject.rotation;
                    }
                }
            }


        }
        //It then checks if the player is close to an interactable object using the CheckUserClose() method.
        //If the player is close, it calls either Examine() or NonExamine() and enables or disables the canvas component accordingly.

        if (CheckUserClose())
        {


            if (isExamining)
            {
                canva.enabled = false;
                Examine(); StartExamination();
            }
            else
            {
                canva.enabled = true;
                NonExamine(); StopExamination();
            }
        }
        else canva.enabled = false;
    }
    public void ToggleExamination()
    {
        isExamining = !isExamining;

    }
    // This method is called when the player starts examining an object. It locks the cursor,
    // makes it visible, and disables the PlayerInput component to prevent player movement during examination.

    void StartExamination()
    {

        lastMousePosition = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        characterController.enabled = false;
    }
    //This method is called when the player stops examining an object. It locks the cursor again,
    //hides it, and re-enables the PlayerInput component to allow player movement.

    void StopExamination()
    {
        Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
        characterController.enabled = true;
    }

    // This method is called when the player is examining an object.
    // It moves the examined object towards the offset object and allows the player to rotate it based on mouse movement.

    void Examine()
    {
        if (examinedObject != null)
        {
            examinedObject.position = Vector3.Lerp(examinedObject.position, offset.transform.position, 0.2f);

            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            float rotationSpeed = 1.0f;
            examinedObject.Rotate(deltaMouse.x * rotationSpeed * Vector3.up, Space.World);
            examinedObject.Rotate(deltaMouse.y * rotationSpeed * Vector3.left, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    // This method is called when the player is examining an object.
    // It moves the examined object towards the offset object and allows the player to rotate it based on mouse movement.

    void NonExamine()
    {
        if (examinedObject != null)
        {
            // Reset the position and rotation of the examined object to its original values
            if (originalPositions.ContainsKey(examinedObject))
            {
                examinedObject.position = Vector3.Lerp(examinedObject.position, originalPositions[examinedObject], 0.2f);
            }
            if (originalRotations.ContainsKey(examinedObject))
            {
                examinedObject.rotation = Quaternion.Slerp(examinedObject.rotation, originalRotations[examinedObject], 0.2f);
            }
        }
    }
    // This method calculates the distance between the player(targetObject) and 
    // an object called tableObject.If the distance is less than 2 units, it returns true, indicating that the player is close to the object.
    bool CheckUserClose()
    {
        //Calculate the distance between the two GameObjects
        float distance = Vector3.Distance(targetObject.transform.position, tableObject.transform.position);

        // Check if they are close based on the threshold
        return (distance < 2);

    }
}

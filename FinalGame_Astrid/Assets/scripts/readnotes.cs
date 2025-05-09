using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class readnotes : MonoBehaviour
{
    public GameObject player;
    public GameObject noteUI;
    public GameObject hud;

    //will appear when text is picked up
    public GameObject pickUpText;

   // public AudioSource pickUpSound;

    //to tell the distance between the note and player
    public bool inReach;

    void Start()
    {
        noteUI.SetActive(false);
        hud.SetActive(true);
        pickUpText.SetActive(false);

        inReach = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if in contact with game object, pick up texts get activated
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.SetActive(false);
        }
    }


    void Update()
    {
        if (Input.GetButtonDown("Interact") && inReach)
        {
            noteUI.SetActive(true);
           // pickUpSound.Play();
           hud.SetActive(false);
            player.GetComponent<ThirdPersonController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ExitButton()
    {
         noteUI.SetActive(false);
         hud.SetActive(true);
        player.GetComponent<ThirdPersonController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}


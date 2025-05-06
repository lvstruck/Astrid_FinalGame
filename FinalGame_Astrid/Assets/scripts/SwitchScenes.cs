using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public GameObject openText;

    public bool inReach;

    void Start()
    {
        //starting off with the door not in reach
        inReach = false;
    }

    void OnTriggerEnter(Collider other)
    {
        //empty game object attached to player to show when in range of objects
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }

    }
    void Update()
    {

        if (inReach && Input.GetButtonDown("Interact"))
        {
            SceneManager.LoadScene(2);
        }

    }
}





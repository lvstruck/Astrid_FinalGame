using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenKeypad : MonoBehaviour
{

    public GameObject keypadOB;
    public GameObject keypadText;

    public bool inReach;

    // Start is called before the first frame update
    void Start()
    {
        inReach = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            keypadText.SetActive(true);

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            keypadText.SetActive(false);

        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && inReach)
        {
            keypadOB.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LevelExit")
        {
            SceneManager.LoadScene("Second Level");
        }
    }
}

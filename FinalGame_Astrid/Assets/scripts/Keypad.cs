using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public GameObject player;
    public GameObject keypadOB;
    public GameObject hud;
   // public GameObject inv;


    //want to be able to change password
    public TextMeshProUGUI textOB;
    public string answer = "12345";

    public AudioSource button;
    public AudioSource correct;
    public AudioSource wrong;


    // Start is called before the first frame update
    void Start()
    {
        keypadOB.SetActive(false);
    }

    public void Number(int number)
    {
        //add sounds after you fix it
        textOB.text += number.ToString();
        //button.Play();
    }
    public void Execute()
    {
        if (textOB.text == answer)
        {
           // correct.Play();
            textOB.text = "Right";
 

        }
        else
        {
           // wrong.Play();
            textOB.text = "Wrong";
        }

    }
    public void Clear()
    {
        {
            textOB.text = "";
            //button.Play();
        }
    }

    public void Exit()
    {
        keypadOB.SetActive(false);
      //  inv.SetActive(true);
        hud.SetActive(true);
    }
    public void Update()
    {
        if (textOB.text == "Right")
        {
            //ANI.SetBool("animate", true);
            Debug.Log("its open");
        }

        if (keypadOB.activeInHierarchy)
        {
            //if active, turn off everything else on the screen
            hud.SetActive(false);
          //  inv.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

}




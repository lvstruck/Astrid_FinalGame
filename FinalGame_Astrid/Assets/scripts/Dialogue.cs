using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject Activator;
    public string dialogue = "Dialogue";

    public float timer = 2f;

    public AudioSource computernoise;



    void Start()
    {
        text.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            computernoise.Play();
            text.GetComponent<TextMeshProUGUI>().enabled = true;
            text.text = dialogue.ToString();
            StartCoroutine(DisableText());
        }
    }

    IEnumerator DisableText()
    {
        yield return new WaitForSeconds(timer);
        text.GetComponent<TextMeshProUGUI>().enabled = false;
        Destroy(Activator);

    }


}

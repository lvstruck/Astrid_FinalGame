using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialougemanager : MonoBehaviour
{
    //ref to SO
    public Dialouge currentLine;
    //container for dialogue lines
    public Transform dialougeParent;
    public GameObject dialougePrefab;
    //prefab for button choices
    public GameObject choiceButtonPrefab;
    //container for our button choices
    public Transform choiceparent;
    public Button continueButton;

    public void StartingDialouge()
    {
        UpdateDialouge(currentLine);
    }

    public void UpdateDialouge(Dialouge dialougeLine)
    {
        currentLine = dialougeLine;
        //ShowDialouge(currentLine);
        StartCoroutine(DisplayDialouge(currentLine));

        //when dialogue begins appear but not needed
        continueButton.enabled = true;
    }

    //_ can be used to differate variables
   // public void ShowDialouge(Dialouge _dialougeLine)
   // {

    //    //if we have a next line
      //  if (_dialougeLine.nextLine != null) 
        
     //   { 
            //turn on button
        //    continueButton.gameObject.SetActive(true);
      //  }

   // }
    IEnumerator DisplayDialouge(Dialouge line)
    {
        //if (_dialougeLine == null) return; //if no dialouge, exit
        foreach (string _dialogueLine in currentLine.dialogueLinesList)
        {

        //make new copy of button
        GameObject textBubble = Instantiate(dialougePrefab, dialougeParent);
        //grab getcompenentinchildren
        TextMeshProUGUI grabText = textBubble.GetComponentInChildren<TextMeshProUGUI>();
            //set the text ti whatecer string we are currently looping
            grabText.text = _dialogueLine;

            if (!string.IsNullOrEmpty(line.speakerName))
            {
                grabText.text = $"< b > [line.speakerName];</b>{_dialogueLine}";
            }
            yield return new WaitForSeconds(1f);

        }
        //ensure continue button is below all
        continueButton.transform.SetAsLastSibling();

        //clear all choices so they dont stack
        foreach(Transform _child in choiceparent) Destroy(_child.gameObject);
        //hide the continue button by default
        continueButton.gameObject.SetActive(false);
        //button choices appear after the lastest chat line
        choiceparent.transform.SetAsLastSibling();

        if (line.choices != null && line.choices.Length > 0)
        {
            foreach (DialougeChoices choice in line.choices)
            {
                //create a button
                GameObject newButtonChoice = Instantiate(choiceButtonPrefab, choiceparent);
                TextMeshProUGUI buttonText = newButtonChoice.GetComponentInChildren<TextMeshProUGUI>();

                bool meetsRequirement = true;

                //if required stat field has something, not empty
                if (!string.IsNullOrEmpty(choice.requiredStat))
                {
                    //checks player stats and retusn the current value stored in variable
                    //int playerStat = GetPlayerStatValue(choice.requiredStat);
                    //chekcs if greater than or equal to req value
                    //if it is true

                    int playerStat = PlayerStats.Instance.GetStat(choice.requiredStat);
                    meetsRequirement = playerStat >= choice.requiredValue;
                }

                //update button text
                buttonText.text = choice.choicetext;
                if (!meetsRequirement) 
                {
                    buttonText.text += "<color=red>" + choice.requiredStat + ":" + choice.requiredValue + "</color>";

                    //buttonText.text += $"<color=red>({choice.requiredStat}: {choice.requiredValue})</color>";
                }
                Button buttonComp = newButtonChoice.GetComponent<Button>();
                buttonComp.onClick.AddListener(() =>
                {
                    if(!string.IsNullOrEmpty(choice.requiredStat))
                    {
                        PlayerStats.Instance.IncreaseStat(choice.RewardStat, choice.RewardAmt);
                    }
                });

                buttonComp.interactable = meetsRequirement;
                if (meetsRequirement)
                {
                  newButtonChoice.GetComponent<OptionalChoices>().SetUp(this, choice.nextLine, choice.choicetext);

                }
            }
        }
        else if (line.choices != null)
        {

            //clear everything because we are using the same button to avoid cluttering
            continueButton.onClick.RemoveAllListeners();
            //when button is clicked run this code
            continueButton.onClick.AddListener(() =>
            {
                //contuine to next line
                UpdateDialouge(line.nextLine);
                continueButton.gameObject.SetActive(false);
            });
        }
    }

     //int GetPlayerStatValue(string StatName)
   // {
      //  switch (StatName)
      //  {
       //     case "charisma": return PlayerStats.Instance.charmisa;
        //    case "logic": return PlayerStats.Instance.logic;
        //    default: return 0;
        
       // }
        
    //}
}

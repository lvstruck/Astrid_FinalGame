using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialouge/Object")]
//SO is a data container, allows you to storre large amounts of dada without a script

public class Dialouge : ScriptableObject
{
    public string speakerName;

    [TextArea]public List<string>dialogueLinesList = new List<string>();

    
    //list of text to show
    //[TextArea] public string textLine;
    //next line if no choices
    public Dialouge nextLine;
    //choices if any
    public DialougeChoices[] choices;
}
[System.Serializable]
public class DialougeChoices
{
    public string choicetext; // what choices say
    public Dialouge nextLine; //what happens if you pick it

    public string requiredStat;
    public int requiredValue;

    public string RewardStat;
    public int RewardAmt;
}

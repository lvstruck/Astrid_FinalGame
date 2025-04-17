using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionalChoices : MonoBehaviour
{
    Dialougemanager dialougemanager;
    Dialouge dialougeline;
    public TextMeshProUGUI dialogueText;

    public void SetUp(Dialougemanager _dialogueManager, Dialouge _dialongueLine, string _dialogueText)
    {
        dialougemanager = _dialogueManager;
        dialougeline = _dialongueLine;
        dialogueText.text = _dialogueText;
    }

    public void SelectOption()
    {
        dialougemanager.UpdateDialouge(dialougeline);
    }
}

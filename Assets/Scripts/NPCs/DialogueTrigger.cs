using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public TextAsset textFile; // TextAsset: literally text file assets
    private string[] textLines; // each line of the text asset is assigned to an element
    private DialogueManager dialogueManager;

    private int endAtLine; // line we want to end on
    private int currentLine; // current line on the screen

    void Start()
    {
        if (textFile != null) // check if there is a textFile available
        {
            textLines = (textFile.text.Split('\n')); // split at line break
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space) && !dialogueManager.dialogueIsActive)
            {
                

                endAtLine = textLines.Length;
                if (currentLine < endAtLine)
                {
                    dialogueManager.EnableTextBox();
                    dialogueManager.dialogueText.text = textLines[currentLine];
                    Debug.Log(textLines[currentLine]);
                    currentLine += 1;

                } else {
                    dialogueManager.DisableTextBox();
                }
            }
        }
    }

}

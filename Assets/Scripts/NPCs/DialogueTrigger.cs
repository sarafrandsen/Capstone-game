using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public TextAsset textFile; // TextAsset: literally text file assets
    private string[] textLines; // each line of the text asset is assigned to an element
    private DialogueManager dialogueManager;

    private int endAtLine; // line we want to end on
    private int currentLine; // current line on the screen

    /*////////////////////*/
    private PlayerController playerController;
    /*////////////////////*/

    void Start()
    {
        if (textFile != null) // check if there is a textFile available
        {
            textLines = (textFile.text.Split('\n')); // split at line break
        }
        dialogueManager = FindObjectOfType<DialogueManager>();

        /*////////////////////*/
        playerController = FindObjectOfType<PlayerController>();
        /*////////////////////*/

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        /*////////////////////*/
        for (int i = 0; i < playerController.quests.Length; i += 1)
        {
            /*////////////////////*/
            if (other.name == "Player" && this.tag == playerController.quests[i])
            {
                if (Input.GetKeyDown(KeyCode.Space) && !dialogueManager.dialogueIsActive)
                {
                    endAtLine = textLines.Length;
                    if (currentLine < endAtLine && !playerController.isComplete)
                    {
                        dialogueManager.EnableTextBox();
                        dialogueManager.dialogueText.text = textLines[currentLine];
                        Debug.Log(textLines[currentLine]);
                        currentLine += 1;
                    }
                    else
                    {
                        dialogueManager.DisableTextBox();

                        /*////////////////////*/
                        playerController.isComplete = true;
                        i += 1;

                    }
                }
            }
        /*////////////////////*/
        }
        /*////////////////////*/

    }

}

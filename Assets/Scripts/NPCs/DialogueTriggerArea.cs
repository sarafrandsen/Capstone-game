using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerArea : MonoBehaviour {
    public TextAsset textFile; // TextAsset: literally text file assets
    private string[] textLines; // each line of the text asset is assigned to an index

    private DialogueManager dialogueManager;
    private int currentLine; // current line on the screen
	private int endLine; // last line in text

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
            // play bubble animation while inside box collider until dialogue starts

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // end bubble anim
                endLine = textLines.Length;
				dialogueManager.EnableTextBox(); // open dialogue box

                if (currentLine < endLine)
                {
                    dialogueManager.DisplayNextSentence(textLines[currentLine]);
                    Debug.Log(textLines[currentLine]);
                    currentLine += 1; // next line in dialogue
                }
                else
                {
                    dialogueManager.DisableTextBox(); // close dialogue box
                    currentLine = 0;
                    // play melt anim
                    // destroy game object (to get rid of rigid body)
                }
            }
        }
    }
}

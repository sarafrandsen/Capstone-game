using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartNewGame : MonoBehaviour {

    public string levelToLoad;
    public TextAsset instructionsFile;

    private string[] instructionLines;
    private int currentLine; // current line on the screen
    private int endLine; // last line in text

    private DialogueManager dialogueManager;

    public System.Action onConversationBegin;
    public System.Action onConversationEnd;

    void Start()
    {
        if (instructionsFile != null) // check if there is a textFile available
        {
            instructionLines = (instructionsFile.text.Split('\n')); // split at line break
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

	void Update () {
        if (levelToLoad == "Instructions" && (Input.GetKeyDown(KeyCode.Space) || Input.GetKey("joystick button 0")))
        {
            SceneManager.LoadScene(levelToLoad); // load scene (as specified in GUI for collider component
        } 
        else 
        {
            ShowInstructions();
        }
	}

    public void ShowInstructions()
    {
		if (currentLine == 0 && onConversationBegin != null)
		{
			onConversationBegin();
		}

        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space))
        {

            endLine = instructionLines.Length;
            dialogueManager.dialogueBox.SetActive(true); // open dialogue box
            dialogueManager.nameText.text = "@sara__eff";

            if (currentLine < endLine)
            {
                // scripted dialogue
                dialogueManager.DisplayNextSentence(instructionLines[currentLine]);
                currentLine += 1; // next line in dialogue
            }
            else
            {
            dialogueManager.dialogueBox.SetActive(false); // close dialogue box
                currentLine = 0;
                if (onConversationEnd != null)
                {
                    onConversationEnd();
                    SceneManager.LoadScene(levelToLoad); // load scene (as specified in GUI for collider component
       
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerArea : MonoBehaviour {
    public TextAsset textFile; // TextAsset: literally text file assets
    private string[] textLines; // each line of the text asset is assigned to an index

    private PlayerController thePlayer;
    private DialogueManager dialogueManager;
    private int currentLine; // current line on the screen
	private int endLine; // last line in text
    private bool turnedOff = false;

    private GameData gameData;

    public System.Action onConversationBegin;
    public System.Action onConversationEnd;

    public void TurnOff()
    {
        turnedOff = true;
    }

    void Start()
    {
        if (textFile != null) // check if there is a textFile available
        {
            textLines = (textFile.text.Split('\n')); // split at line break
        }

        thePlayer = PlayerController.Instance;
        dialogueManager = FindObjectOfType<DialogueManager>();
        gameData = GameData.Instance;

        thePlayer.canMove = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (turnedOff)
            return;
        
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.A))
        {
            if (other.tag == "Player")
            {
                if (currentLine == 0 && onConversationBegin != null)
                {
                    onConversationBegin();
                }

                endLine = textLines.Length;
                dialogueManager.EnableTextBox(); // open dialogue box

                if (currentLine < endLine)
                {
                    // pause player animation
                    other.GetComponent<Animator>().speed = 0;

                    // scripted dialogue
                    dialogueManager.DisplayNextSentence(textLines[currentLine]);
                    currentLine += 1; // next line in dialogue
                }
                else if (currentLine == endLine)
                {
                    // show random tweet
                    dialogueManager.DisplayNextSentence(dialogueManager.singleTweet);
                    gameData.storiesCollected.Add(dialogueManager.singleTweet);
                    currentLine += 1;
                }
                else
                {
                    dialogueManager.DisableTextBox(); // close dialogue box
                    currentLine = 0;
                    if (onConversationEnd != null)
                    {
                        onConversationEnd();
                    }
                    other.GetComponent<Animator>().speed = 1;
                    thePlayer.canMove = true;
                }
            }
        }
    }
}

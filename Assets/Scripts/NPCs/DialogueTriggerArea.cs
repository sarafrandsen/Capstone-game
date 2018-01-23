using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerArea : MonoBehaviour {
    public TextAsset textFile; // TextAsset: literally text file assets
    private string[] textLines; // each line of the text asset is assigned to an index

    private DialogueManager dialogueManager;
    private Animator anim;
    //private Animation bubble;
    private int currentLine; // current line on the screen
	private int endLine; // last line in text

    private CameraController theCamera;
    private GameData gameData;

    public System.Action onConversationBegin;
    public System.Action onConversationEnd;

    void Start()
    {
        if (textFile != null) // check if there is a textFile available
        {
            textLines = (textFile.text.Split('\n')); // split at line break
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
        anim = GetComponent<Animator>();
        //bubble = GetComponent<Animation>();

        theCamera = FindObjectOfType<CameraController>();
        gameData = FindObjectOfType<GameData>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey("joystick button 0"))
        {
            if (other.name == "Player")
            {
                if (currentLine == 0 && onConversationBegin != null)
                {
                    onConversationBegin();
                }

                // end bubble anim
                endLine = textLines.Length;
                dialogueManager.EnableTextBox(); // open dialogue box
                dialogueManager.nameText.text = "@" + dialogueManager.nameMagicBot;

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
                    dialogueManager.DisplayNextSentence(dialogueManager.tweetMagicBot);
                    gameData.storiesCollected.Add(dialogueManager.tweetMagicBot);
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
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            // begin bubble anim
            //bubble.Play(); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            // end bubble anim
        }
    }

}

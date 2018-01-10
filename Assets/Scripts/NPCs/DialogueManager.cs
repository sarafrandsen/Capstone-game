using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for UI objects like Text

public class DialogueManager : MonoBehaviour {

    public Text nameText; // where to put the Name game object in the Inspector
    public Text dialogueText; // where to put the Dialogue game object in the Inspector

    public TextAsset textFile; // TextAsset: literally text file assets
    public PlayerController player; // access the player
    public GameObject dialogueBox; // access the dialogue box game object

    public bool dialogueIsActive; // toggle dialogue box


    private Queue<string> sentences; // FIFO collection
    private string[] textLines; // each line of the text asset is assigned to an element
    private int currentLine; // current line on the screen
    private int endAtLine; // line we want to end on

    void Start()
    {
        sentences = new Queue<string>();

        if (textFile != null) // check if there is a textFile available
        {
            textLines = (textFile.text.Split('\n')); // split at line break
        }

        player = FindObjectOfType<PlayerController>(); // using this class on the PlayerController

        endAtLine = textLines.Length - 1;

        if (dialogueIsActive)
        {
            EnableTextBox();
        } else {
            DisableTextBox();
        }
    }

    void Update()
    {
        if (!dialogueIsActive)
        {
            return;
        }

        if (currentLine > endAtLine )
        {
            DisableTextBox();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            dialogueText.text = textLines[currentLine];
            currentLine += 1;
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) // if there are no more sentences in the queue
        {
            DisableTextBox();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        StopAllCoroutines(); // if a user wants to skip a sentence, stop animating and move on to next sentence
        StartCoroutine(TypeSentence(sentence));
    }

    public void EnableTextBox()
    {
        dialogueBox.SetActive(true);
    }

    public void DisableTextBox()
    {
        dialogueBox.SetActive(false);
        dialogueIsActive = false;
        Debug.Log("End of conversation");
        return;
    }

    IEnumerator TypeSentence(string sentence) // instead of updating text directly, using coroutine
    {
        dialogueText.text = ""; // start with empty string

        // loop through the individual characters in each sentence
        foreach(char letter in sentence.ToCharArray()) // ToCharArray is a function that converts a string into a character array
        {
            dialogueText.text += letter; // add each letter to the text to be displayed
            yield return null; // wait a frame between each letter
        }
    }
}

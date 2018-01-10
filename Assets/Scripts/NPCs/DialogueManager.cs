using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for UI objects like Text

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    //public Animator animator; // reference animator controller in order to control the dialog box animation 
    public TextAsset textFile; // TextAsset: literally text file assets
    public string[] textLines; // each line of text asset is assigned to an element
    public int currentLine;
    public int endAtLine;
    public PlayerController player;
    public bool dialogueIsActive;
    public GameObject dialogueBox;


    private Queue<string> sentences; // FIFO collection

    void Start()
    {
        sentences = new Queue<string>();

        if (textFile != null) // check if there is a textFile available
        {
            textLines = (textFile.text.Split('\n')); // split at line break
        }

        player = FindObjectOfType<PlayerController>(); // using on the PlayerController

        if (endAtLine == 0) // if endAtLine is not defined
        {
            endAtLine = textLines.Length - 1; // default to every line
        }

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

        dialogueText.text = textLines[currentLine];

        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentLine += 1;
        }

        if (currentLine > endAtLine)
        {
            DisableTextBox();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //animator.SetBool("IsOpen", true); // open dialogue box when conversation starts 

        nameText.text = dialogue.name;
        sentences.Clear(); // clear any previous sentences

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue((sentence)); // put each sentence in the array in a queue
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) // if there are no more sentences in the queue
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        StopAllCoroutines(); // if a user wants to skip a sentence, stop animating and move on to next sentence
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        //animator.SetBool("IsOpen", false); // close box at the end of the dialogue
        Debug.Log("End of conversation");
    }

    public void EnableTextBox()
    {
        dialogueBox.SetActive(true);
    }

    public void DisableTextBox()
    {
        dialogueBox.SetActive(false);
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

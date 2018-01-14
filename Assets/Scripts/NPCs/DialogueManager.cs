using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for UI objects like Text

public class DialogueManager : MonoBehaviour {
    /*///////////////////////////////////*/
    public string key, secret, accessToken;
    [SerializeField]
    Twitter.TwitterUser user;
    /*///////////////////////////////////*/


    public Text nameText; // where to put the Name game object in the Inspector
    public Text dialogueText; // where to put the Dialogue game object in the Inspector

    public PlayerController player; // access the player
    public GameObject dialogueBox; // access the dialogue box game object

    public bool dialogueIsActive; // toggle dialogue box

    private Queue<string> sentences; // FIFO collection

    void Start()
    {
        /*///////////////////////////////////*/
        accessToken = Twitter.API.GetTwitterAccessToken(key, secret);
        user = Twitter.API.GetProfileInfo("sara__eff", accessToken, false);
        /*///////////////////////////////////*/



        //sentences = new Queue<string>();
        //sentences.Enqueue(dialogueText.text);
        dialogueIsActive = false;
        player = FindObjectOfType<PlayerController>(); // using this class on the PlayerController
    }

    void Update()
    {
        if (!dialogueIsActive)
        {
            return;
        }
    }

    public void DisplayNextSentence()
    {
        //if (sentences.Count == 0) // if there are no more sentences in the queue
        //{
        //    DisableTextBox();
        //    return;
        //}

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
        foreach(char letter in sentence)
        {
            dialogueText.text += letter; // add each letter to the text to be displayed
            yield return null; // wait a frame between each letter
        }
    }
}

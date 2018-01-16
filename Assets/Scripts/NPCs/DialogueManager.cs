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

	public GameObject dialogueBox; // access the dialogue box object
    public Text nameText; // where to put the character's name in inspector
    public Text dialogueText; // where to put the Dialogue game object in the Inspector

    public PlayerController player; // access the player object

    public bool dialogueIsActive; // toggle dialogue box

    void Start()
    {
        /*///////////////////////////////////*/
        accessToken = Twitter.API.GetTwitterAccessToken(key, secret);
        user = Twitter.API.GetProfileInfo("sara__eff", accessToken, false);
        /*///////////////////////////////////*/

        DisableTextBox();
        player = FindObjectOfType<PlayerController>(); // using this class on the PlayerController
    }

    public void DisplayNextSentence(string nextSentence)
    {
        StopAllCoroutines(); // if a user wants to skip a sentence, stop animating and move on to next sentence
        StartCoroutine(TypeSentence(nextSentence));
    }

    public void EnableTextBox()
    {
        dialogueBox.SetActive(true);
        dialogueIsActive = true;
    }

    public void DisableTextBox()
    {
        dialogueBox.SetActive(false);
        dialogueIsActive = false;
        Debug.Log("End of conversation");
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

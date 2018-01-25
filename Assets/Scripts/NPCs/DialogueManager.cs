using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for UI objects like Text

public class DialogueManager : MonoBehaviour {
    /*///////////////////////////////////*/
    public string key, secret, accessToken;
    public Twitter.Tweet[] tweets;

    public string twitterHandle;
    public string singleTweet;
    public int numberOfTweets;

    /*///////////////////////////////////*/

	public GameObject dialogueBox; // access the dialogue box object
    public Text nameText; // where to put the character's name in inspector
    public Text dialogueText; // where to put the Dialogue game object in the Inspector

    private PlayerController thePlayer;

    public void Start()
    {
        /*///////////////////////////////////*/
        accessToken = Twitter.API.GetTwitterAccessToken(key, secret); // generate access token
        tweets = Twitter.API.GetUserTimeline(twitterHandle, numberOfTweets, accessToken);
		
        int randomIndex = UnityEngine.Random.Range(0, tweets.Length); // random index for tweet
		singleTweet = tweets[randomIndex].text; // text of the random tweet
        /*///////////////////////////////////*/

        thePlayer = FindObjectOfType<PlayerController>();

        DisableTextBox();
    }

    public void DisplayNextSentence(string nextSentence)
    {
        StopAllCoroutines(); // if a user wants to skip a sentence, stop animating and move on to next sentence
        StartCoroutine(TypeSentence(nextSentence));
    }

    public void EnableTextBox()
    {
        dialogueBox.SetActive(true);
        thePlayer.canMove = false;
    }

    public void DisableTextBox()
    {
        dialogueBox.SetActive(false);
        thePlayer.canMove = true;
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

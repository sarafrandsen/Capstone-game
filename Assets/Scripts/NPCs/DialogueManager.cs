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
    public string singleTweet = "If only there were a tweet here.";
    public int numberOfTweets;
    public bool startDialogueClosed = true;

    /*///////////////////////////////////*/

	public GameObject dialogueBox; // access the dialogue box object
    public Text dialogueText; // where to put the Dialogue game object in the Inspector

    private PlayerController thePlayer;

    public void Start()
    {
        if (!string.IsNullOrEmpty(twitterHandle))
        {
            StartCoroutine(FindTweet());
        }

        thePlayer = PlayerController.Instance;

        if (startDialogueClosed)
        {
            DisableTextBox();
        }
    }

    private IEnumerator FindTweet()
    {
        /*///////////////////////////////////*/
        accessToken = Twitter.API.GetTwitterAccessToken(key, secret); // generate access token
        tweets = Twitter.API.GetUserTimeline(twitterHandle, numberOfTweets, accessToken);

        int randomIndex = UnityEngine.Random.Range(0, tweets.Length); // random index for tweet
        singleTweet = tweets[randomIndex].text; // text of the random tweet
        /*///////////////////////////////////*/

        yield return null;
    }

    public void DisplayNextSentence(string nextSentence)
    {
        StopAllCoroutines(); // if a user wants to skip a sentence, stop animating and move on to next sentence
        StartCoroutine(TypeSentence(nextSentence));
    }

    public void EnableTextBox()
    {
        dialogueBox.SetActive(true);

        if (thePlayer != null)
        {
            thePlayer.canMove = false;
        }
    }

    public void DisableTextBox()
    {
        dialogueBox.SetActive(false);

        if (thePlayer != null)
        {
            thePlayer.canMove = true;
        }
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

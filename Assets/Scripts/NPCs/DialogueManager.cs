using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for UI objects like Text

public class DialogueManager : MonoBehaviour {
    /*///////////////////////////////////*/
    public string key, secret, accessToken;
    [SerializeField]
    public Twitter.TwitterUser userJamey;
    [SerializeField]
    public Twitter.Tweet[] tweetsJamey;
    public string nameJamey;
    public string urlJamey;
	public string tweetJamey;

    /*///////////////////////////////////*/

	public GameObject dialogueBox; // access the dialogue box object
    public Text nameText; // where to put the character's name in inspector
    public Text dialogueText; // where to put the Dialogue game object in the Inspector
    public GameObject profileImage; // Twitter profile image

    public void Start()
    {
        /*///////////////////////////////////*/
        accessToken = Twitter.API.GetTwitterAccessToken(key, secret);
        userJamey = Twitter.API.GetProfileInfo("jameydeorio", accessToken, false);
        tweetsJamey = Twitter.API.GetUserTimeline("jameydeorio", 200, accessToken);


        nameJamey = userJamey.screen_name; // get the user name
        urlJamey = userJamey.profile_image_url; // get the profile image
        urlJamey.Replace("_normal", ""); // original size, not 48x48
        /*///////////////////////////////////*/

        DisableTextBox();
    }

    public void Update()
    {
        int randomIndex = UnityEngine.Random.Range(0, tweetsJamey.Length); // random index for tweet
		tweetJamey = tweetsJamey[randomIndex].text; // text of the random tweet
    }

    public void DisplayNextSentence(string nextSentence)
    {
        StopAllCoroutines(); // if a user wants to skip a sentence, stop animating and move on to next sentence
        StartCoroutine(TypeSentence(nextSentence));
    }

    public void EnableTextBox()
    {
        dialogueBox.SetActive(true);
    }

    public void DisableTextBox()
    {
        dialogueBox.SetActive(false);
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

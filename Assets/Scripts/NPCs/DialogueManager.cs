using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for UI objects like Text

public class DialogueManager : MonoBehaviour {
    /*///////////////////////////////////*/
    public string key, secret, accessToken;
    [SerializeField]
    public Twitter.TwitterUser userMagicBot;

    [SerializeField]
    public Twitter.Tweet[] tweetsMagicBot;

    public string nameMagicBot;
    public string urlMagicBot;
	public string tweetMagicBot;

    /*///////////////////////////////////*/

    public GameObject profileImage;
	public GameObject dialogueBox; // access the dialogue box object
    public Text nameText; // where to put the character's name in inspector
    public Text dialogueText; // where to put the Dialogue game object in the Inspector

    private CameraController theCamera;
    private PlayerController thePlayer;

    public void Start()
    {
        /*///////////////////////////////////*/
        accessToken = Twitter.API.GetTwitterAccessToken(key, secret); // generate access token
        userMagicBot = Twitter.API.GetProfileInfo("magicrealismbot", accessToken, false);
        tweetsMagicBot = Twitter.API.GetUserTimeline("magicrealismbot", 100, accessToken);

        nameMagicBot = userMagicBot.screen_name; // get the user name
        urlMagicBot = userMagicBot.profile_image_url.Replace("_normal", ""); // get the profile image // original size, not 48x48
		
        int randomIndex = UnityEngine.Random.Range(0, tweetsMagicBot.Length); // random index for tweet
		tweetMagicBot = tweetsMagicBot[randomIndex].text; // text of the random tweet
        /*///////////////////////////////////*/

        theCamera = FindObjectOfType<CameraController>();
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
        profileImage.GetComponent<Image>().enabled = true;
        thePlayer.canMove = false;
    }

    public void DisableTextBox()
    {
        dialogueBox.SetActive(false);
        profileImage.GetComponent<Image>().enabled = false;
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

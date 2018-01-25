using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalDialogueTrigger : MonoBehaviour {
    public TextAsset textFile; // TextAsset: literally text file assets
	public Image fadeOverlay;
    public AudioSource finalSong;
    private string[] textLines; // each line of the text asset is assigned to an index

    private DialogueManager dialogueManager;
    private int currentLine; // current line on the screen
    private int currentStory; // current random tweet being repeated back
    private int endLine; // last line in text

    private CameraController theCamera;
    private GameData gameData;

    public System.Action onConversationEnd;

    void Start()
    {
        if (textFile != null) // check if there is a textFile available
        {
            textLines = (textFile.text.Split('\n')); // split at line break
        }

        dialogueManager = FindObjectOfType<DialogueManager>();

        theCamera = FindObjectOfType<CameraController>();
        gameData = FindObjectOfType<GameData>();


        AudioSource gameDataAudioSource = gameData.GetComponent<AudioSource>();
        gameDataAudioSource.Stop();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                {
                    // end bubble anim
                    endLine = textLines.Length;
                    dialogueManager.EnableTextBox(); // open dialogue box
                    dialogueManager.nameText.text = "@" + dialogueManager.twitterHandle;

                    if (currentLine < endLine)
                    {
                        // pause player animation
                        other.GetComponent<Animator>().speed = 0;

                        // scripted dialogue
                        dialogueManager.DisplayNextSentence(textLines[currentLine]);
                        currentLine += 1; // next line in dialogue
                    }
                    else if (currentLine >= endLine)
                    {
                        if (currentStory < gameData.storiesCollected.Count)
                        {
                            // show random tweet
                            dialogueManager.DisplayNextSentence(gameData.storiesCollected[currentStory]);
                            currentStory += 1;
                        }
                    }
                    StartCoroutine(Fade(other.gameObject, fadeTime: 5));
                }
            }
        }
    }

    private IEnumerator Fade(GameObject player, float fadeTime)
    {
        // NOTE: Don't let fadeTime be 0.

        float time = 0f;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            float t = time / fadeTime;
            finalSong.volume =  1 - t;
            fadeOverlay.color = new Color(0f, 0f, 0f, t);

            yield return null;
        }

        finalSong.Stop();
        Destroy(player);
        Destroy(theCamera.gameObject);
        Destroy(gameData.gameObject);
        SceneManager.LoadScene("Credits");
    }
}

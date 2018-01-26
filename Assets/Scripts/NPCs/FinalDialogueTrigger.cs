using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalDialogueTrigger : MonoBehaviour {
    public TextAsset textFile; // TextAsset: literally text file assets
    public TextAsset endingFile;
	public Image fadeOverlay;
    public AudioSource finalSong;

    private DialogueManager dialogueManager;
    private int currentStory; // current random tweet being repeated back
    private List<string> storiesToRemember = new List<string>();

    private CameraController theCamera;
    private GameData gameData;

    void Start()
    {
        if (textFile != null) // check if there is a textFile available
        {
            storiesToRemember.AddRange(textFile.text.Split('\n'));
        }

        gameData = GameObject.FindWithTag("GameData").GetComponent<GameData>();
        foreach (string story in gameData.storiesCollected)
        {
            storiesToRemember.Add('"' + story + '"');
        }

        if (endingFile != null)
        {
            storiesToRemember.AddRange(endingFile.text.Split('\n'));
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
        theCamera = FindObjectOfType<CameraController>();

        AudioSource gameDataAudioSource = gameData.GetComponent<AudioSource>();
        gameDataAudioSource.Stop();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                // end bubble anim
                dialogueManager.EnableTextBox(); // open dialogue box
                dialogueManager.nameText.text = "@";

                if (currentStory < storiesToRemember.Count)
                {
                    other.GetComponent<Animator>().speed = 0;

                    // scripted dialogue
                    dialogueManager.DisplayNextSentence(storiesToRemember[currentStory]);
                    currentStory += 1; // next line in dialogue
                }
                else
                {
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
        Destroy(gameData.gameObject);
        Destroy(theCamera.gameObject);
		SceneManager.LoadScene("Credits");
    }

}

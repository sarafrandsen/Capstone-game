using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartNewGame : MonoBehaviour
{

    public string levelToLoad;
    public TextAsset instructionsFile;
    public Image fadeOverlay;
    public AudioClip bgm;
    public bool turnOffMusicAtEnd = true;

    private string[] instructionLines;
    private int currentLine; // current line on the screen
    private int endLine; // last line in text
    private string sceneName;

    private DialogueManager dialogueManager;

    public System.Action onConversationBegin;
    public System.Action onConversationEnd;

    void Start()
    {
        if (instructionsFile != null) // check if there is a textFile available
        {
            instructionLines = (instructionsFile.text.Split('\n')); // split at line break
        }

        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Instructions")
        {
            ShowInstructions();
        }

        dialogueManager = FindObjectOfType<DialogueManager>();

        if (bgm != null)
            BGMPlayer.Instance.PlayMusic(bgm);
    }

    void Update()
    {
        if (levelToLoad == "Instructions" && (Input.GetKeyDown(KeyCode.Space) || Input.GetKey("joystick button 0")))
        {
            // fade
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            ShowInstructions();
        }
    }

    public void ShowInstructions()
    {
        if (currentLine == 0 && onConversationBegin != null)
        {
            onConversationBegin();
        }

        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space))
        {
            endLine = instructionLines.Length;
            dialogueManager.dialogueBox.SetActive(true); // open dialogue box

            if (currentLine < endLine)
            {
                // scripted dialogue
                dialogueManager.DisplayNextSentence(instructionLines[currentLine]);
                currentLine += 1; // next line in dialogue
            } else {
                
				StartCoroutine(Fade(fadeTime: 3));
            }

        }
    }

    private IEnumerator Fade(float fadeTime)
    {
        // NOTE: Don't let fadeTime be 0.

        float time = 0f;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            float t = time / fadeTime;

            if (turnOffMusicAtEnd)
            {
                BGMPlayer.Instance.SetVolume(1 - t);
            }
            fadeOverlay.color = new Color(0f, 0f, 0f, t);

            yield return null;
        }

        if (turnOffMusicAtEnd)
        {
            BGMPlayer.Instance.Stop();
        }
        SceneManager.LoadScene("Main");
    }
}
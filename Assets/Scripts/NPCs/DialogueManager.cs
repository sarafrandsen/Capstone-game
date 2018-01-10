﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for UI objects like Text

public class DialogueManager : MonoBehaviour {

    //   public GameObject dialogBox;
    //   public Text dialogText;
    //   public bool dialogActive;
    //   public float showTime = 0.5f;

    //   private float showCounter;

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {
    //       // if the dialogue box is up and the user presses 'space'
    //       if (dialogActive)
    //       {
    //           showCounter -= Time.deltaTime;

    //           if (Input.GetKeyDown(KeyCode.Space) && showCounter < 0)
    //           {
    //               Debug.Log("Hide");
    //               dialogBox.SetActive(false); // close the dialog box obj on screen
    //               dialogActive = false;
    //           }
    //       }
    //}

    //public void ShowDialogBox(string dialog)
    //{
    //    if (dialogActive)
    //    {
    //        return; 
    //    }

    //    showCounter = showTime;
    //    dialogActive = true;
    //    dialogBox.SetActive(true);
    //    dialogText.text = dialog;
    //}


    public Text nameText;
    public Text dialogueText;
    public Animator animator; // reference animator controller in order to control the dialog box animation

    private Queue<string> sentences; // FIFO collection

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true); // open dialogue box when conversation starts

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
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false); // close box at the end of the dialogue
        Debug.Log("End of conversation");
    }
}

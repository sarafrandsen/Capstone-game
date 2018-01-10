using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// trigger new dialogue
public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // feed dialogue to DialogueManager
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}

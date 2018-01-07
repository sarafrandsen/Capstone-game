using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour {

    public string dialog; // what we want the npc to say
    private DialogManager dialogMgr; // so we can access ShowDialogBox()

	// Use this for initialization
	void Start () {
        dialogMgr = FindObjectOfType<DialogManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // checking if the player is in the collider to interact with the NPC
    private void OnTriggerStay2D(Collider2D other) // 'stay' is any time a box is in a trigger
    {
        if (other.gameObject.name == "Player") // if the collider is the player
        {
            if (Input.GetKeyUp(KeyCode.Space)) // if they release 'space'
            {
                dialogMgr.ShowDialogBox(dialog);
            }
        }
    }
}

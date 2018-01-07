using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public GameObject dialogBox;
    public Text dialogText;
    public bool dialogActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // if the dialogue box is up and the user presses 'space'
		if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            dialogBox.SetActive(false); // close the dialog box obj on screen
            dialogActive = false; // deactivate dialog
        }
	}

    public void ShowDialogBox(string dialog)
    {
        dialogActive = true; // activate dialog
        dialogBox.SetActive(true); // open dialog box obj on screen
        dialogText.text = dialog; // display the text given
    }
}

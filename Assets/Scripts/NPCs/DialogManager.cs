using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for UI objects like Text

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
            dialogActive = false;
        }
	}

    public void ShowDialogBox(string dialog)
    {
        dialogActive = true;
        dialogBox.SetActive(true);
        dialogText.text = dialog;
    }
}

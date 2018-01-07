using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogText : MonoBehaviour {
    public string dialog;

    private DialogManager dialogManager;

	// Use this for initialization
	void Start () {
        dialogManager = FindObjectOfType<DialogManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "TempPlayer")
        {
            Input.GetKeyUp(KeyCode.Space);
        }
        dialogManager.ShowDialogBox(dialog);
    }
}

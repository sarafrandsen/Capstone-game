using System.Collections;
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


    private Queue<string> sentences; // FIFO collection

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Start conversation with " + dialogue.name);
    }
}

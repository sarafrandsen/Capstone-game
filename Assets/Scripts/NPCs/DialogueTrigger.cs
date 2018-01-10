using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public TextAsset textFile; // TextAsset: literally text file assets
    private string[] textLines; // each line of the text asset is assigned to an element

    void Start()
    {
        if (textFile != null) // check if there is a textFile available
        {
            textLines = (textFile.text.Split('\n')); // split at line break
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(textLines[0]);
            }
        }
    }

}

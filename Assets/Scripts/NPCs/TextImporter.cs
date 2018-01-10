using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImporter : MonoBehaviour {

    public TextAsset textFile; // TextAsset: literally text file assets
    public string[] textLines; // each line of text asset is assigned to an element

	// Use this for initialization
	void Start () {
		if (textFile != null) // check if there is a textFile available
        {
            textLines = (textFile.text.Split('\n')); // split at line break
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

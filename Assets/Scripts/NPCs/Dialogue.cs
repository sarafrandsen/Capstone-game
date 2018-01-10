using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // not deriving from monobehavior
public class Dialogue {

    public string name;

    [TextArea(3, 10)] // change the size of text slots in the GUI (min lines, max lines)
    public string[] sentences; // array of strings
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

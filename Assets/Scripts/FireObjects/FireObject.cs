using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour {
    private GameData gameData;

	// Use this for initialization
	void Start () {
        // check in GameData if true or false
        // false means still accessible
        // true means inaccessible
        string fireDoor = this.tag;
        if (gameData.fireDoors[fireDoor] == true)
        {
            ExtinguishFire();
        }
	}

    void ExtinguishFire() {
        // change animation to empty fire
        // disable 'load new area' script
        this.GetComponent<LoadNewArea>().enabled = false;
    }
}

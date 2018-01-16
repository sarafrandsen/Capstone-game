using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour {
    private GameData gameData;
    private Animator anim;

	// Use this for initialization
	void Start () {
        // opening dialogue with instructions
        // set dialogue as active?


        anim = GetComponent<Animator>();

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
        anim.SetBool("FireExtinguished", true);
        // disable 'load new area' script
        this.GetComponent<LoadNewArea>().enabled = false;
    }
}

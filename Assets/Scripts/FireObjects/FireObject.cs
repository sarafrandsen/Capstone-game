using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour {
    private GameData gameData;
    private Animator anim;
    private BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () {
        // opening dialogue with instructions
        // set dialogue as active?

        var gameDatas = GameObject.FindGameObjectsWithTag("GameData");
        gameData = gameDatas[0].GetComponent<GameData>();

        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

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
        anim.SetBool("FireExtinguished", false);
        // disable 'load new area' script
        boxCollider.isTrigger = false;
    }
}

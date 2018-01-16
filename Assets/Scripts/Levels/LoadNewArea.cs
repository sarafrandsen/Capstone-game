using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {

    public string levelToLoad;
    public string exitPoint;

    private PlayerController thePlayer;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
	}

    void OnTriggerEnter2D(Collider2D other) // we know it'll be a collider, and will reference it as 'other'
    {
        if (other.gameObject.name == "Player") // if the collider entering the trigger is named "Player"
        {
            SceneManager.LoadScene(levelToLoad); // load scene (as specified in GUI for collider component)
            thePlayer.startPoint = exitPoint;
        }
    }
}

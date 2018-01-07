using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {

    public string levelToLoad;

	// Use this for initialization
	void Start () {
        Debug.Log("new area load");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) // we know it'll be a collider, and will reference it as 'other'
    {
        if (other.gameObject.name == "Player") // if the collider entering the trigger is named "Player"
        {
            SceneManager.LoadScene(levelToLoad); // load scene (as specified in GUI for collider component)

        }
    }
}

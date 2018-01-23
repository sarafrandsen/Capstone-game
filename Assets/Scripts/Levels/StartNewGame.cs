using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGame : MonoBehaviour {

    public string levelToLoad;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey("joystick button 0"))
        {
            SceneManager.LoadScene(levelToLoad); // load scene (as specified in GUI for collider component
        }
	}
}

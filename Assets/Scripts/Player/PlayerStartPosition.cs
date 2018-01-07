using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// check position of starting point and set player and camera accordingly
public class PlayerStartPosition : MonoBehaviour {

    public Vector2 startDirection; // enter in the GUI the direction player should face

    private PlayerController thePlayer;
    private CameraController theCamera;

	// Use this for initialization
	void Start () {
        Debug.Log("player start position");
        thePlayer = FindObjectOfType<PlayerController>();
        thePlayer.transform.position = transform.position;
        thePlayer.lastMove = startDirection; // set the default position the player should face upon entry

        theCamera = FindObjectOfType<CameraController>();
        theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z); // set default position camera should point
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

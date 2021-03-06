﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// check position of starting point and set player and camera accordingly
public class PlayerStartPosition : MonoBehaviour {

    public Vector2 startDirection; // enter in the GUI the direction player should face
    public string positionID;
    public WorldOrientation worldOriention = WorldOrientation.Overhead;
    public GameObject followTarget;

    private PlayerController thePlayer;
    private CameraController theCamera;

    // Use this for initialization
    void Start()
    {
        thePlayer = PlayerController.Instance;

        if (thePlayer.startPoint == positionID)
        {
            thePlayer.transform.position = transform.position;
            thePlayer.lastMove = startDirection; // set the default position the player should face upon entry

            thePlayer.SetOrientation(worldOriention);

            theCamera = FindObjectOfType<CameraController>();
            theCamera.SetOrientation(worldOriention, (followTarget != null) ? followTarget : thePlayer.gameObject);
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z); // set default position camera should point
        }
    }
}

public enum WorldOrientation
{
    Overhead,
    SideScroll
}
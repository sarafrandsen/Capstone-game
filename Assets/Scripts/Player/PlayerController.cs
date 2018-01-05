using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update () {

        // get the input from the keyboard
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // determine the speed in seconds per frame
        float moveHorizontal = horizontal * moveSpeed * Time.deltaTime;
        float moveVertical = vertical * moveSpeed * Time.deltaTime;

        // absolute value keeps player from moving diagonally
        if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
        {
            moveVertical = 0;
        } else {
            moveHorizontal = 0;
        } 

        // this tells it the coordinates to move to
        transform.Translate (new Vector2(moveHorizontal, moveVertical));
    }﻿
}

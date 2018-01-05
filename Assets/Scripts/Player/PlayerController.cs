using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5;

    private Animator anim;
    private bool isMoving;
    private Vector2 lastMove;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
    void Update () {

        isMoving = false;

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
            isMoving = true;
            lastMove = new Vector2(horizontal, 0f);
        } else if (Mathf.Abs(moveHorizontal) < Mathf.Abs(moveVertical)) {
            moveHorizontal = 0;
            isMoving = true;
            lastMove = new Vector2(0f, vertical);
        } else {
            isMoving = false;
        }

        // this tells it the coordinates to move to
        transform.Translate (new Vector2(moveHorizontal, moveVertical));
        anim.SetBool("IsMoving", isMoving);
        anim.SetFloat("MoveX", horizontal);
        anim.SetFloat("MoveY", vertical);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }﻿
}

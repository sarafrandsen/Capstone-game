﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    [HideInInspector]
    public Vector2 lastMove;
    public string startPoint;

    private Animator anim;
    private bool isMoving;
    private Rigidbody2D myRigBod;
    private bool isVertAnimActive = true;

    private static bool playerExists;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        myRigBod = GetComponent<Rigidbody2D>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
    void Update () {

        isMoving = false;

        // get the input from the keyboard
        // absolute value keeps from going diagonal
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = isVertAnimActive ? Input.GetAxisRaw("Vertical") : 0f;

        if (horizontal > 0.5f || horizontal < -0.5f)
        {
            myRigBod.velocity = new Vector2(horizontal * moveSpeed, myRigBod.velocity.y);
            isMoving = true;
            lastMove = new Vector2(horizontal, 0);
        }

        if (vertical > 0.5f || vertical < -0.5f)
        {
            myRigBod.velocity = new Vector2(myRigBod.velocity.x, vertical * moveSpeed);
            isMoving = true;
            lastMove = new Vector2(0, vertical);
        }

        if (horizontal < 0.5f && horizontal > -0.5f)
        {
            myRigBod.velocity = new Vector2(0, myRigBod.velocity.y);
        }

        if (vertical < 0.5f && vertical > -0.5f)
        {
            myRigBod.velocity = new Vector2(myRigBod.velocity.x, 0);
        }


        // this tells it the coordinates to move to
        anim.SetBool("IsMoving", isMoving);
        anim.SetFloat("MoveX", horizontal);
        anim.SetFloat("MoveY", vertical);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }

    public void SetOrientation(WorldOrientation worldOrientation)
    {
        if (worldOrientation == WorldOrientation.Overhead)
        {
            isVertAnimActive = true;
            myRigBod.gravityScale = 0;
        }
        else if (worldOrientation == WorldOrientation.SideScroll) {
            isVertAnimActive = false;
            myRigBod.gravityScale = 1;
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{

    public bool isMoving;
    public float moveSpeed;
    public float walkTime; // how long they will be walking
    public float waitTime; // how long they will be waiting
    public Collider2D walkArea;

    private Rigidbody2D npcRigBod;
    private float walkCounter; // count down until they are no longer walking
    private float waitCounter; // count down until they are no longer waiting to move
    private int walkDirection; // direction they will move with directions assigned to ints
    private Vector2 minWalkAreaPt; // lower left corner of box area (min val)
    private Vector2 maxWalkAreaPt; // upper right corner of box area (max val)
    private bool hasWalkArea;
    private SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        npcRigBod = GetComponent<Rigidbody2D>();
        // default waitCounter and waitTime should be equal
        waitCounter = waitTime;
        walkCounter = walkTime;

        SetDirection(); // choose direction, set isMoving to true, reset walkCounter

        if (walkArea != null) // only set the bounds if there is a walk area object set
        {
            hasWalkArea = true;
            minWalkAreaPt = walkArea.bounds.min; // bounds finds the coordinates of the box
            maxWalkAreaPt = walkArea.bounds.max;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            walkCounter -= Time.deltaTime; // countdown the seconds

            // switch statements: given a var, here are the options available for it
            switch (walkDirection)
            {
                case 0: // East
                    npcRigBod.velocity = new Vector2(moveSpeed, 0);

                    if (hasWalkArea && transform.position.x > maxWalkAreaPt.x)
                    {
                        StopMoving();
                    }
                    break;
                case 1: // West
                    npcRigBod.velocity = new Vector2(-moveSpeed, 0);
                    //GetComponent<Sprite>().flipX = true;

                    if (hasWalkArea && transform.position.x < minWalkAreaPt.x)
                    {
                        StopMoving();
                    }
                    break;
            }

            if (walkCounter < 0) // when the countdown for walking completes
            {
                StopMoving();
            }

        }
        else
        {
            waitCounter -= Time.deltaTime; // count down the seconds
            npcRigBod.velocity = Vector2.zero; // Vector2(0, 0)

            if (waitCounter < 0) // when the countdown for waiting completes
            {
                SetDirection(); // choose direction, set isMoving to true, reset walkCounter
            }
        }
    }

    // randomly choose the direction they will move in
    public void SetDirection()
    {
        walkDirection = Random.Range(0, 2); // non-inclusive
        isMoving = true;
        walkCounter = walkTime;
    }

    public void StopMoving()
    {
        isMoving = false; // no longer walking
        waitCounter = waitTime; // reset
    }
}
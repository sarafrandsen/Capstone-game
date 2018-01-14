using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
/*////////////////////////////////////////////*/
	[HideInInspector]
    public Vector2 lastMove; // where the player faced when they stop moving
    public float moveSpeed; // movement speed--for velocity, not anim
    public string startPoint; // where they spawn in a new scene

	private Rigidbody2D myRigBod;
	private Animator anim;
    private SpriteRenderer sprite;
    private bool isMoving; // used by animator/setting velocity
    private bool isVertAnimActive = true; // overhead or side scroll
    private static bool playerExists; // check for player duplicates
    //private Camera theCamera;

    /*////////////////////*/
    public string[] quests;
    public bool isComplete;
    /*////////////////////*/

/*////////////////////////////////////////////*/
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        myRigBod = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        //theCamera = GetComponent<Camera>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else {
            Destroy(gameObject);
        }
	}
	
/*////////////////////////////////////////////*/
	// Update is called once per frame
    void Update () {

        isMoving = false; // don't animate by default

        // get the input from the keyboard
        float horizontal = Input.GetAxisRaw("Horizontal");
        // if isVertAnimAcive == false, up/down input is disabled
        float vertical = isVertAnimActive ? Input.GetAxisRaw("Vertical") : 0f; 

        /*////////////////////////////////////////////
        ////////////////////////////////////////////*/
        if (horizontal > 0.5f || horizontal < -0.5f)
        {
            myRigBod.velocity = new Vector2(horizontal * moveSpeed, myRigBod.velocity.y);
            isMoving = true;
            lastMove = new Vector2(horizontal, 0);

            if (horizontal < -0.5f)
            {
                sprite.flipX = true;
            } 
            else if (horizontal > 0.5f) 
            {
                sprite.flipX = false;
            }
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
        /*////////////////////////////////////////////
        ////////////////////////////////////////////*/

        /*//////////////ANIMATION////////////////*/
        // this tells it the coordinates to move to
        anim.SetBool("IsMoving", isMoving);
        anim.SetFloat("MoveX", horizontal);
        anim.SetFloat("MoveY", vertical);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }

/*////////////////////////////////////////////*/
    // overhead or side scroll
    // WorldOrientation Enum set in PlayerStartPosition class
    public void SetOrientation(WorldOrientation worldOrientation)
    {
        if (worldOrientation == WorldOrientation.Overhead)
        {
            isVertAnimActive = true; // can move up/down
            myRigBod.gravityScale = 0; // turn off gravity
        } else if (worldOrientation == WorldOrientation.SideScroll || worldOrientation == WorldOrientation.Parallax) {
            isVertAnimActive = false; // cannot move up/down
            myRigBod.gravityScale = 10; // gravity on
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public bool canInteract;
    public bool dialogueComplete;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // if canInteract, play bubble interaction
    // if dialogueComplete, play slime melt anim and get rid of rigidbody,
    // access entrance back to main room
}

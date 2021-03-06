﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public DialogueTriggerArea dialogueTriggerArea;
    public GameObject fireToFocusOn;
    public PlayerController thePlayer;

    private GameData gameData;
    private Animator anim;
    private CameraController theCamera;

    private float oldZoomSize;
    private GameObject oldFollowTarget;

    // Use this for initialization
    void Start()
    {
        dialogueTriggerArea.onConversationBegin = ConversationBegin;
        dialogueTriggerArea.onConversationEnd = ConversationEnd;

        thePlayer = PlayerController.Instance;

        gameData = FindObjectOfType<GameData>();
        anim = GetComponent<Animator>();
        theCamera = FindObjectOfType<CameraController>();
    }

    void ConversationBegin()
    {
        oldZoomSize = theCamera.GetComponent<Camera>().orthographicSize;
        oldFollowTarget = theCamera.followTarget;
        theCamera.PopToFollow(thePlayer.gameObject, 5);
    }

    void ConversationEnd()
    {
        dialogueTriggerArea.TurnOff();  

        StartCoroutine(MeltBeforeDisappear());
    }

    IEnumerator MeltBeforeDisappear()
    {
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("DialogueComplete", true); // slime melt anim

        string slime = this.tag;
        gameData.fireDoors[slime] = true; // change value in GameData dictionary

        yield return new WaitForSeconds(1.0f);

        if (fireToFocusOn != null)
        {
            theCamera.PanToFollow(fireToFocusOn, 5);

            // Fire starts
            yield return new WaitForSeconds(2.0f);
            fireToFocusOn.GetComponent<Animator>().SetBool("FireExtinguished", false);
            yield return new WaitForSeconds(2.5f);
        }

        // Reset camera
        theCamera.PopToFollow(oldFollowTarget, oldZoomSize);
        this.gameObject.SetActive(false); // takes object off screen
    }
}

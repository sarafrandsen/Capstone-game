using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public DialogueTriggerArea dialogueTriggerArea;

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
        gameData = FindObjectOfType<GameData>();
        anim = GetComponent<Animator>();
        theCamera = FindObjectOfType<CameraController>();

        Debug.Log(tag);
    }

    void ConversationBegin()
    {
        oldZoomSize = theCamera.GetComponent<Camera>().orthographicSize;
        oldFollowTarget = theCamera.followTarget;
        theCamera.PopToFollow(this.gameObject, 5);
    }

    void ConversationEnd()
    {
        StartCoroutine(MeltBeforeDisappear());
    }

    IEnumerator MeltBeforeDisappear()
    {
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("DialogueComplete", true); // slime melt anim

        string slime = this.tag;
        gameData.fireDoors[slime] = true; // change value in GameData dictionary

        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false); // takes object off screen

        // Move camera to fire

        // Fire starts

        // Reset camera
        theCamera.PopToFollow(oldFollowTarget, oldZoomSize);
    }
}

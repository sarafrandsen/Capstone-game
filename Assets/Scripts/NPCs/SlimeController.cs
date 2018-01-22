using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public DialogueTriggerArea dialogueTriggerArea;
    public GameObject fireToFocusOn;

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
    }

    void ConversationBegin()
    {
        oldZoomSize = theCamera.GetComponent<Camera>().orthographicSize;
        oldFollowTarget = theCamera.followTarget;
        theCamera.PanToFollow(this.gameObject, 5);
    }

    void ConversationEnd()
    {
        dialogueTriggerArea.enabled = !dialogueTriggerArea.enabled;   
        StartCoroutine(MeltBeforeDisappear());
    }

    IEnumerator MeltBeforeDisappear()
    {
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("DialogueComplete", true); // slime melt anim

        string slime = this.tag;
        gameData.fireDoors[slime] = true; // change value in GameData dictionary

        yield return new WaitForSeconds(1.5f);

        if (fireToFocusOn != null)
        {
            // Move camera to fire
            //theCamera.doPanning = true;
            theCamera.PanToFollow(fireToFocusOn, 5);

            // Fire starts
            yield return new WaitForSeconds(1.0f);
            fireToFocusOn.GetComponent<Animator>().SetBool("FireExtinguished", false);
            yield return new WaitForSeconds(1.0f);
        }

        // Reset camera
        theCamera.PanToFollow(oldFollowTarget, oldZoomSize);
        this.gameObject.SetActive(false); // takes object off screen
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public DialogueTriggerArea dialogueTriggerArea;

    private GameData gameData;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        dialogueTriggerArea.onConversationEnd = ConversationEnd;
        gameData = FindObjectOfType<GameData>();
        anim = GetComponent<Animator>();

        Debug.Log(tag);
    }

    void ConversationEnd()
    {
        StartCoroutine(MeltBeforeDisappear());
    }

    IEnumerator MeltBeforeDisappear()
    {
        //yield return new WaitForSeconds(1.0f);
        anim.SetBool("DialogueComplete", true); // slime melt anim

        string slime = this.tag;
        gameData.fireDoors[slime] = true; // change value in GameData dictionary

        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false); // takes object off screen
    }
}

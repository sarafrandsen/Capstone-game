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
        string slime = this.tag;
        gameData.fireDoors[slime] = true; // change value in GameData dictionary
        anim.SetBool("DialogueComplete", true); // slime melt anim
        this.gameObject.SetActive(false); // takes object off screen
    }
}

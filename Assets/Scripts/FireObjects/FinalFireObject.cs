using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFireObject : MonoBehaviour
{
    private GameData gameData;
    private Animator anim;
    private BoxCollider2D boxCollider;

    // Use this for initialization
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim.SetBool("FireExtinguished", true);

        // check in GameData if true or false
        // only accessible if all are true
        string fireDoor = this.tag;
        if (!gameData.fireDoors.ContainsValue(false))
        {
            BuildFire();
        }
    }

    void BuildFire()
    {
        // change animation to lit fire 
        anim.SetBool("FireExtinguished", false);
        // enable 'load new area' script
        boxCollider.isTrigger = true;
    }
}

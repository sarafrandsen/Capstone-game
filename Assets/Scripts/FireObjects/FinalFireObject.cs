using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFireObject : MonoBehaviour
{
    private GameData gameData;
    private Animator anim;
    private LoadNewArea loadNewArea;

    // Use this for initialization
    void Start()
    {
        // opening dialogue with instructions
        // set dialogue as active?

        gameData = FindObjectOfType<GameData>();
        anim = GetComponent<Animator>();
        loadNewArea = GetComponent<LoadNewArea>();

        // check in GameData if true or false
        // only accessible if all are true
        string fireDoor = this.tag;
        if (gameData.fireDoors[fireDoor] == true)
        {
            BuildFire();
        }
    }

    void BuildFire()
    {
        // change animation to lit fire 
        anim.SetBool("FireExtinguished", false);
        // enable 'load new area' script
        loadNewArea.enabled = true;
    }
}

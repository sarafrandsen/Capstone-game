using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFireObject : MonoBehaviour
{
    private GameData gameData;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private CameraController theCamera;
    private PlayerController thePlayer;

    // Use this for initialization
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<CameraController>();
        thePlayer = FindObjectOfType<PlayerController>();
        anim.SetBool("FireExtinguished", true);

        // check in GameData if true or false
        // only accessible if all are true
        string fireDoor = this.tag;
        if (!gameData.fireDoors.ContainsValue(false))
        {
            StartCoroutine(BuildFire());
        }

    }

    IEnumerator BuildFire()
    {
        theCamera.PanToFollow(this.gameObject, 4f);
        yield return new WaitForSeconds(2.0f);
        // change animation to lit fire 
        anim.SetBool("FireExtinguished", false);
        yield return new WaitForSeconds(2.0f);
        // enable 'load new area' script
        boxCollider.isTrigger = true;

        theCamera.PopToFollow(thePlayer.gameObject, 6f);
    }
}

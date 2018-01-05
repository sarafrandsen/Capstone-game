using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //   public GameObject followTarget;
    //   public float moveSpeed; // speed the camera follows target

    //   // camera's target
    //   private Vector3 targetPosition; // z-val can't be zero, default here is -10

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {
    //       targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z); // get the x and y coords of target
    //       transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime); // move camera to the target position
    //       // Lerp = linearly interpolates between two vectors
    //       // V3a = where we are
    //       // V3b = where we want to be
    //       // float t = how fast camera gets there

    //       //if (newPos.x > minX && newPos.x < maxX && newPos.y > minY && newPos.y < maxY)
    //           //transform.position = newPos;
    //}


    public GameObject followTarget;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - followTarget.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = followTarget.transform.position + offset;
    }
}

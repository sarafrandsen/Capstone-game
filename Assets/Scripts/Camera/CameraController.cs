using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public GameObject followTarget;
    private Vector3 offset;
    private static bool cameraExists;

    void Start()
    {
        offset = transform.position - followTarget.transform.position;

        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        transform.position = followTarget.transform.position + offset;
    }
}

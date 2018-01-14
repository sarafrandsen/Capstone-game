using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
/*////////////////////////////////////////////*/
    public BoxCollider2D boundsBox; // where camera should stop
    public GameObject followTarget; // what the camera should center on (player)

    private Camera theCamera;
    private float halfHeight; // half the view height
    private float halfWidth; // half the view width
    private Vector3 minBounds; // lower-left point of boundsBox
    private Vector3 maxBounds; // upper-right point of boundsBox
    private Vector3 offset; 
    private static bool cameraExists; // check for duplicate cameras

/*////////////////////////////////////////////*/
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

        minBounds = boundsBox.bounds.min;
        maxBounds = boundsBox.bounds.max;
        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

/*////////////////////////////////////////////*/
    // keep camera in scene bounds
    private void LateUpdate()
    {
        transform.position = followTarget.transform.position + offset; // follow the target (player)
        // clamp: given a min val and a max val, make sure the current vals do not exceed them
        // Clamp(value, min, max)
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        // new camera position using the clamped values
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        if (boundsBox == null)
        {
            boundsBox = FindObjectOfType<CameraBounds>().GetComponent<BoxCollider2D>();
            minBounds = boundsBox.bounds.min;
            maxBounds = boundsBox.bounds.max;
        }
    }

    public void SetBounds(BoxCollider2D newBounds)
    {
        boundsBox = newBounds;
        minBounds = boundsBox.bounds.min;
        maxBounds = boundsBox.bounds.max;
    }

    /*////////////////////////////////////////////*/
    // overhead or side scroll
    // WorldOrientation Enum set in PlayerStartPosition class
    public void SetOrientation(WorldOrientation worldOrientation)
    {
        if (worldOrientation == WorldOrientation.Overhead)
        {
            theCamera.orthographic = true; // camera is orthographic
            theCamera.orthographicSize = 6;
            // disable second camera
        }
        else if (worldOrientation == WorldOrientation.SideScroll)
        {
            theCamera.orthographic = true;
            theCamera.orthographicSize = 4;
            // disable second camera
        } 
        else
        {
            // theCamera.orthographic = true and false;
            // enable second camera
            // set fields needed for parallax view
        }
    }
}

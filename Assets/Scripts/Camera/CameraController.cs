using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*////////////////////////////////////////////*/
    public BoxCollider2D boundsBox; // where camera should stop
    public GameObject followTarget; // what the camera should center on (player)

    private Camera theCamera;
    private static bool cameraExists; // check for duplicate cameras
    private float halfHeight; // half the view height
    private float halfWidth; // half the view width
    private Vector3 minBounds; // lower-left point of boundsBox
    private Vector3 maxBounds; // upper-right point of boundsBox
    private Vector3 offset; // z-axis (-10)

    public bool doPanning = false;
    private Vector3 panStart; //New - Jonathan
    private Vector3 panEnd; //New - Jonathan
    //private float zoomTarget;
    //private float zoomVelocity;
    //private Vector3 panVelocity;
    /*////////////////////////////////////////////*/
    void Start()
    {
        offset = new Vector3(0, 0, -10);

        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        minBounds = boundsBox.bounds.min;
        maxBounds = boundsBox.bounds.max;
        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

        //zoomTarget = theCamera.orthographicSize;
    }

    /*////////////////////////////////////////////*/
    private void LateUpdate()
    {
        // keep camera in scene bounds
        if (boundsBox == null)
        {
            boundsBox = FindObjectOfType<CameraBounds>().GetComponent<BoxCollider2D>();
            minBounds = boundsBox.bounds.min;
            maxBounds = boundsBox.bounds.max;
        }

        // clamp: given a min val and a max val, make sure the current vals do not exceed them
        // Clamp(value, min, max)
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        // new camera position using the clamped values
        if (doPanning)
        {
            transform.position = Vector3.Lerp(panStart, panEnd, Time.deltaTime * 1f); // New -Jonathan
            if (transform.position == panEnd) // New - Jonathan
            {
                doPanning = false; // New - Jonathan
            }
        }
        else
        {
            transform.position = followTarget.transform.position + offset; // follow the target (player or BG)
        }
        //else
        //{
        //    doPanning = false;

        //    Vector3 newTargetPos = new Vector3(clampedX, clampedY, transform.position.z);
        //    transform.position = Vector3.SmoothDamp(transform.position, newTargetPos, ref panVelocity, 1f);
        //    theCamera.orthographicSize = Mathf.SmoothDamp(theCamera.orthographicSize, zoomTarget, ref zoomVelocity, 0.2f);
        //}
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
    public void SetOrientation(WorldOrientation worldOrientation, GameObject newFollowTarget)
    {
        if (worldOrientation == WorldOrientation.Overhead)
        {
            PopToFollow(newFollowTarget, 6);
        }
        else if (worldOrientation == WorldOrientation.SideScroll)
        {
            PopToFollow(newFollowTarget, 15);
        }
    }

    public void PopToFollow(GameObject newFollowTarget, float newSize)
    {
        //panVelocity = Vector3.zero;
        //zoomVelocity = 0;

        followTarget = newFollowTarget;
        theCamera.orthographicSize = newSize;

        doPanning = false;
    }

    public void PanToFollow(GameObject newFollowTarget, float newSize)
    {
        //panVelocity = Vector3.zero;
        //zoomVelocity = 0.1f;

        //zoomTarget = newSize;
        theCamera.orthographicSize = newSize;
        followTarget = newFollowTarget;

        panStart = followTarget.transform.position; // New -Jonathan
        panEnd = newFollowTarget.transform.position; // New - Jonathan

        doPanning = true;
    }
}
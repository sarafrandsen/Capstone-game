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

    private Vector3 minBounds; // lower-left point of boundsBox
    private Vector3 maxBounds; // upper-right point of boundsBox
    private Vector3 offset; // z-axis (-10)

    public bool doPanning = false;
    public float secondsToPan = 2f;
    private Vector3 panStart; //New - Jonathan
    private Vector3 panEnd; //New - Jonathan
    private float zoomStart;
    private float zoomEnd;
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
    }

    private float time = 0f;

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

        // new camera position using the clamped values
        if (doPanning)
        {
            time += Time.deltaTime;
            float t = Mathf.Min(time/secondsToPan, 1f);
            transform.position = Vector3.Lerp(panStart, panEnd, t);
            theCamera.orthographicSize = Mathf.Lerp(zoomStart, zoomEnd, t);
            if (transform.position == panEnd)
            {
                doPanning = false;
            }
        }
        else
        {
            transform.position = followTarget.transform.position + offset; // follow the target (player or BG)
        }
		
        // clamp: given a min val and a max val, make sure the current vals do not exceed them
        // Clamp(value, min, max)
        float halfHeight = theCamera.orthographicSize;
        float halfWidth = halfHeight * Screen.width / Screen.height;
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
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
        followTarget = newFollowTarget;
        theCamera.orthographicSize = newSize;

        doPanning = false;
    }

    public void PanToFollow(GameObject newFollowTarget, float newSize)
    {
		panStart = followTarget.transform.position;
        panStart.z = -10f;
		panEnd = newFollowTarget.transform.position;
        panEnd.z = -10f;

        zoomStart = theCamera.orthographicSize;
        zoomEnd = newSize;
        followTarget = newFollowTarget;

        doPanning = true;
        time = 0f;
    }
}
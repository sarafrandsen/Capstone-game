using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public BoxCollider2D boundsBox;
    public GameObject followTarget;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;
    private Vector3 minBounds;
    private Vector3 maxBounds;
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

        minBounds = boundsBox.bounds.min;
        maxBounds = boundsBox.bounds.max;
        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    private void LateUpdate()
    {
        transform.position = followTarget.transform.position + offset;
        // clamp: given a min val and a max val, make sure the current vals do not exceed
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        if (boundsBox == null)
        {
            boundsBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
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
}

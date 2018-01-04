using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Direction currentDir; // accessor for player movement
    Vector2 input; // input from directional keys (UP DOWN LEFT RIGHT,  A W S D)
    bool isMoving = false; // toggle movement with player input
    Vector3 startPos; // start position
    Vector3 endPos; // end position
    float t; // time

    public float walkSpeed = 3f; // higher number = faster

    void FixedUpdate()
    {
        // if character is not moving, check if keys are being pressed
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // gets key input (UP DOWN LEFT RIGHT, A W S D)
            // keep from moving diagonally:
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.y = 0;
            }
            else
            {
                input.x = 0;
            }

            if (input != Vector2.zero) // what is V2.zero?
            {
                StartCoroutine(Move(transform)); // what is Coroutine?
            }
        }
    }

    public IEnumerator Move(Transform entity) // what is IEnumerable?
    {
        isMoving = true;
        startPos = entity.position;
        t = 0;

        endPos = new Vector3(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y), startPos.z); // what the eff does this mean

        while (t < 1f)
        {
            t += Time.deltaTime * walkSpeed;
            entity.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        isMoving = false;
        yield return 0;
    }

}

enum Direction
{
    Noth,
    East,
    South,
    West
}
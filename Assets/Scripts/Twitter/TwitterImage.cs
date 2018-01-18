using UnityEngine;
using System.Collections;

// Get the latest webcam shot from outside "Friday's" in Times Square
public class TwitterImage : MonoBehaviour
{
    private SpriteRenderer dialogueImage;

    void Start()
    {
        dialogueImage = GetComponent<SpriteRenderer>();
        StartCoroutine(LoadSpriteImage());
    }

    IEnumerator LoadSpriteImage()
    {
        string url = "http://pbs.twimg.com/profile_images/834151634628288512/8BOBiWxh_normal.jpg";
        var www = new WWW(url);
        Debug.Log("Profile image download in progress");
        yield return www;

        Texture2D texture = new Texture2D(1, 1);
        www.LoadImageIntoTexture(texture);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
        dialogueImage.sprite = sprite;
    }
}
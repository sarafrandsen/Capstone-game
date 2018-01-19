using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Get the latest webcam shot from outside "Friday's" in Times Square
public class TwitterImage : MonoBehaviour
{
    private Image profileImage;

    void Start()
    {
        profileImage = GetComponent<Image>();

        StartCoroutine(LoadProfileImage());
    }

    IEnumerator LoadProfileImage()
    {
        //string url = dialogueManager.urlMagicBot;
        string url = "http://pbs.twimg.com/profile_images/668872745329885184/67TNOs2A.png";
        var www = new WWW(url);
        yield return www;

        Texture2D texture = new Texture2D(100, 100);
        www.LoadImageIntoTexture(texture);
        Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
        profileImage.sprite = newSprite;
    }
}
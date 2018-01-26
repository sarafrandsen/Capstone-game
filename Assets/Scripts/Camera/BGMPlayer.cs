using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {

    public AudioSource player;

    public static BGMPlayer Instance { get; private set; }

    public void PlayMusic(AudioClip newClip)
    {
        player.clip = newClip;
        player.Play();
    }

    public void Stop()
    {
        player.Stop();
    }

    public void SetVolume(float volume)
    {
        player.volume = volume;
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioSource btnsource;
    private void Start()
    {
        bgm.Play();
    }

    public void SetMusicVolume(float volume)
    {
        bgm.volume = volume;
    }

    public void StartButton()
    {
        btnsource.Play();
    }

}

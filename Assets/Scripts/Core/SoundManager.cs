using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance
    {
        get; private set;
        // get: getter method
        // private set: private setter
    }

    private AudioSource source; 

    private void Awake()
    {
        instance = this; // reference current instance of soundmanager
        source = GetComponent<AudioSource>(); //gets audiosource attached to gameobject
    }

    public void PlaySound(AudioClip sound) //function to play sound
    {
        source.PlayOneShot(sound); //gets the soundclip from object
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;

    public static TravelManager instance;

    void Start()
    {
        // Only will have one game manager
        instance = this;
    }

    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
    }
}

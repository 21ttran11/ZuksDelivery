using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public float delayMusic = 1f;
    public string sfxString = "s_enter";
    public string musicString = "m_post";

    private void Start()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.StopMusic();
            AudioManager.PlaySFX(sfxString, 1.0f);
        }
            
        StartCoroutine(PlayMusicWithDelay(delayMusic));
    }

    private IEnumerator PlayMusicWithDelay(float delay)
    {
        yield return new WaitForSeconds(delayMusic);

        if (AudioManager.instance != null)
            AudioManager.PlayMusic(musicString, 0.5f);
    }
}

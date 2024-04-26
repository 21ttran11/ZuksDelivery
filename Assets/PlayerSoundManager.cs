using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public float delayMusic = 1f;

    private void Start()
    {
        if (AudioManager.instance != null)
            AudioManager.PlaySFX("s_enter", 1.0f);
        StartCoroutine(PlayMusicWithDelay(delayMusic));
    }

    private IEnumerator PlayMusicWithDelay(float delay)
    {
        yield return new WaitForSeconds(delayMusic);

        if (AudioManager.instance != null)
            AudioManager.PlayMusic("m_post", 0.5f);
    }
}

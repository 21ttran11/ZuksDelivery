using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AudioClipEntry
{
    public string key;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private List<AudioClipEntry> musicEntries;
    [SerializeField]
    private List<AudioClipEntry> sfxEntries;

    private Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeAudioDictionaries();
    }

    private void InitializeAudioDictionaries()
    {
        foreach (AudioClipEntry entry in musicEntries)
        {
            musicClips[entry.key] = entry.clip;
        }
        foreach (AudioClipEntry entry in sfxEntries)
        {
            sfxClips[entry.key] = entry.clip;
        }
    }

    public static void PlayMusic(string name, float volume = 0.5f)
    {
        if (instance.musicClips.TryGetValue(name, out AudioClip clip))
        {
            instance.musicSource.clip = clip;
            instance.musicSource.volume = volume;
            instance.musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip not found: " + name);
        }
    }

    public static void PlaySFX(string name, float volume = 1.0f, bool loop = false, float pitch = 1.0f)
    {
        if (instance.sfxClips.TryGetValue(name, out AudioClip clip))
        {
            instance.sfxSource.clip = clip;
            instance.sfxSource.volume = volume;
            instance.sfxSource.loop = loop;
            instance.sfxSource.pitch = pitch;
            instance.sfxSource.Play();
        }
        else
        {
            Debug.LogWarning("SFX clip not found: " + name);
        }
    }

    public static void StopSFX()
    {
        instance.sfxSource.Stop();
        instance.sfxSource.pitch = 1.0f;
    }

    public static void StopMusic()
    {
        instance.musicSource.Stop();
    }

}
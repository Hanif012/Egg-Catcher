using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> musicDict;
    private Dictionary<string, AudioClip> sfxDict;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        musicDict = new Dictionary<string, AudioClip>();
        foreach (var clip in musicClips) {
            musicDict[clip.name] = clip;
        }

        sfxDict = new Dictionary<string, AudioClip>();
        foreach (var clip in sfxClips) {
            sfxDict[clip.name] = clip;
        }
    }

    public void PlayMusic(string name) {
        if (musicDict.TryGetValue(name, out var clip)) {
            musicSource.clip = clip;
            musicSource.Play();
        } else {
            Debug.LogWarning("Music clip not found: " + name);
        }
    }

    public void PlaySFX(string name) {
        if (sfxDict.TryGetValue(name, out var clip)) {
            sfxSource.clip = clip;
            sfxSource.Play();
        } else {
            Debug.LogWarning("SFX clip not found: " + name);
        }
    }
}
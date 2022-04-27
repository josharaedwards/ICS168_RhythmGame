using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] public AudioSource playbackMix;
    [SerializeField] private AudioSource sfxMix;
    [SerializeField] private AudioSource uiMix;

    private void Awake()
    {
        if(instance && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlaySong(AudioClip song)
    {
        if(playbackMix.isPlaying)
        {
            playbackMix.Stop();
        }

        playbackMix.clip = song;
        playbackMix.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        sfxMix.PlayOneShot(sfx);
    }

    public void PlayUI(AudioClip ui)
    {
        uiMix.PlayOneShot(ui);
    }
}

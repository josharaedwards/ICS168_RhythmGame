using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private AudioSource playbackMix;
    [SerializeField] private AudioSource sfxMix;
    [SerializeField] private AudioSource uiMix;

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Init()
    {
        if (instance && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject.transform.root);
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

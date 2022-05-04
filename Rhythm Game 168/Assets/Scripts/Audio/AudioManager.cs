using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private AudioSource playbackMix;
    [SerializeField] private AudioSource sfxMix;
    [SerializeField] private AudioSource uiMix;

    private float currentSongTime;
    private float currentSongPercentage;

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        
    }

    void Update()
    {
        currentSongTime = playbackMix.time;
        currentSongPercentage = playbackMix.time / playbackMix.clip.length;

        Debug.Log("Current song time is " + currentSongTime);
        Debug.Log("Current song percent is " + currentSongPercentage);
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

    public float GetCurrentSongTime()
    {
        if(playbackMix.isPlaying)
        {
            return currentSongTime;
        }

        return 0f;
    }

    public float GetCurrentSongPercentage()
    {
        if(playbackMix.isPlaying)
        {
            return currentSongPercentage;
        }

        return 0f;
    }

    public void SetSongTime(float songPercent)
    {
        if(songPercent < 0f)
        {
            songPercent = 0f;
        }
        else if (songPercent > 1f)
        {
            songPercent = 1f;
        }

        playbackMix.Pause();
        playbackMix.time = 0f;
        playbackMix.time = playbackMix.clip.length * songPercent;
        playbackMix.Play();
    }
}

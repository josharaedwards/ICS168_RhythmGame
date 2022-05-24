using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private AudioSource playbackMix;
    [SerializeField] private AudioSource sfxMix;
    [SerializeField] private AudioSource uiMix;
    [SerializeField] private AudioSource beatMix;

    private float currentSongTime = 0.0f;
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
        if(playbackMix.isPlaying)
        {
            currentSongTime = playbackMix.time;
            currentSongPercentage = playbackMix.time / playbackMix.clip.length;
        }
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

    public void PlaySong(AudioClip song, bool play=true)
    {
        // if(playbackMix.isPlaying)
        // {
        //     playbackMix.Stop();
        // }

        playbackMix.clip = song;
        if (play)
        {
            playbackMix.Play();
        }
    }

    public void PlaySFX(AudioClip sfx)
    {
        sfxMix.PlayOneShot(sfx);
    }

    public void PlayUI(AudioClip ui)
    {
        uiMix.PlayOneShot(ui);
    }

    public void PlayBeat(AudioClip beat)
    {
        beatMix.PlayOneShot(beat);
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


    public void Play()
    {
        playbackMix.Play();
    }

    public void Pause()
    {
        playbackMix.Pause();
    }

    public void Replay()
    {
        playbackMix.time = 0.0f;
        playbackMix.Stop();
        playbackMix.Play();
    }

    // Jump seconds forward
    public bool JumpTime(float seconds)
    {
        bool jumped;

        bool wasPlaying = playbackMix.isPlaying;
        playbackMix.Pause();

        float currentTime = playbackMix.time;
        playbackMix.time = 0f;
        float time = currentTime + seconds;
        if (time <= 0f || time >= playbackMix.clip.length)
        {
            playbackMix.time = currentTime;
            jumped = false;
        }
        else
        {
            playbackMix.time = time;
            jumped = true;
        }
        
        if (wasPlaying)
        {
            playbackMix.Play();
        }

        return jumped;
    }

    public void SetSongPercent(float songPercent)
    {
        if (songPercent < 0f)
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

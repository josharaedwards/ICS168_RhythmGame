using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
public class BeatmapController : MonoBehaviour
{
    [SerializeField] private bool started = false;

    [SerializeField] private float beatPerMinute;
    private float beatPerSecond;

    [SerializeField] private SongData song;
    AudioManager audioManager;


    // Start is called before the first frame update
    void Awake()
    {
        beatPerMinute = song.bpm;
        beatPerSecond = beatPerMinute / 60f;
    }

    void Start()
    {
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!started)
        {
            if (Input.anyKeyDown)
            {
                started = true;
                audioManager.PlaySong(song.songClip);
            }
        }
        else
        {
            transform.position -= new Vector3(0.0f, beatPerSecond * Time.fixedDeltaTime, 0.0f);
        }
    }
}

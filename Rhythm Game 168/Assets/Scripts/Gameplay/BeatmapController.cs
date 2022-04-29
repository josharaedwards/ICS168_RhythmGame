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

    [SerializeField] [Range(1.0f, 5.0f)] private float highwaySpeed = 1;

    private Transform[] beatPositions;

    // Start is called before the first frame update
    void Awake()
    {
        beatPositions = GetComponentsInChildren<Transform>();

        for(int i = 1; i < beatPositions.Length; i++)
        {
            Debug.Log("changed");
            beatPositions[i].localPosition = Vector3.Scale(beatPositions[i].localPosition, new Vector3(1.0f, highwaySpeed, 1.0f));
        }

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
            transform.position -= new Vector3(0.0f, beatPerSecond * highwaySpeed * Time.fixedDeltaTime, 0.0f);
        }
    }
}

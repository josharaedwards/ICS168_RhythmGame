using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
public class BeatmapController : MonoBehaviour
{
    [SerializeField] private bool editMode = true;

    [SerializeField] private bool started = false;

    [SerializeField] private bool isPlaying = false;

    [SerializeField] private float beatPerMinute;
    private float beatPerSecond, secondsPerBeat;

    [SerializeField] private SongData song;
    AudioManager audioManager;

    [SerializeField] [Range(1.0f, 5.0f)] private float highwaySpeed = 1;

    private Transform[] beatPositions;

    private Vector3 initPos;

    // Start is called before the first frame update
    void Awake()
    {
        beatPositions = GetComponentsInChildren<Transform>();

        for(int i = 1; i < beatPositions.Length; i++)
        {
            //Debug.Log("changed");
            beatPositions[i].localPosition = Vector3.Scale(beatPositions[i].localPosition, new Vector3(1.0f, highwaySpeed, 1.0f));
        }

        beatPerMinute = song.bpm;
        beatPerSecond = beatPerMinute / 60f;
        // secondsPerBeat = 60f/ beatPerMinute;
    }

    void Start()
    {
        initPos = transform.position;
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!started)
        {
            if (Input.anyKeyDown)
            {
                isPlaying = true;
                started = true;
                audioManager.PlaySong(song.songClip);
            }
        }
        if (isPlaying)
        {
            transform.localPosition -= transform.rotation * (new Vector3(0.0f, beatPerSecond * highwaySpeed * Time.fixedDeltaTime, 0.0f));
        }
    }

    void Update()
    {
        BeatmapEditInput();
    }

    private void BeatmapEditInput()
    {
        if (!editMode || !started)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            for(int i = 1; i < beatPositions.Length; i++)
            {
                beatPositions[i].gameObject.SetActive(true);
            }
            JumpBeat(-16);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            JumpBeat(16);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            for(int i = 1; i < beatPositions.Length; i++)
            {
                beatPositions[i].gameObject.SetActive(true);
            }
            JumpBeat(-4);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            JumpBeat(4);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    // Move beatmap beats
    private void MoveBeatmap(int beats)
    {
        transform.localPosition -= transform.rotation * (new Vector3(0.0f, beats * highwaySpeed, 0.0f));
    }

    // Jump beats on both song and beatmap
    private void JumpBeat(int beats)
    {
        float jumpTime = beats / beatPerSecond;
        bool jumped = audioManager.JumpTime(jumpTime);

        if (jumped)
        {
            MoveBeatmap(beats);
        }
    }

    private void Play()
    {
        isPlaying = true;
        audioManager.Play();
    }

    private void Pause()
    {
        isPlaying = false;
        audioManager.Pause();
    }

    private void Restart()
    {
        for(int i = 1; i < beatPositions.Length; i++)
        {
            beatPositions[i].gameObject.SetActive(true);
        }
        transform.position = initPos;
        audioManager.Replay();
    }
}

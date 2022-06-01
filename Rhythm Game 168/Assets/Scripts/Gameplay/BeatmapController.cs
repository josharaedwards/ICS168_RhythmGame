using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using TMPro;

public class BeatmapController : MonoBehaviour
{
    [SerializeField] private bool editMode = true;

    public bool started = false;

    public bool paused = false;

    [SerializeField] private bool isPlaying = false;

    [SerializeField] private float beatPerMinute;
    private float beatPerSecond, secondsPerBeat;

    [SerializeField] private SongData song;
    AudioManager audioManager;

    [SerializeField] [Range(1.0f, 5.0f)] private float highwaySpeed = 1;

    // [SerializeField] private TextMeshProUGUI countdownText;

    // [SerializeField] private Animation countdownAnim;

    private Transform[] beatPositions;

    private Vector3 initPos;

    private BeatmapLoader beatmapLoader;
    // Start is called before the first frame update
    void Awake()
    {
        // secondsPerBeat = 60f/ beatPerMinute;
    }

    void Start()
    {
        song = GameManager.instance.currentSong;
        beatPerMinute = song.bpm;
        beatPerSecond = beatPerMinute / 60f;

        beatmapLoader = GetComponent<BeatmapLoader>();
        beatmapLoader.setBeatmapName(song.beatmapName);
        beatmapLoader.Clear();
        beatmapLoader.Load();

        beatPositions = GetComponentsInChildren<Transform>();
        for (int i = 1; i < beatPositions.Length; i++)
        {
            //Debug.Log("changed");
            beatPositions[i].localPosition = Vector3.Scale(beatPositions[i].localPosition, new Vector3(1.0f, highwaySpeed, 1.0f));
        }

        initPos = transform.position;
        audioManager = AudioManager.instance;

        
        // PlayerManager.allPlayersReady += PlayersNowReady; 
        // CountdownScript.countdownEnded += CountdownEnded; 
        PlayerManager.playersStartPlaying += StartBeatmap; //<---- Observer Pattern (subscribing)
    }

    void OnDestroy()
    {
        PlayerManager.playersStartPlaying -= StartBeatmap;  //<---- Observer Pattern (unsubscribing)
        // CountdownScript.countdownEnded += CountdownEnded; 
    }

    private void StartBeatmap(PlayerManager sub)
    {
        started = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (editMode)
        {
            started = true;
        }   

        if (started)
        {
            if(!isPlaying)
            {
                isPlaying = true;
                audioManager.PlaySong(song.songClip);
                if(editMode)
                {
                    Pause();
                }
            }
            
        }
        if (isPlaying && !paused)
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
            for (int i = 1; i < beatPositions.Length; i++)
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
            for (int i = 1; i < beatPositions.Length; i++)
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
            if (!paused)
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

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.G))
        {
            beatmapLoader.Save();
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
        paused = false;
        audioManager.Play();
    }

    private void Pause()
    {
        paused = true;
        audioManager.Pause();
    }

    private void Restart()
    {
        for (int i = 1; i < beatPositions.Length; i++)
        {
            
            beatPositions[i].gameObject.SetActive(true);
        }
        transform.position = initPos;
        audioManager.Replay();
    }
}

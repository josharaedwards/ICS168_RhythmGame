using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    [SerializeField] private SongData song;
    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        audioManager.PlaySong(song.songClip);
    }
}

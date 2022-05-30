using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class SongData : ScriptableObject
{
    public string songTitle;
    public string artistName;
    public float bpm;
    public AudioClip songClip;
    public Sprite albumCover;

    public string difficulty;
    public Sprite songButton;

    public string beatmapName;
}

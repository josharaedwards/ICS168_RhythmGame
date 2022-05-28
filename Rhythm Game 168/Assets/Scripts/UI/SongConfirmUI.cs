using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongConfirmUI : RGButton
{
    private SongData songOut;

    private void OnEnable()
    {
        Setup();

        SongListButton.songButtonClicked += ReadySong;
    }

    private void OnDisable()
    {
        SongListButton.songButtonClicked -= ReadySong;
    }

    private void ReadySong(SongData song)
    {
        songOut = song;
    }

    protected override void OnRGButtonClick()
    {
        base.OnRGButtonClick();
        GameManager.instance.SetCurrentSong(songOut);
    }
}

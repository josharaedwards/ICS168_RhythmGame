using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongListButton : RGButton
{
    public delegate void SongButtonClicked(SongData buttonSong);
    public static event SongButtonClicked songButtonClicked;

    private SongData mySong;

    void Start()
    {

    }

    public void Init(SongData song)
    {
        Setup();
        mySong = song;
        SetupButtonText(mySong);
    }

    protected override void OnRGButtonClick()
    {
        base.OnRGButtonClick();
        songButtonClicked(mySong);
    }

    private void SetupButtonText(SongData song)
    {
        string buttonText;

        buttonText = song.songTitle + " - " + song.artistName;
        self.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
    }
}

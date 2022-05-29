using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongListButton : RGButton
{
    public delegate void SongButtonClicked(SongData buttonSong);
    public static event SongButtonClicked songButtonClicked;

    private SongData mySong;
    [SerializeField] private Image myImage;

    void Start()
    {

    }

    public void Init(SongData song)
    {
        Setup();

        mySong = song;
        SetupButtonVisuals(mySong.songButton);
    }

    protected override void OnRGButtonClick()
    {
        base.OnRGButtonClick();
        songButtonClicked(mySong);
        GameManager.instance.SetCurrentSong(mySong);
    }

    //Deprecated due to using UI art for song title
    private void SetupButtonText(SongData song)
    {
        string buttonText;

        buttonText = song.songTitle + " - " + song.artistName;
        self.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
    }

    private void SetupButtonVisuals(Sprite songButtonimg)
    {
        if(myImage)
        {
            myImage.sprite = songButtonimg;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlbumInfoTextUI : MonoBehaviour
{
    private TextMeshProUGUI textObj;

    private void OnEnable()
    {
        SongListButton.songButtonClicked += ChangeAlbumInfoText;
        textObj = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        SongListButton.songButtonClicked -= ChangeAlbumInfoText;
    }

    private void ChangeAlbumInfoText(SongData song)
    {
        string albumInfo;

        albumInfo = " " + song.songTitle + "\n  by " + song.artistName;
        textObj.text = albumInfo;
    }
}

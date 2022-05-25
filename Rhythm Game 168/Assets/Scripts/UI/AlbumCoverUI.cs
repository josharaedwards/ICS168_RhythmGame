using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumCoverUI : MonoBehaviour
{
    private Image albumCover;

    private void OnEnable()
    {
        SongListButton.songButtonClicked += ChangeAlbumImage;
        albumCover = GetComponent<Image>();
    }

    private void OnDisable()
    {
        SongListButton.songButtonClicked -= ChangeAlbumImage;
    }

    void ChangeAlbumImage(SongData song)
    {
        albumCover.sprite = song.albumCover;
    }
}

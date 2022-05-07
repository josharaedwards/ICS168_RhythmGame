using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongListButton : RGButton
{
    void Start()
    {

    }

    public void Init(SongData song)
    {
        Setup();
        self.GetComponentInChildren<TextMeshProUGUI>().text = song.songTitle;
    }
}

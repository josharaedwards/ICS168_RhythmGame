using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongSelectDisplay : MonoBehaviour
{
    [SerializeField] private GameObject songButtonPrototype;
    [SerializeField] private GameObject songListContainer;  

    void Awake()
    {
        UpdateSongList();
    }

    void Update()
    {
        
    }

    public void UpdateSongList()
    {
        SongData[] songList = GameManager.instance.songList;
        int numOfSongs = songList.Length;

        for (int i = 0; i < numOfSongs; ++i)
        {
            GameObject songButton = CreateSongButton(songList[i]);

            if(i == 0)
            {
                ReadyFirstSong(songButton);
            }
        }
    }

    private GameObject CreateSongButton(SongData song)
    {
        GameObject songButton = Instantiate(songButtonPrototype, songListContainer.transform);
        songButton.GetComponent<SongListButton>().Init(song);

        return songButton;
    }

    private void ReadyFirstSong(GameObject songButton)
    {
        songButton.GetComponent<SongListButton>().ClickFirstButton();
    }
}

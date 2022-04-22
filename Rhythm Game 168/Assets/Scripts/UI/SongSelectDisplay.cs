using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongSelectDisplay : MonoBehaviour
{
    public int numOfSongs = 25;

    [SerializeField] private GameObject songButtonPrototype;
    [SerializeField] private GameObject songListContainer;  

    void Start()
    {
        UpdateSongList();
    }

    void Update()
    {
        
    }

    public void UpdateSongList()
    {
        for(int i = 0; i < numOfSongs; ++i)
        {
            CreateSongButton();
        }
    }

    private void CreateSongButton()
    {
        Instantiate(songButtonPrototype, songListContainer.transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyTextUI : MonoBehaviour
{
    
    private void OnEnable()
    {
        SongListButton.songButtonClicked += ChangeDifficultyText;
    }


    private void OnDisable()
    {
        SongListButton.songButtonClicked -= ChangeDifficultyText;
    }
    
    private void ChangeDifficultyText(SongData song)
    {
        Debug.Log("Difficulty: " + "Easy");
    }
}

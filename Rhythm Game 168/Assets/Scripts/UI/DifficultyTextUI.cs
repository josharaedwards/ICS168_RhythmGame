using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyTextUI : MonoBehaviour
{
    private TextMeshProUGUI textObj;
    
    private void OnEnable()
    {
        SongListButton.songButtonClicked += ChangeDifficultyText;
        textObj = GetComponent<TextMeshProUGUI>();
    }


    private void OnDisable()
    {
        SongListButton.songButtonClicked -= ChangeDifficultyText;
    }
    
    private void ChangeDifficultyText(SongData song)
    {
        string difficultyText = " Difficulty: ";
        textObj.text = difficultyText + song.difficulty;
    }
}

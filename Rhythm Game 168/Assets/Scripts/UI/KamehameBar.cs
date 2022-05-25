using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KamehameBar : MonoBehaviour
{
    private int totScore = 0;

    [SerializeField] private Image KamehameBarUI;
    public static event Action<int> totScoreChange;
    [SerializeField] private TextMeshProUGUI totScoreText;

    private PlayerStats playerTwo;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.addToTotalScore += addToScore;
    }

    void OnDestroy()
    {
        PlayerStats.addToTotalScore -= addToScore;
    }

    // Update is called once per frame
    private void addToScore(int addScore)
    {
        if (playerTwo == null)
        {
            playerTwo = GameObject.FindWithTag("Player2").GetComponent<PlayerStats>();
        }    
        
        totScore += addScore;
        totScoreText.text = totScore.ToString();
        KamehameBarUI.fillAmount = 1.0f - (float)playerTwo.score/(float)totScore;
        totScoreChange(totScore);
        
    }
}

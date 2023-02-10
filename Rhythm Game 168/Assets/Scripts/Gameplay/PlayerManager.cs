using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static event Action<PlayerManager> allPlayersReady, playersStartPlaying;

    private PlayerInputManager manager;

   

    [SerializeField] private int playerCount = 1; //MUST set in editor for single vs multiplayer
    private int playerNumber = 0;
    private int totalScore = 0;

    private int PlayersDead = 0;
    // private int PlayersReady = 0;

    [SerializeField]
    private GameObject[] playerPrefabs;

    private GameObject[] playerStats;
    

    // public static PlayerManager instance;
    
    void Awake()
    {
        manager = GetComponent<PlayerInputManager>();

        KamehameBar.totScoreChange += TotalScore;

        manager.playerPrefab = playerPrefabs[playerNumber]; 

        
    }

    void Start()
    {
        PlayerPrefs.DeleteAll();


        PlayerStats.imDead += LoseGameCheck; //<---- Observer Pattern (subscribing)
        PlayerStats.wonMyGame += wonGame;
        // PlayerStats.iJoinedIn += PlayersReadyCheck;
        CountdownScript.countdownEnded += CountdownEnded; 
        
        playerStats = playerPrefabs;
        // PlayerStats.totScoreChange += TotalScore;
        if(playerCount == 1)
        {
            manager.gameObject.SetActive(false);
            
            allPlayersReady(this);
            
        }
    }

    private void OnDestroy()
    {
        PlayerStats.imDead -= LoseGameCheck; //<---- Observer Pattern (unsubscribing)
        PlayerStats.wonMyGame -= wonGame;
        // PlayerStats.iJoinedIn -= PlayersReadyCheck;
        KamehameBar.totScoreChange -= TotalScore;
        CountdownScript.countdownEnded -= CountdownEnded; 
    }

    // void PlayersReadyCheck(PlayerStats sub)
    // {
    //     PlayersReady += 1;
    //     if(PlayersReady >= playerCount)
    //     {
            
    //     }
    // }

    private void CountdownEnded(CountdownScript sub)
    {
        playersStartPlaying(this);
    }

    private void LoseGameCheck(PlayerStats sub)
    {
        PlayersDead += 1;
        if (PlayersDead >= playerCount)
        {
            GameOver();
            AudioManager.instance.Stop();
            LevelLoaderScript.instance.LoadNextSceneFromDead("ResultsScreen");
        }
    }

    private void wonGame(PlayerStats sub)
    {
        GameOver();
        LevelLoaderScript.instance.LoadNextSceneFromWin("ResultsScreen");
    }

    private void TotalScore(int totScore)
    {
        totalScore = totScore;
    }

    private void GameOver()
    {
        PlayerPrefs.SetInt("TotalScore", totalScore);

        for(int i = 0; i < playerStats.Length; i++) //Get Scores from each player to put into results screen.
        {
            Debug.Log("Collecting Data!! for: " + playerStats[i]);
            PlayerPrefs.SetString("SongTitle", GameManager.instance.currentSong.songTitle);
            PlayerPrefs.SetInt("ScorePlayer" + (i+1), playerStats[i].GetComponent<PlayerStats>().score);
            PlayerPrefs.SetInt("HealthPlayer" + (i+1), playerStats[i].GetComponent<PlayerStats>().health);
            PlayerPrefs.SetInt("SuperbHitPlayer" + (i+1), playerStats[i].GetComponent<PlayerStats>().superb);
            PlayerPrefs.SetInt("GoodHitPlayer" + (i+1), playerStats[i].GetComponent<PlayerStats>().good);
            PlayerPrefs.SetInt("BadHitPlayer" + (i+1), playerStats[i].GetComponent<PlayerStats>().bad);
            PlayerPrefs.SetInt("GloomyHitPlayer" + (i+1), playerStats[i].GetComponent<PlayerStats>().gloomy);
        }

        
        
    }

    // void OnEnable()
    // {
    //     Debug.Log("OnEnable called");
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // void OnDisable()
    // {
    //     Debug.Log("OnDisable");
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    public void OnPlayerJoined(PlayerInput input)
    {
        playerStats[playerNumber] = input.gameObject;
        playerNumber += 1;
        if(playerNumber >= playerPrefabs.Length)
        {
            allPlayersReady(this);
            Debug.Log("Looped");
            playerNumber = 0; 
        }
        else
        {
            manager.playerPrefab = playerPrefabs[playerNumber];
        }   

        


    }

    // Start is called before the first frame update
    
    public void UpdatePlayerNumber(int newPlayerNumber)
    {
        playerNumber = newPlayerNumber;
    }
}

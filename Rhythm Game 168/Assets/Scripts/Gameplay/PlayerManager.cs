using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private PlayerInputManager manager;

    [SerializeField] private int playerCount = 1; //MUST set in editor for single vs multiplayer
    private int playerNumber = 0;
    private int totalScore = 0;

    private int PlayersDead = 0;


    [SerializeField]
    private GameObject[] playerPrefabs;
    

    // public static PlayerManager instance;
    
    void Awake()
    {
        manager = GetComponent<PlayerInputManager>();

        manager.playerPrefab = playerPrefabs[playerNumber]; 
    }

    void Start()
    {
        PlayerPrefs.DeleteAll();

        PlayerStats.imDead += LoseGameCheck; //<---- Observer Pattern (subscribing)
        PlayerStats.wonMyGame += wonGame;
        PlayerStats.totScoreChange += TotalScore;
        if(playerCount == 1)
        {
            manager.gameObject.SetActive(false);
        }
    }

    private void TotalScore(int addedScore)
    {
        totalScore += addedScore;
    }

    private void OnDestroy()
    {
        PlayerStats.imDead -= LoseGameCheck; //<---- Observer Pattern (unsubscribing)
    }

    private void LoseGameCheck(PlayerStats sub)
    {
        PlayersDead += 1;
        if (PlayersDead >= playerCount)
        {
            GameOver();
            LevelLoaderScript.instance.LoadNextSceneFromDead("ResultScr");
        }
    }

    private void wonGame(PlayerStats sub)
    {
        GameOver();
        LevelLoaderScript.instance.LoadNextSceneFromWin("ResultScr");
    }

    private void GameOver()
    {
        PlayerPrefs.SetInt("TotalScore", totalScore);

        for(int i = 0; i < playerPrefabs.Length; i++) //Get Scores from each player to put into results screen.
        {
            PlayerPrefs.SetInt("ScorePlayer" + i, playerPrefabs[i].GetComponent<PlayerStats>().score);
            PlayerPrefs.SetInt("ScorePlayer" + i, playerPrefabs[i].GetComponent<PlayerStats>().health);
            PlayerPrefs.SetInt("SuperbHitPlayer" + i, playerPrefabs[i].GetComponent<PlayerStats>().superb);
            PlayerPrefs.SetInt("GoodHitPlayer" + i, playerPrefabs[i].GetComponent<PlayerStats>().good);
            PlayerPrefs.SetInt("BadHitPlayer" + i, playerPrefabs[i].GetComponent<PlayerStats>().bad);
            PlayerPrefs.SetInt("GloomyHitPlayer" + i, playerPrefabs[i].GetComponent<PlayerStats>().gloomy);
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

    public void NewPlayer(PlayerInput input)
    {
        playerNumber += 1;
        if(playerNumber >= playerPrefabs.Length)
        {
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

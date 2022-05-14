using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private PlayerInput thisPlayerInput;

    private PlayerSpawner[] myPlayerSpawners;
    private int playerNumber = 0;

    // public static PlayerManager instance;
    
    void Awake()
    {
        thisPlayerInput = GetComponent<PlayerInput>();
        
        playerNumber = thisPlayerInput.playerIndex;

        DontDestroyOnLoad(this);   
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        SpawnPlayers();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        myPlayerSpawners = FindObjectsOfType<PlayerSpawner>();
        foreach(PlayerSpawner player in myPlayerSpawners)
        {
            if(player.assignedPlayerInput == null)
            {
                Debug.Log(thisPlayerInput.playerIndex);
                player.spawnPlayer(thisPlayerInput, playerNumber); 
                break;    
            }    
        }  
    }

    // Start is called before the first frame update
    
        
    

    public void UpdatePlayerNumber(int newPlayerNumber)
    {
        playerNumber = newPlayerNumber;
    }
}

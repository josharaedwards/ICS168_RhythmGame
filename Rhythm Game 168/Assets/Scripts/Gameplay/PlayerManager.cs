using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private PlayerInputManager manager;
    private int playerNumber = 0;


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
        if(playerPrefabs[playerNumber] != null)
        {
            manager.gameObject.SetActive(false);
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
        
        manager.playerPrefab = playerPrefabs[playerNumber];
        if(playerNumber >= playerPrefabs.Length)
        {
            Debug.Log("Looped");
            playerNumber = 0; 
        }
        
            

    }

    // Start is called before the first frame update
    
        
    public void YouDied()
    {
        
    }

    public void UpdatePlayerNumber(int newPlayerNumber)
    {
        playerNumber = newPlayerNumber;
    }
}

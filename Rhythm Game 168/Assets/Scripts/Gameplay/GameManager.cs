using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance { get; private set; }

    public SongData[] songList;
    public SongData currentSong;

    public enum GameStates
    {
        Menu,
        SinglePlayer,
        LocalMultiplayer,
        OnlineMultiplayer
    }

    public static GameStates gameState = GameStates.Menu;

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Init()
    {
        if (instance && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        GameObject[] gms = GameObject.FindGameObjectsWithTag("GameManager");

        if (gms.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject.transform.root);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room Successful");
        SceneManager.LoadScene("SinglePlayerMultiplayer");
    }

    public void LeaveRoom()
    {
        if (gameState == GameStates.OnlineMultiplayer)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            SceneManager.LoadScene("SinglePlayerMultiplayer");
        }

    }

    public void LoadArena()
    {

        switch (gameState)
        {
            case GameStates.SinglePlayer:
                StartCoroutine(LoadSinglePlayerArena());
                break;
            case GameStates.LocalMultiplayer:
                StartCoroutine(LoadLocalMultiplayerArena());
                break;
            case GameStates.OnlineMultiplayer:
                 if (!PhotonNetwork.IsMasterClient)
                {
                    Debug.LogError("Photon: Trying to load a level but not master client");
                }
                Debug.Log("Our current PlayerCount is " + PhotonNetwork.CurrentRoom.PlayerCount);

                LoadOnlineMultiplayerArena();
                break;
        }
    }

    IEnumerator LoadSinglePlayerArena()
    {
        Debug.LogFormat("[GameManager] : Loading Single Player Level : Main");
        SceneManager.LoadSceneAsync("Main");
        while(!BeatmapLoader.JSONLoaded)
        {
            yield return null;
        }
    }

    IEnumerator LoadLocalMultiplayerArena()
    {
        Debug.LogFormat("[GameManager] : Loading Multiplayer Level : Main2");
        SceneManager.LoadSceneAsync("Main2");
        while(!BeatmapLoader.JSONLoaded)
        {
            yield return null;
        }
    }

    private void LoadOnlineMultiplayerArena()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.LogFormat("[GameManager] Photon : Loading Multiplayer Level : Main");
            PhotonNetwork.LoadLevel("WaitingRoom");
        }
        else
        {
            Debug.LogFormat("[GameManager] Photon : Loading Multiplayer Level : Main3");
            PhotonNetwork.LoadLevel("Main3");
        }
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom()");

        if(PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient", PhotonNetwork.IsMasterClient);

            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom()");

        if(PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient", PhotonNetwork.IsMasterClient);

            LoadArena();
        }
    }

    public void SetCurrentSong(SongData otherSong)
    {
        currentSong = otherSong;
        Debug.Log("Song Set: " + currentSong.songTitle);
    }

    public void SetGameState(GameStates state)
    {
        gameState = state;

        Debug.Log("Game State set to " + state);
    }
}

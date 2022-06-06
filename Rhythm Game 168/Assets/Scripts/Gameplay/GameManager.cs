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
        PhotonNetwork.LeaveRoom();
    }

    private void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Photon: Trying to load a level but not master client");
        }

        Debug.Log("Our current PlayerCount is " + PhotonNetwork.CurrentRoom.PlayerCount);

        switch (gameState)
        {
            case GameStates.SinglePlayer:
                LoadSinglePlayerArena();
                break;
            case GameStates.LocalMultiplayer:
                LoadLocalMultiplayerArena();
                break;
            case GameStates.OnlineMultiplayer:
                LoadOnlineMultiplayerArena();
                break;
        }
    }

    private void LoadSinglePlayerArena()
    {
        Debug.LogFormat("[GameManager] Photon : Loading Single Player Level : Main");
        PhotonNetwork.LoadLevel("Main");
    }

    private void LoadLocalMultiplayerArena()
    {
        Debug.LogFormat("[GameManager] Photon : Loading Multiplayer Level : Main2");
        PhotonNetwork.LoadLevel("Main2");
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
            Debug.LogFormat("[GameManager] Photon : Loading Multiplayer Level : Main2");
            PhotonNetwork.LoadLevel("Main3");
        }
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);

        if(PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);

            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName);

        if(PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);

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

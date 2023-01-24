using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    private bool isConnecting;

    [SerializeField] private byte maxPlayersPerRoom = 2;
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject progressLabel;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        if(GameManager.gameState != GameManager.GameStates.OnlineMultiplayer) 
        {
            GameManager.instance.LoadArena();
        }
        else if(PhotonNetwork.IsConnected && BeatmapLoader.JSONLoaded)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN"); 

        //Tries to joing a random existing room and calls OnJoinRandomFailed() if not
        PhotonNetwork.JoinRandomRoom();
        isConnecting = false;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);

        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        isConnecting = false;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so create one. \nCalling: PhotonNetwork.CreateRoom");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
        Debug.Log("Our current PlayerCount is " + PhotonNetwork.CurrentRoom.PlayerCount);

        GameManager.GameStates currentState = GameManager.gameState;

        switch(currentState)
        {
            case GameManager.GameStates.SinglePlayer:
                JoinSinglePlayerRoom();
                break;
            case GameManager.GameStates.LocalMultiplayer:
                JoinLocalMutliplayerRoom();
                break;
            case GameManager.GameStates.OnlineMultiplayer:
                JoinOnlineMultiplayerRoom();
                break;
            default:
                Debug.Log("[Launcher] We are in in menu mode");
                break;
        }
    }

    private void JoinSinglePlayerRoom()
    {
        Debug.Log("[Launcher] We load the Main for SinglePlayer");
        PhotonNetwork.LoadLevel("Main");
    }

    private void JoinLocalMutliplayerRoom()
    {
        Debug.Log("[Launcher] We load the Main for Multiplayer");
        PhotonNetwork.LoadLevel("Main2");
    }

    private void JoinOnlineMultiplayerRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("[Launcher] In Multiplayer but only one player. Load the Main for Singleplayer");
            PhotonNetwork.LoadLevel("WaitingRoom");
        }
        else
        {
            Debug.Log("[Launcher] We load the Main for Multiplayer");
            PhotonNetwork.LoadLevel("Main3");
        }
    }
}

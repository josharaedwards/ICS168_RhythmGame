using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerSpawner : MonoBehaviour
{
    [HideInInspector]
    public PlayerInput assignedPlayerInput;

    [SerializeField]
    private GameObject[] playerPrefabs;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void spawnPlayer(PlayerInput myNewPlayerInput, int playerNumber)
    {
        assignedPlayerInput = myNewPlayerInput;
        GameObject player = Instantiate(playerPrefabs[playerNumber], playerPrefabs[playerNumber].transform.position, playerPrefabs[playerNumber].transform.rotation, gameObject.transform);
    }
}

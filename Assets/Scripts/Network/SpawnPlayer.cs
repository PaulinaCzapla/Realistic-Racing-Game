using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform[] spawnPositions;
    private Transform spawnPos;
    //private bool StartGame = false;
    private int numberPlayers;

    /*private void Awake()
    {
        CheckPlayers();
        if (numberPlayers == 1)
        {
            spawnPos = spawnPositions[0];
            numberPlayers = 2;
        }
 
        else if (numberPlayers == 2)
        {
            spawnPos = spawnPositions[1];
            numberPlayers = 3;
        }
        else if (numberPlayers == 3)
        {
            spawnPos = spawnPositions[2];
            numberPlayers = 4;
        }
        else if (numberPlayers == 4)
        {
            spawnPos = spawnPositions[3];
            numberPlayers = 1;
        }
    }*/

    private void Start()
    {
        CheckPlayers();
        if (numberPlayers == 0)
        {
            PhotonNetwork.Instantiate(playerPrefab.name,spawnPositions[0].position , Quaternion.identity);
            numberPlayers = 1;
        }
 
        else if (numberPlayers == 1)
        {
            PhotonNetwork.Instantiate(playerPrefab.name,spawnPositions[1].position , Quaternion.identity);
            numberPlayers = 2;
        }
        else if (numberPlayers == 3)
        {
            PhotonNetwork.Instantiate(playerPrefab.name,spawnPositions[2].position , Quaternion.identity);
            numberPlayers = 4;
        }
        else if (numberPlayers == 4)
        {
            PhotonNetwork.Instantiate(playerPrefab.name,spawnPositions[3].position , Quaternion.identity);
            numberPlayers = 1;
        }
        //Vector3 position = new Vector3(spawnPos.position.x,spawnPos.position.y,spawnPos.position.z);
        }
    
    void CheckPlayers()
    {
        numberPlayers = PhotonNetwork.CountOfPlayersInRooms;
        for (int i = 0; i <= numberPlayers; i++)
        {
            if (numberPlayers > 4)
            {
                numberPlayers -= 4;
            }
           
        }
    }
}

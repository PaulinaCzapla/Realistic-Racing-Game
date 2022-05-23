using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform[] spawnPositions;
    //private Transform _spawnPos;
    private int _numberPlayers;
    

    private void Start()
    {
        CheckPlayers();
        Invoke(nameof(SpawnPlayers),10f);
    }
    
    void CheckPlayers()
    {
        _numberPlayers = PhotonNetwork.CountOfPlayersInRooms;
        for (int i = 0; i <= _numberPlayers; i++)
        {
            if (_numberPlayers > 4)
            {
                _numberPlayers -= 4;
            }
        }
    }

    private void SpawnPlayers()
    {
        if (_numberPlayers == 0)
        {
            PhotonNetwork.Instantiate(playerPrefab.name,spawnPositions[0].position ,spawnPositions[0].rotation, 0);
            _numberPlayers = 1;
        }
        else if (_numberPlayers == 1)
        {
            PhotonNetwork.Instantiate(playerPrefab.name,spawnPositions[1].position , spawnPositions[1].rotation, 0);
            _numberPlayers = 2;
        }
        else if (_numberPlayers == 2)
        {
            PhotonNetwork.Instantiate(playerPrefab.name,spawnPositions[2].position , spawnPositions[2].rotation, 0);
            _numberPlayers = 3;
        }
        else if (_numberPlayers == 3)
        {
            PhotonNetwork.Instantiate(playerPrefab.name,spawnPositions[3].position , spawnPositions[3].rotation, 0);
            _numberPlayers = 4;
        }
    }
}

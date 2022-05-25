using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Car.WheelsManagement;
using RaceManagement;
using TMPro;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPositions;
    private Transform _spawnPos;
    private int _numberPlayers;

    [SerializeField] private TextMeshProUGUI numberOfPlayersInRace;
    [SerializeField] private RaceController raceController;
    [SerializeField] private List<GameObject> canvasObjects = new List<GameObject>();


    private void Start()
    {
        CheckPlayers();
        StartCoroutine(SpawnNewPlayer());
    }

    private void Update()
    {
        var numberOfPlayersInScene = FindObjectsOfType<CarMovementController>();
        numberOfPlayersInRace.text = numberOfPlayersInScene.Length + "/" + PhotonNetwork.CurrentRoom.PlayerCount;
        if (numberOfPlayersInScene.Length == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            foreach (var canvasObject in canvasObjects)
            {
                canvasObject.SetActive(false);
            }
            PhotonNetwork.CurrentRoom.IsOpen = false;
            raceController.enabled = true;
        }
    }

    private void CheckPlayers()
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

    private IEnumerator SpawnNewPlayer()
    {
        yield return new WaitForSeconds(10f);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[_numberPlayers].position, spawnPositions[_numberPlayers].rotation, 0);
        _numberPlayers ++;
    }
}

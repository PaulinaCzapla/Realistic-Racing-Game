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
    public GameObject playerPrefab;
    public Transform[] spawnPositions;
    private Transform spawnPos;
    private int numberPlayers;

    [SerializeField] private TextMeshProUGUI numberOfPlayersInRace;
    [SerializeField] private RaceController raceController;
    [SerializeField] private List<GameObject> canvasObjects = new List<GameObject>();


    private void Start()
    {
        CheckPlayers();
        StartCoroutine(SpawnNewPlayer());
        //Invoke(nameof(SpawnNewPlayer), 1f);
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
        numberPlayers = PhotonNetwork.CountOfPlayersInRooms;
        for (int i = 0; i <= numberPlayers; i++)
        {
            if (numberPlayers > 4)
            {
                numberPlayers -= 4;
            }
        }
    }

    private IEnumerator SpawnNewPlayer()
    {
        yield return new WaitForSeconds(10f);
        if (numberPlayers == 0)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[0].position, spawnPositions[0].rotation, 0);
            numberPlayers = 1;
        }
        else if (numberPlayers == 1)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[1].position, spawnPositions[1].rotation, 0);
            numberPlayers = 2;
        }
        else if (numberPlayers == 2)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[2].position, spawnPositions[2].rotation, 0);
            numberPlayers = 3;
        }
        else if (numberPlayers == 3)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[3].position, spawnPositions[3].rotation, 0);
            numberPlayers = 4;
        }
    }
}

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
    [SerializeField] public List<Material> colors = new List<Material>();

    private void Start()
    {
        CheckPlayers();
        SpawnNewPlayer();
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
            //PhotonNetwork.CurrentRoom.IsOpen = false;
            //raceController.enabled = true;
            if (raceController.enabled == false)
            {
                GetComponent<PhotonView>().RPC("RaceStart", RpcTarget.AllBuffered, null);
            }
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

    private void SpawnNewPlayer()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[PhotonNetwork.LocalPlayer.ActorNumber - 1].position, spawnPositions[PhotonNetwork.LocalPlayer.ActorNumber - 1].rotation, 0);
        _numberPlayers ++;
    }
    
    [PunRPC]
    public void RaceStart()
    {
        raceController.enabled = true;
    }
}

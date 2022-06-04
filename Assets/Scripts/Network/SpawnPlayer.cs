using System.Collections;
using System.Collections.Generic;
using Car.WheelsManagement;
using Photon.Pun;
using RaceManagement;
using TMPro;
using UnityEngine;

namespace Network
{
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

        private IEnumerator SpawnNewPlayer()
        {
            yield return new WaitForSeconds(1f);
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[PhotonNetwork.LocalPlayer.ActorNumber - 1].position, spawnPositions[PhotonNetwork.LocalPlayer.ActorNumber - 1].rotation, 0);
            _numberPlayers ++;
        }
    
        [PunRPC]
        public void RaceStart()
        {
            raceController.enabled = true;
        }
    }
}

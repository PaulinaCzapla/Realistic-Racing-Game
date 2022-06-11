using System;
using System.Collections;
using System.Collections.Generic;
using Car.WheelsManagement;
using Photon.Pun;
using Photon.Realtime;
using RaceManagement;
using TMPro;
using UnityEngine;
using PhotonNetwork = Photon.Pun.PhotonNetwork;


namespace Network
{
    public class Sync : Photon.Pun.MonoBehaviourPun, IPunObservable
    {
        private Vector3 _trueLoc;
        private Quaternion _trueRot;
        private PhotonView _photonView;
        private SpawnPlayer _spawnPlayer;
        private int _color;
        private string _name;
        [SerializeField] private GameObject body;
        [SerializeField] private RaceParticipant raceParticipant;
        [SerializeField] private TextMeshProUGUI text;

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
            _spawnPlayer = FindObjectOfType<SpawnPlayer>();
            if (gameObject.TryGetComponent(out CarMovementController _))
            {
                body.GetComponent<MeshRenderer>().material =
                    _spawnPlayer.colors[(int) PhotonNetwork.LocalPlayer.CustomProperties["color"] - 1];
            }

            /*switch ((int) PhotonNetwork.LocalPlayer.CustomProperties["color"])
            {
                case 1:
                    raceParticipant.Name = "Red";
                    break;
                case 2:
                    raceParticipant.Name = "Green";
                    break;
                case 3:
                    raceParticipant.Name = "Pink";
                    break;
                case 4:
                    raceParticipant.Name = "Yellow";
                    break;
                    
            }*/
            if (raceParticipant != null)
            {
                raceParticipant.Name = (string) PhotonNetwork.LocalPlayer.CustomProperties["name"];
            }
            
        }

        private void Update()
        {
            if (!_photonView.IsMine)
            {
                transform.position = Vector3.Lerp(transform.position, _trueLoc, Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, _trueRot, Time.deltaTime);
                if (gameObject.TryGetComponent(out CarMovementController _))
                {
                    transform.Find("View").transform.Find("body").GetComponent<MeshRenderer>().material =
                        _spawnPlayer.colors[_color - 1];
                }
                raceParticipant.Name = _name;
                text.text = _name;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsReading)
            {
                if (!_photonView.IsMine)
                {
                    this._trueLoc = (Vector3) stream.ReceiveNext();
                    this._color = (int) stream.ReceiveNext();
                    this._name = (string) stream.ReceiveNext();
                }
            }
            else
            {
                if (_photonView.IsMine)
                {
                    stream.SendNext(transform.position);
                    stream.SendNext((int) PhotonNetwork.LocalPlayer.CustomProperties["color"]);
                    stream.SendNext((string)PhotonNetwork.LocalPlayer.CustomProperties["name"]);
                }

                if (_photonView == null)
                {
                }
            }
        }
    }
}

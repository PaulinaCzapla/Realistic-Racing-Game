using System.Collections;
using System.Collections.Generic;
using Car.WheelsManagement;
using Photon.Pun;
using Photon.Realtime;
using RaceManagement;
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
        [SerializeField] private GameObject body;

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
            _spawnPlayer = FindObjectOfType<SpawnPlayer>();
            if (gameObject.TryGetComponent(out CarMovementController _))
            {
                body.GetComponent<MeshRenderer>().material =
                    _spawnPlayer.colors[(int) PhotonNetwork.LocalPlayer.CustomProperties["color"] - 1];
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
                }
            }
            else
            {
                if (_photonView.IsMine)
                {
                    stream.SendNext(transform.position);
                    stream.SendNext((int) PhotonNetwork.LocalPlayer.CustomProperties["color"]);
                }

                if (_photonView == null)
                {
                }
            }
        }
    }
}

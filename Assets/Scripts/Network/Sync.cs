using Car.WheelsManagement;
using Photon.Pun;
using UnityEngine;

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
                body.GetComponent<MeshRenderer>().material = _spawnPlayer.colors[(int) PhotonNetwork.LocalPlayer.CustomProperties["color"] - 1];
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
                    this._trueLoc = (Vector3)stream.ReceiveNext(); 
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
        /*Vector3 trueLoc;
    Quaternion trueRot;
    PhotonView photonView;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, trueLoc, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, trueRot, Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            if (!photonView.IsMine)
            {
                this.trueLoc = (Vector3)stream.ReceiveNext(); //the stream send data types of "object" we must typecast the data into a Vector3 format
            }
        }
        else
        {
            if (photonView.IsMine)
            {
                stream.SendNext(transform.position);
            }
            if (photonView == null)
            {
            }
        }
    }*/
    }
}

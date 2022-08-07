using Events.ScriptableObjects;
using Photon.Pun;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class DisconnectRoom : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button menuReturnButton;
        [SerializeField] private LoadSceneEventChannelSO loadMenuSceneEvent;
        [SerializeField] private GameSceneSO mainMenuScene;
        [SerializeField] private PlayerChoicesController playerChoicesController;
        private void Start()
        {
            menuReturnButton.onClick.AddListener(ReturnMenu);
        }

        private void ReturnMenu()
        {
            playerChoicesController.returnToMenu = true;
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            loadMenuSceneEvent.RaiseEvent(mainMenuScene, true);
        }

        /*public override void OnLeftRoom()
        {
            loadMenuSceneEvent.RaiseEvent(mainMenuScene, true);
            base.OnLeftRoom();
        }*/
    }
}

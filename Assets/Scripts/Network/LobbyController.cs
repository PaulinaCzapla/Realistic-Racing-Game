using System;
using Events.ScriptableObjects;
using Photon.Pun;
using SceneManagement.ScriptableObjects;
using SoundManagement;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Network
{
    public class LobbyController : MonoBehaviourPun
    {
        private Color _selectedColor = new Color(236,197,48,255);
        private Color _unselectedColor = new Color(0,0,0,255);
        private int _numberOfLaps;

        [SerializeField] private Button raceStartButton;
        [SerializeField] private Button menuReturnButton;
        [SerializeField] private Button color1Button;
        [SerializeField] private Button color2Button;
        [SerializeField] private Button color3Button;
        [SerializeField] private Button color4Button;
        [SerializeField] private Button minusButton;
        [SerializeField] private Button plusButton;
        [SerializeField] private TextMeshProUGUI playersInLobbyText;
        [SerializeField] private TextMeshProUGUI numberOfLapsText;

        [SerializeField] private LoadSceneEventChannelSO loadSceneEvent;
        //[SerializeField] private LoadSceneEventChannelSO loadMenuSceneEvent;
        //[SerializeField] private LoadSceneEventChannelSO photonLoadSceneEvent;
        [SerializeField] private SoundEventChannelSO onMenuMusicStart;
        [SerializeField] private PlayerChoicesController playerChoices;
    
        [SerializeField] private GameSceneSO mainMenuScene;
        [SerializeField] private GameSceneSO multiplayerDemoScene;
    
        
        private void OnEnable()
        {
            raceStartButton.onClick.AddListener(RaceStart);
            //menuReturnButton.onClick.AddListener(ReturnMenu);
            color1Button.onClick.AddListener(() => ChosenColor(1));
            color2Button.onClick.AddListener(() => ChosenColor(2));
            color3Button.onClick.AddListener(() => ChosenColor(3));
            color4Button.onClick.AddListener(() => ChosenColor(4));
            minusButton.onClick.AddListener(() => UpdateLaps(-1));
            plusButton.onClick.AddListener(() => UpdateLaps(1));
            _numberOfLaps = 1;
            if (PhotonNetwork.IsMasterClient)
            {
                raceStartButton.gameObject.SetActive(true);
                minusButton.gameObject.SetActive(true);
                plusButton.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (PhotonNetwork.IsConnected)
            {
                playersInLobbyText.text = "Players in lobby: " + PhotonNetwork.CurrentRoom.PlayerCount + "/4";
            }
            if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }

        private void ChosenColor(int color)
        {
            var hash = new Hashtable();
            var hash1 = new Hashtable();
            
            if (playerChoices.chosenButton > 0)
            {
                switch (playerChoices.chosenButton)
                {
                    case 1:
                        PlayerUnselectedColor(color1Button);
                        break;
                    case 2:
                        PlayerUnselectedColor(color2Button);
                        break;
                    case 3:
                        PlayerUnselectedColor(color3Button);
                        break;
                    case 4:
                        PlayerUnselectedColor(color4Button);
                        break;
                }
                GetComponent<PhotonView>().RPC("UnselectColor", RpcTarget.OthersBuffered, playerChoices.chosenButton);
            }
            GetComponent<PhotonView>().RPC("ColorOccupied", RpcTarget.OthersBuffered, color);

            var name = "white";
            
            switch (color)
            {
                case 1:
                    PlayerSelectedColor(color1Button);
                    //hash1.Add("name","Red");
                    name = "Red";
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hash1);
                    break;
                case 2:
                    PlayerSelectedColor(color2Button);
                    //hash1.Add("name","Green");
                    name = "Green";
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hash1);
                    break;
                case 3:
                    PlayerSelectedColor(color3Button);
                    //hash1.Add("name","Pink");
                    name = "Pink";
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hash1);
                    break;
                case 4:
                    PlayerSelectedColor(color4Button);
                    //hash1.Add("name","Yellow");
                    name = "Yellow";
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hash1);
                    break;
            }
        
            playerChoices.chosenButton = color;
            hash.Add("color",color);
            hash1.Add("name", name);
            playerChoices.PlayerHasChosenColor();
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash1);
        }

        private void OnDisable()
        {
            raceStartButton.onClick.RemoveAllListeners();
            menuReturnButton.onClick.RemoveAllListeners();
            color1Button.onClick.RemoveAllListeners();
            color2Button.onClick.RemoveAllListeners();
            color3Button.onClick.RemoveAllListeners();
            color4Button.onClick.RemoveAllListeners();
            PlayerPrefs.SetInt("NumberOfLaps",_numberOfLaps);
        }

        private void RaceStart()
        {
            GetComponent<PhotonView>().RPC("StartRace", RpcTarget.AllBuffered, null);
        }

        private void PlayerSelectedColor(Component chosenButton)
        {
            var chosenColor = new Vector2(8, 10);
            var component = chosenButton.gameObject.transform.parent.Find("Panel").GetComponent<Outline>();
            component.effectColor = _selectedColor;
            component.effectDistance = chosenColor;
        }
    
        private void PlayerUnselectedColor(Component chosenButton)
        {
            var changedColor = new Vector2(3, 5);
            var component = chosenButton.gameObject.transform.parent.Find("Panel").GetComponent<Outline>();
            component.effectColor = _unselectedColor;
            component.effectDistance = changedColor;
        }

        private void UpdateLaps(int i)
        {
            GetComponent<PhotonView>().RPC("LapsUpdate", RpcTarget.AllBuffered, i);
        }

        [PunRPC]
        public void LapsUpdate(int i)
        {
            if (i > 0 && _numberOfLaps < 10)
            {
                _numberOfLaps++;
            }else if (i < 0 && _numberOfLaps > 1)
            {
                _numberOfLaps--;
            }
            numberOfLapsText.text = _numberOfLaps.ToString();
        }

        [PunRPC]
        public void ColorOccupied(int i)
        {
            switch (i)
            {
                case 1:
                    color1Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _selectedColor;
                    color1Button.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                    color1Button.interactable = false;
                    PlayerPrefs.SetInt("color1", 1);
                    break;
                case 2:
                    color2Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _selectedColor;
                    color2Button.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                    color2Button.interactable = false;
                    PlayerPrefs.SetInt("color2", 1);
                    break;
                case 3:
                    color3Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _selectedColor;
                    color3Button.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                    color1Button.interactable = false;
                    PlayerPrefs.SetInt("color3", 1);
                    break;
                case 4:
                    color4Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _selectedColor;
                    color4Button.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                    color1Button.interactable = false;
                    PlayerPrefs.SetInt("color4", 1);
                    break;
            }
        }
    
        [PunRPC]
        public void UnselectColor(int i)
        {
            switch (i)
            {
                case 1:
                    color1Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _unselectedColor;
                    color1Button.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    color1Button.interactable = true;
                    PlayerPrefs.SetInt("color1", 0);
                    break;
                case 2:
                    color2Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _unselectedColor;
                    color2Button.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    color1Button.interactable = true;
                    PlayerPrefs.SetInt("color2", 0);
                    break;
                case 3:
                    color3Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _unselectedColor;
                    color3Button.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    color1Button.interactable = true;
                    PlayerPrefs.SetInt("color3", 0);
                    break;
                case 4:
                    color4Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _unselectedColor;
                    color4Button.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    color1Button.interactable = true;
                    PlayerPrefs.SetInt("color4", 0);
                    break;
            }
        }

        [PunRPC]
        public void StartRace()
        {
            loadSceneEvent.RaiseEvent(multiplayerDemoScene, true);
        }
    }
}

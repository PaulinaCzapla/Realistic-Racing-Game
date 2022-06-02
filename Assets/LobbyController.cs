using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Photon.Pun;
using SceneManagement.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyController : MonoBehaviourPun
{
    private Color _selectedColor = new Color(236,197,48,255);
    [SerializeField] private Button raceStartButton;
    [SerializeField] private Button menuReturnButton;
    [SerializeField] private Button color1Button;
    [SerializeField] private Button color2Button;
    [SerializeField] private Button color3Button;
    [SerializeField] private Button color4Button;
    [SerializeField] private TextMeshProUGUI playersInLobbyText;
    
    [SerializeField] private LoadSceneEventChannelSO loadSceneEvent;
    
    [SerializeField] private GameSceneSO mainMenuScene;
    [SerializeField] private GameSceneSO multiplayerDemoScene;

    private void OnEnable()
    {
        SceneManager.LoadSceneAsync("PersistentScene", LoadSceneMode.Additive);
        raceStartButton.onClick.AddListener(RaceStart);
        menuReturnButton.onClick.AddListener(() => loadSceneEvent.RaiseEvent(mainMenuScene, true));
        color1Button.onClick.AddListener(() => ChosenColor(1));
        color2Button.onClick.AddListener(() => ChosenColor(2));
        color3Button.onClick.AddListener(() => ChosenColor(3));
        color4Button.onClick.AddListener(() => ChosenColor(4));
        if (PhotonNetwork.IsMasterClient)
        {
            playersInLobbyText.text = "is a master";
            raceStartButton.gameObject.SetActive(true);
        }
        else
        {
            playersInLobbyText.text = "is not a master";
        }
    }

    private void ChosenColor(int color)
    {
        var hash = new Hashtable();
        switch (color)
        {
            case 1:
                GetComponent<PhotonView>().RPC("ColorOccupied", RpcTarget.AllBuffered, 1);
                break;
            case 2:
                GetComponent<PhotonView>().RPC("ColorOccupied", RpcTarget.AllBuffered, 2);
                break;
            case 3:
                GetComponent<PhotonView>().RPC("ColorOccupied", RpcTarget.AllBuffered, 3);
                break;
            case 4:
                GetComponent<PhotonView>().RPC("ColorOccupied", RpcTarget.AllBuffered, 4);
                break;
        }
        hash.Add("color",color);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    private void OnDisable()
    {
        SceneManager.UnloadSceneAsync("PersistentScene");
        raceStartButton.onClick.RemoveAllListeners();
        menuReturnButton.onClick.RemoveAllListeners();
        color1Button.onClick.RemoveAllListeners();
        color2Button.onClick.RemoveAllListeners();
        color3Button.onClick.RemoveAllListeners();
        color4Button.onClick.RemoveAllListeners();
    }

    private void RaceStart()
    {
        GetComponent<PhotonView>().RPC("StartRace", RpcTarget.AllBuffered, null);
    }

    [PunRPC]
    public void ColorOccupied(int i)
    {
        switch (i)
        {
            case 1:
                color1Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _selectedColor;
                break;
            case 2:
                color2Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _selectedColor;
                break;
            case 3:
                color3Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _selectedColor;
                break;
            case 4:
                color4Button.gameObject.transform.parent.Find("Panel").GetComponent<Outline>().effectColor = _selectedColor;
                break;
        }
    }

    [PunRPC]
    public void StartRace()
    {
        loadSceneEvent.RaiseEvent(multiplayerDemoScene, true);
    }
}

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
        raceStartButton.onClick.AddListener(() => loadSceneEvent.RaiseEvent(multiplayerDemoScene, true));
        menuReturnButton.onClick.AddListener(() => loadSceneEvent.RaiseEvent(mainMenuScene, true));
        color1Button.onClick.AddListener(() => ChosenColor(1));
        color2Button.onClick.AddListener(() => ChosenColor(2));
        color3Button.onClick.AddListener(() => ChosenColor(3));
        color4Button.onClick.AddListener(() => ChosenColor(4));
        if (PhotonNetwork.IsMasterClient)
        {
            playersInLobbyText.gameObject.SetActive(true);
        }
    }

    private void ChosenColor(int color)
    {
        switch (color)
        {
            case 1:
                GetComponent<PhotonView>().RPC("ColorOccupied", RpcTarget.AllBuffered, 1);
                PlayerPrefs.SetInt("PlayerColor",1);
                break;
            case 2:
                GetComponent<PhotonView>().RPC("ColorOccupied", RpcTarget.AllBuffered, 2);
                PlayerPrefs.SetInt("PlayerColor",2);
                break;
            case 3:
                GetComponent<PhotonView>().RPC("ColorOccupied", RpcTarget.AllBuffered, 3);
                PlayerPrefs.SetInt("PlayerColor",3);
                break;
            case 4:
                GetComponent<PhotonView>().RPC("ColorOccupied", RpcTarget.AllBuffered, 4);
                PlayerPrefs.SetInt("PlayerColor",4);
                break;
        }
    }

    private void OnDisable()
    {
        raceStartButton.onClick.RemoveAllListeners();
        menuReturnButton.onClick.RemoveAllListeners();
        color1Button.onClick.RemoveAllListeners();
        color2Button.onClick.RemoveAllListeners();
        color3Button.onClick.RemoveAllListeners();
        color4Button.onClick.RemoveAllListeners();
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
}

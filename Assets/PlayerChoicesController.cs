using System;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerChoicesController : MonoBehaviour
{
    public int chosenButton;
    private Hashtable hasChosen;
    private Hashtable hash;
    [SerializeField] private LobbyController _lobbyController;

    private void OnEnable()
    {
        hasChosen = new Hashtable {{"hasChosen", false}};
        PhotonNetwork.LocalPlayer.SetCustomProperties(hasChosen);
        PlayerPrefs.SetInt("color1", 0);
        PlayerPrefs.SetInt("color2", 0);
        PlayerPrefs.SetInt("color3", 0);
        PlayerPrefs.SetInt("color4", 0);
    }

    private void OnDisable()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (!(bool) player.CustomProperties["hasChosen"])
            {
                if (PlayerPrefs.GetInt("color1") == 0)
                {
                    hash = new Hashtable {{"color", 1}};
                    player.SetCustomProperties(hash);
                    hasChosen = new Hashtable {{"hasChosen", true}};
                    player.SetCustomProperties(hasChosen);
                    PlayerPrefs.SetInt("color1", 1);
                    continue;
                }
                if (PlayerPrefs.GetInt("color2") == 0)
                {
                    hash = new Hashtable {{"color", 2}};
                    player.SetCustomProperties(hash);
                    hasChosen = new Hashtable {{"hasChosen", true}};
                    player.SetCustomProperties(hasChosen);
                    PlayerPrefs.SetInt("color2", 1);
                    continue;
                }
                if (PlayerPrefs.GetInt("color3") == 0)
                {
                    hash = new Hashtable {{"color", 3}};
                    player.SetCustomProperties(hash);
                    hasChosen = new Hashtable {{"hasChosen", true}};
                    player.SetCustomProperties(hasChosen);
                    PlayerPrefs.SetInt("color3", 1);
                    continue;
                }
                if (PlayerPrefs.GetInt("color4") == 0)
                {
                    hash = new Hashtable {{"color", 4}};
                    player.SetCustomProperties(hash);
                    hasChosen = new Hashtable {{"hasChosen", true}};
                    player.SetCustomProperties(hasChosen);
                    PlayerPrefs.SetInt("color4", 1);
                }
            }
        }
    }

    public void PlayerHasChosenColor()
    {
        hasChosen = new Hashtable {{"hasChosen", true}};
        PhotonNetwork.LocalPlayer.SetCustomProperties(hasChosen);
    }
}

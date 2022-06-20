using System;
using System.Collections;
using System.Collections.Generic;
using RaceManagement;
using TMPro;
using UnityEngine;

public class RaceFinishMenuController : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> ranking = new List<TextMeshProUGUI>();
    [SerializeField] private ControlPointsManager controlPointsManager;


    private void Update()
    {
        for (var i = 0; i < controlPointsManager.raceOutcome.Count; i++)
        {
            ranking[i].gameObject.SetActive(true);
            ranking[i].transform.Find("NameTime").GetComponent<TextMeshProUGUI>().text =
                controlPointsManager.raceOutcome[i];
        }
    }
}

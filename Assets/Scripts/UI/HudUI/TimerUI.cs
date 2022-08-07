using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.GlobalIllumination;

/// <summary>
/// Sets the date format and displays it in UI
/// </summary>
public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private FloatEventChannelSO onUpdateRaceTimeUI;

    private void OnEnable()
    {
        onUpdateRaceTimeUI.OnEventRaised += (float time) => OnUpdateTimer(time);
    }

    private void OnDisable()
    {
        onUpdateRaceTimeUI.OnEventRaised -= (float time) => OnUpdateTimer(time);
    }

    /// <summary>
    /// Updates timer ui
    /// </summary>
    private void OnUpdateTimer(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using RaceManagement.ControlPoints;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    private int _i;
    [SerializeField] private ControlPoint controlPoint;

    private void Start()
    {
        Debug.Log("hahahaha");
    }

    private void OnTriggerEnter(Collider other)
    {
        _i = controlPoint.spawnPoints.IndexOf(this.gameObject);
        controlPoint.spawnPoints.RemoveAt(_i);
    }

    private void OnTriggerExit(Collider other)
    {
        //controlPoint.spawnPoints.Add(this.gameObject);
        controlPoint.spawnPoints.Insert(_i, this.gameObject);
    }
}

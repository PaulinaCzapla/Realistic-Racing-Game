using System;
using System.Collections;
using System.Collections.Generic;
using RaceManagement.ControlPoints;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
   
    [SerializeField] private ControlPoint controlPoint;
     private int _i;
    
    private void OnTriggerEnter(Collider other)
    {
        _i = controlPoint.spawnPoints.IndexOf(this.gameObject);
        controlPoint.spawnPoints.RemoveAt(_i);
    }

    private void OnTriggerExit(Collider other)
    {
        controlPoint.spawnPoints.Insert(_i, this.gameObject);
    }
}

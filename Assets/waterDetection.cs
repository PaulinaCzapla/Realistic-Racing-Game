using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDetection : MonoBehaviour
{
    [SerializeField] private Transform resetPos;
    
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Water"))
        {
            transform.parent.position = resetPos.position;
            transform.parent.rotation = resetPos.rotation;
            transform.rotation = resetPos.rotation;
        }
    }
}

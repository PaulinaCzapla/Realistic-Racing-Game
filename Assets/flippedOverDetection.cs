using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flippedOverDetection : MonoBehaviour
{
    private float resetTime = 0f;
    [SerializeField] private Transform resetPos;
    public bool flipped = false;

    private void Start()
    {
        //pos = GetComponent<Transform>();
    }

    void Update()
    {
        Debug.Log(transform.eulerAngles.z);
        if (transform.position.y < 5 && transform.eulerAngles.z > 90 && transform.eulerAngles.z<300) 
        {
            timer();
            Debug.Log("time: " + resetTime);
            
        }
        else
        {
            resetTime = 0f;
        }
    }

    private void timer()
    {
        if (resetTime > 2f)
        {
            transform.parent.position = resetPos.position;
            transform.parent.rotation = resetPos.rotation;
            transform.rotation = resetPos.rotation;
            flipped = true;
        }
        resetTime += Time.deltaTime;
        Debug.Log(resetTime);
    }
}

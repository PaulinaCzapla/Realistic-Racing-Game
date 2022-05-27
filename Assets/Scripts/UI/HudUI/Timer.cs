using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    //public TextMeshProUGUI TimerText;
   
    public bool timerOn = false;
    public float time=0;
    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn)
        {
            //time += 0;
            updateTimer(time);
        }
    }

    void updateTimer(float currentTime){
        currentTime +=1;
        float minutes = Mathf.FloorToInt(currentTime/60);
        float seconds = Mathf.FloorToInt(currentTime%60);
        gameObject.GetComponent<TextMeshProUGUI>.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

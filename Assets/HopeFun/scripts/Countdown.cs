using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] TextMeshPro txtTimer;
    [SerializeField] int allcount;

    public void SetValue(int _input)
    {
        allcount = _input * 60;
    }

    private void Start()
    {
        allcount = SceneController.Minutes * 60;
        InvokeRepeating("CountDown", 0, 1);
    }

    private void CountDown()
    {
        allcount--;

        if (allcount < 0)
            allcount = 0;

        var ts = TimeSpan.FromSeconds(allcount);
        var time = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);

        txtTimer.text = time;
    }
}

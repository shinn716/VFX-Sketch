using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] TextMeshPro txtTimer;
    [SerializeField] int minutes = 10;


    //private DateTime dateTarget;

    private int allcount;

    public void SetMinutes(string _input)
    {
        int.TryParse(_input, out int value);
        minutes = value;
        allcount = minutes * 60;
    }

    private void Start()
    {
        //dateTarget = new DateTime(2022, 10, 12, 20, 30, 00);
        allcount = minutes * 60;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] TextMeshPro txtTimer;

    private DateTime dateTarget;

    private void Start()
    {
        dateTarget = new DateTime(2022, 10, 12, 20, 30, 00);
        InvokeRepeating("CountDown", 0, 1);
    }

    private void CountDown()
    {
        var result = dateTarget - DateTime.Now;
        int hours, minutes, seconds;
        hours = result.Hours;
        minutes = result.Minutes;
        seconds = result.Seconds;

        if (hours < 0)
            hours = 0;

        if (minutes < 0)
            minutes = 0;

        if (seconds < 0)
            seconds = 0;

        txtTimer.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }
}

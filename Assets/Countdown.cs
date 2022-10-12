using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] Text txtTimer;

    private DateTime dateTarget;

    private void Start()
    {
        dateTarget = new DateTime(2022, 10, 12, 17, 30, 00);
        InvokeRepeating("CountDown", 0, 1);
    }

    private void CountDown()
    {
        var result = dateTarget - DateTime.Now;
        txtTimer.text = $"{result.Hours.ToString("00")}:{result.Minutes.ToString("00")}:{result.Seconds.ToString("00")}";
    }
}

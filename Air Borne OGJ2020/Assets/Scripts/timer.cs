using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class timer : MonoBehaviour
{
    private double currentDoubleTime;
    private TextMeshProUGUI time;
    // Start is called before the first frame update
    void Start()
    {
        currentDoubleTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - ScoreManager.instanceScore.startTime;
        time = gameObject.GetComponent<TextMeshProUGUI>();
        time.text = "" + (Math.Floor(currentDoubleTime / 60000)).ToString() + ":" + currentDoubleTime % 60000 / 1000;

    }

    // Update is called once per frame
    void Update()
    {
        currentDoubleTime += (double)(Time.deltaTime*1000);
        time.text = "" + (Math.Floor(currentDoubleTime / 60000)).ToString().PadLeft(2, Char.Parse("0")) + ":" + Math.Round(currentDoubleTime % 60000 / 1000, 1).ToString("F1").PadLeft(4, Char.Parse("0"));

    }
}

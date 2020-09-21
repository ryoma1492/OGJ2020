using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public int points;
    public int flowers;
    public float time;
    public double dateDbl;
    public string name;
    public int lastLvl;
    // Start is called before the first frame update
    public ScoreData(Score score )
    {
        points = score.points;
        flowers = score.flowers;
        time = score.time;
        name = score.name;
        dateDbl = score.date.ToOADate();
        lastLvl = score.lastLvl;
    }
}

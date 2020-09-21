using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System;
using System.Linq;


public static class SaveManager
{
    private static string path = Application.persistentDataPath + "/data.ssf";

    // Start is called before the first frame update
    public static void SaveScores(Score[] scores)
    {

        List<ScoreData> datas = new List<ScoreData>();
        foreach (Score s in scores)
        {
//            Debug.Log("saving "+s.name);
            //            datas[i].flowers = s.flowers;
            //            datas[i].dateDbl = s.date.ToOADate();
            //            datas[i].name = s.name;
            //            datas[i].time = s.time;
            //            datas[i].points = s.points;
            //            datas[i].lastLvl = s.lastLvl;
            ScoreData data = new ScoreData(s);
//            Debug.Log("saving data " + data.name);
            datas.Add(data);
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, datas);
        stream.Close();
    }
    public static Score[] LoadScores()
    {
        List<Score> scores = new List<Score>();
        BinaryFormatter formatter = new BinaryFormatter();
        if (System.IO.File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            List<ScoreData> datas = (List<ScoreData>)formatter.Deserialize(stream);
            int i = 0;
            foreach (ScoreData s in datas)
            {
                Score score = new Score();
//                Debug.Log("loading " + s.name);
                score.flowers = s.flowers;
                score.date = DateTime.FromOADate(s.dateDbl);
                score.name = s.name;
                score.time = s.time;
                score.points = s.points;
                score.lastLvl = s.lastLvl;
                scores.Add(score);
                i++;
            }
            stream.Close();
        }
        else
        {
            Debug.Log("file " + path + " not found.");
        }

        return scores.ToArray();
    }
}

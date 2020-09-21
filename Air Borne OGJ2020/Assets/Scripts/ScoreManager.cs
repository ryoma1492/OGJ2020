using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Score[] scores;
    public static ScoreManager instanceScore;
    [SerializeField] private SceneManage sceneManage;
    [SerializeField] private int totalFlowers;
    [SerializeField] private int totalLevels;
    public double startTime;
    [SerializeField] private double endTime;
    [SerializeField] private List<Score> sortScores;
    public int points;
    public string name;
    private int addFlower;
    private int addLevel;

    public bool AddFlower 
    {
        set
        {
            totalFlowers++;
        }
    }

    public bool AddLevel 
    {
        set 
        {
            totalLevels++;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (instanceScore == null)
        {
            instanceScore = this;
        }
        else
        {
//            Debug.Log("Destroy");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        if (scores.Length <=0)
        {
            scores = SaveManager.LoadScores();
            sortScores = new List<Score>(scores);
            sortScores.Sort((x, y) => y.points.CompareTo(x.points));

        }
    }
    private void Start()
    {
        sceneManage = FindObjectOfType<SceneManage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveScores()
    {
        SaveManager.SaveScores(scores);
    }
    public void LoadScores()
    {
        scores = SaveManager.LoadScores();
    }
    public List<Score> SortScores()
    {
        sortScores = new List<Score>(scores);
        sortScores.Sort((x, y) => y.points.CompareTo(x.points));
        return sortScores;
    }
    public List<Score> AddScore()
    {
        Score score = new Score();
        score.name = PlayerPrefs.GetString("Name");
        score.lastLvl = totalLevels;
        score.flowers = totalFlowers;
        score.time = (float)(endTime - startTime);
        //         trying to be fair and calculate Score as (1000*flowers - 100*minutes) * (totalLevels / current level build number - 1)
        //                                                means 1 flower is worth 10 minutes      (will always be a fraction or 1)
        //
        Debug.Log("variables : (" + 1000 * totalFlowers + " - " + (100 * (score.time / 60000)) + ") * " +  (float)(score.lastLvl / sceneManage.buildIndexOfScoreboard) + " ");


        if (SceneManager.GetActiveScene().buildIndex > sceneManage.buildIndexOfScoreboard) { }
        score.points = (int)(1000 * totalFlowers - (100 * (score.time / 60000))) * score.lastLvl / (sceneManage.buildIndexOfScoreboard);
        points = score.points;
        sortScores.Add(score);
        sortScores.Sort((x, y) => y.points.CompareTo(x.points));
        return sortScores;
    }
    public void StartScoring()
    {
        startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
    public void EndScoring()
    {
        endTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        AddScore();
        scores = sortScores.ToArray();
        SaveScores();
        totalFlowers = 0;
        totalLevels = 0;
        startTime = 0;
        endTime = 0;
        points = 0;
        name="";
// set preassigned playerprefs name to blank unless input by player
        if (PlayerPrefs.GetString("Name","User").StartsWith("User"))
        {
            PlayerPrefs.SetString("Name", "");
            PlayerPrefs.DeleteKey("Name");
        }
        addFlower = 0;
        addLevel = 0;


}
}

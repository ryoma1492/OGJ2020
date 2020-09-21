using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{


    public int buildIndexOfScoreboard = 22;
    private Scene scene;
    public static int nextScene;
    public static string sceneName;
    public static SceneManage instance;
    [SerializeField] private bool isNotSingleton;
    [SerializeField] private int currentScene;
    public static int numTimesQuestions = 5;
    public static int numTimesSoFar;


    private void Awake()
    {
        if (!isNotSingleton)
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            AudioManager.instance.PlayClipAfterDone("Dreamer7");
            AudioManager.instance.PlayClipAfterDone("DreamerDrums");
        }
        AudioManager.instance.PlayClipAfterDone("Dreamer7");
        AudioManager.instance.PlayClipAfterDone("DreamerDrums");
        currentScene = scene.buildIndex;
        Debug.Log("Current Scene is  -  " + scene.name);
        Debug.Log("Current Scene is  - #" + currentScene);
        Debug.Log("Next Scene is     - #" + nextScene);
    }
    public void LoadNextScene()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Current Scene is  -  " + scene.name);
        Debug.Log("Current Scene is  - #" + currentScene);
        Debug.Log("Next Scene is     - #" + nextScene);
        AudioManager.instance.PlayClip("VeryVeryVeryCool");
        if (scene.name != "RaisedHand")
        {
            numTimesSoFar = 0;
        }
        if (SceneManager.GetActiveScene().name == "RaisedHand")
        {
            if (scene.buildIndex > 1)
            {
                ScoreManager.instanceScore.AddLevel = true;
            }
            numTimesSoFar++;
            if(numTimesSoFar >= numTimesQuestions)
            {
                LoadThisScene(0);
            }
            LoadThisScene(nextScene);
            while (!SceneManager.GetActiveScene().isLoaded)
            {
                continue;
            } 
            scene = SceneManager.GetActiveScene();
        } else
        {
            if (scene.buildIndex == 0)
            {
                ScoreManager.instanceScore.StartScoring();
                Debug.Log("Started timer");
            }

            if (scene.buildIndex >= 1 && SceneManager.GetActiveScene().name != "RaisedHand") 
            {
                ScoreManager.instanceScore.AddLevel = true; 
            }
            if (scene.buildIndex == buildIndexOfScoreboard - 1)
            {
                AudioManager.instance.StopClip("Dreamer7");
                AudioManager.instance.StopClip("DreamerDrums");
                AudioManager.instance.PlayClipAfterDone("Dreamer7");
                AudioManager.instance.PlayClipAfterDone("DreamerDrums");
                ScoreManager.instanceScore.EndScoring();
            }
            SceneManager.LoadScene(scene.buildIndex + 1);
            while (!SceneManager.GetActiveScene().isLoaded)
            {
                continue;
            }
            scene = SceneManager.GetActiveScene();
        }
    }
    public void LoadTitleScene()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Current Scene is  -  " + scene.name);
        Debug.Log("Current Scene is  - #" + currentScene);
        Debug.Log("Next Scene is     - #" + nextScene);
        nextScene = scene.buildIndex;
        ScoreManager.instanceScore.EndScoring();
        AudioManager.instance.StopClip("Dreamer7");
        AudioManager.instance.StopClip("DreamerDrums");
        AudioManager.instance.PlayClipAfterDone("Dreamer7");
        AudioManager.instance.PlayClipAfterDone("DreamerDrums");
        SceneManager.LoadScene(0);
        while (!SceneManager.GetActiveScene().isLoaded)
        {
            continue;
        }
        scene = SceneManager.GetActiveScene();
    }
    public void LoadInterimScene()
    {
        scene = SceneManager.GetActiveScene();
        nextScene = scene.buildIndex + 1;
        Debug.Log("Current Scene is  -  " + scene.name);
        Debug.Log("Current Scene is  - #" + currentScene);
        Debug.Log("Next Scene is     - #" + nextScene);
        AudioManager.instance.PlayClip("VeryVeryVeryCool");
        SceneManager.LoadScene("RaisedHand");
        while (!SceneManager.GetActiveScene().isLoaded)
        {
            continue;
        }
        scene = SceneManager.GetActiveScene();

    }
    public void ReloadScene()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Current Scene is  -  " + scene.name);
        Debug.Log("Current Scene is  - #" + currentScene);
        Debug.Log("Next Scene is     - #" + nextScene);
        AudioManager.instance.PlayClip("Pop2");
        SceneManager.LoadScene("RaisedHand");
        while (!SceneManager.GetActiveScene().isLoaded)
        {
            continue;
        }
        scene = SceneManager.GetActiveScene();

    }
    public void LoadThisScene(int buildno) 
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Current Scene is  -  " + scene.name);
        Debug.Log("Current Scene is  - #" + currentScene);
        Debug.Log("Next Scene is     - #" + nextScene);
        if (sceneName !=null)
        {
            sceneName = null;
            SceneManager.LoadScene(sceneName);
        }
        if (buildno != 0 && buildno < buildIndexOfScoreboard && SceneManager.GetActiveScene().name !="RaisedHand")
        {
            AudioManager.instance.StopClip("Dreamer7");
            AudioManager.instance.StopClip("DreamerDrums");
            AudioManager.instance.PlayClipAfterDone("Dreamer7");
            AudioManager.instance.PlayClipAfterDone("DreamerDrums");
            ScoreManager.instanceScore.StartScoring();
        }
        if (buildno == buildIndexOfScoreboard)
        {
            AudioManager.instance.StopClip("Dreamer7");
            AudioManager.instance.StopClip("DreamerDrums");
            AudioManager.instance.PlayClipAfterDone("Dreamer7");
            AudioManager.instance.PlayClipAfterDone("DreamerDrums");
        }
        if (buildno == 0)
        {
            AudioManager.instance.StopClip("Dreamer7");
            AudioManager.instance.StopClip("DreamerDrums");
            AudioManager.instance.PlayClipAfterDone("Dreamer7");
            AudioManager.instance.PlayClipAfterDone("DreamerDrums");
            ScoreManager.instanceScore.EndScoring();
        }
        AudioManager.instance.PlayClip("VeryVeryVeryCool");
        SceneManager.LoadScene(buildno);
        while (!SceneManager.GetActiveScene().isLoaded)
        {
            continue;
        }
        scene = SceneManager.GetActiveScene();
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponentNeeds : MonoBehaviour
{
    public SceneManage sceneManage;
    public Exit exit;
    public FlowerManager flowerManager;
    public GameObject player;
    public string[] HeaderText;


    private void Start()
    {
        exit = FindObjectOfType<Exit>();
        flowerManager = FindObjectOfType<FlowerManager>();
        player = FindObjectOfType<FloatyGirl>().gameObject;
        sceneManage = FindObjectOfType<SceneManage>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect : MonoBehaviour
{
    public bool collected;
    public FlowerManager FlowerManager;
    public int flowerorder;
    [SerializeField] private int currentFlower;


    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.gameObject.CompareTag("Ball"));
        if (col.gameObject.CompareTag("Ball"))
        {
            AudioManager.instance.PlayClip("FlowerGet");
            gameObject.SetActive(false);
            FlowerManager.collectFlower();
            ScoreManager.instanceScore.AddFlower=true;
        }
    }

}

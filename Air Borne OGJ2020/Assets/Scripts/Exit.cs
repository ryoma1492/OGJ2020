using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Collider2D player;
    [SerializeField] private UIComponentNeeds UISceneManage;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FloatyGirl>().gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == player)
        {
            UISceneManage.sceneManage.LoadNextScene();
        }
    }
    public GameObject getExit()
    {
        return gameObject;
    }
}

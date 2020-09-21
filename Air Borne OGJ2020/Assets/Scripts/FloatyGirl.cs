using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FloatyGirl : MonoBehaviour
{
    private Rigidbody2D rb;
//    [SerializeField] private GameObject gustPosition;
    [SerializeField] private bool isGusted;
    [SerializeField] private float maxPushSpeed = 80f;
    [SerializeField] private TextMeshProUGUI TMPro;
    private Gust gust;
    private bool isFallingOut;
    private bool isAtGoal;
    private float timeSinceStart;
    private float timeForFoundMessage = 3.5f;
    private float foundMessageTime = 0f;
    [SerializeField] private float screenFallTime = 2f;
    [SerializeField] private float screenCloseTime = 1.4f;
    [SerializeField] private Animator bubbleGirl;
    // Start is called before the first frame update
    void Start()
    {
        TMPro.text = "Explore and Find the flowers";
        rb = gameObject.GetComponent<Rigidbody2D>();
        gust = FindObjectOfType<Gust>();
    }

    void FixedUpdate()
    {
        if (isAtGoal)
        {
            timeSinceStart += Time.deltaTime;
            if (timeSinceStart>=screenCloseTime)
            {
                SceneManage.sceneName = null;
                SceneManage.nextScene = SceneManager.GetActiveScene().buildIndex+1;
                SceneManage.instance.LoadInterimScene();
            }
        }
        if(isFallingOut)
        {
            timeSinceStart += Time.deltaTime;
            if (timeSinceStart>=screenFallTime)
            {
                SceneManage.sceneName = SceneManager.GetActiveScene().name;
                SceneManage.nextScene = SceneManager.GetActiveScene().buildIndex;
                SceneManage.instance.ReloadScene();
            }
            else
            {
                if (timeSinceStart >= .5f)
                {
                    rb.AddForce(Vector2.down * maxPushSpeed * 2.3f);
                }
            }
        }
        else
        {
            if (rb.velocity.magnitude > maxPushSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxPushSpeed;
            }

        }
        if (foundMessageTime > 0)
        {
            foundMessageTime += Time.deltaTime;
            if (foundMessageTime >= timeForFoundMessage)
            {
                foundMessageTime = 0;
                TMPro.text = "Look for more Flowers";
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Damage"))
        {
            FallOut();
        }
        if (col.gameObject.CompareTag("Bell"))
        {
            TMPro.text = "You Win";
        }
        if (col.gameObject.CompareTag("Flower"))
        {
            TMPro.text = "Found a flower - keep exploring";
            gust.targets.Remove(col.gameObject);
            col.gameObject.GetComponent<Flower>().DestroyMe();
            gust.destination = null;
            gust.FindClosest();
            foundMessageTime = .01f;

        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Damage"))
        {
            FallOut();
        }
        if (col.gameObject.CompareTag("Bell"))
        {
            NextScene();
        }
        if (col.gameObject.CompareTag("Flower"))
        {
            TMPro.text = "Found a flower - keep exploring";
            gust.targets.Remove(col.gameObject);
            col.gameObject.GetComponent<Flower>().DestroyMe();
            gust.destination = null;
            gust.FindClosest();
            foundMessageTime = .01f;

        }

    }
    private void FallOut()
    {
        AudioManager.instance.PlayClip("Pop2");
        bubbleGirl.Play("Pop");
        isFallingOut = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;

    }
    private void NextScene()
    {
        isAtGoal = true;
        bubbleGirl.Play("FindGoal");
    }
}

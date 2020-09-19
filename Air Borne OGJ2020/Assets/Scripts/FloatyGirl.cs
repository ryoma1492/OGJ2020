using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FloatyGirl : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject gustPosition;
    [SerializeField] private bool isGusted;
    [SerializeField] private float maxPushSpeed = 80f;
    [SerializeField] private TextMeshProUGUI TMPro;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxPushSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxPushSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Damage"))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (col.gameObject.CompareTag("Bell"))
        {
            TMPro.text = "You Win";
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Damage"))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (col.gameObject.CompareTag("Bell"))
        {
            TMPro.text = "You Win";
        }

    }
}

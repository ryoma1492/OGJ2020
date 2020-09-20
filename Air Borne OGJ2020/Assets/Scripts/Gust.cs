using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float gustSpeed;
    [SerializeField] private float gustRadius;
    [SerializeField] private float fadeRadius;
    private Rigidbody2D[] rbList;
    [SerializeField] private GameObject gustLoc;
    [SerializeField] private bool isGusting;
    public void setGustPositon()
    {
        Debug.Log("Moved");

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gustLoc = gameObject;
    }
    void Update()
    {
        transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetMouseButtonDown(0))
        {
            isGusting = true;
        }
        if (isGusting)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isGusting = false;
            }
        }

    }
    void FixedUpdate()
    {
        if (isGusting)
        {
            blowWind();
        }
    }

    // Update is called once per frame
    public void blowWind ()
    {
        rbList = FindObjectsOfType<Rigidbody2D>();
        foreach (Rigidbody2D r in rbList)
        {
            if(rb != r)
            {
//                Debug.Log(rbList.Length);
//                Debug.Log(r.gameObject.name);
                Vector2 direction = r.transform.position - gameObject.transform.position;
                if (Vector2.Distance(r.transform.position, gameObject.transform.position) <= gustRadius)
                {
                    r.AddForce(direction.normalized * gustSpeed * gustRadius / Vector2.Distance(r.transform.position, gameObject.transform.position));
                }

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gust : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float gustSpeed;
    [SerializeField] private float gustRadius;
    [SerializeField] private float fadeRadius;
    private Rigidbody2D[] rbList;
    [SerializeField] private GameObject gustLoc;
    [SerializeField] private GameObject compass;
    [SerializeField] private bool isGusting;
    private GameObject[] objs;
    public List<GameObject> targets;
    private Animator animator;
    private GameObject player;
    public GameObject destination;
    private bool isTouching;
    private float destDistance;
    private float timeSinceMove;
    [SerializeField] private float timeBeforeCompass = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gustLoc = gameObject;
        animator = gameObject.GetComponent<Animator>();
        Debug.Log(animator.gameObject.name);
        objs = FindObjectsOfType<GameObject>();
        targets = new List<GameObject>();
        player = FindObjectOfType<FloatyGirl>().gameObject;
        foreach (GameObject obj in objs)
        {
            if (obj.CompareTag("Flower") || obj.CompareTag("Bell"))
            {
                targets.Add(obj);
            }
        }
        destination = targets[0];
        destDistance = Vector2.Distance(destination.transform.position, player.transform.position);
        FindClosest();

    }
    void Update()
    {
        if (!isTouching)
        {
            transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetMouseButtonDown(0) ||  Input.touchCount > 0)
        {
            animator.SetBool("Gusting", true);
            isGusting = true;
            if (Input.touchCount > 0)
            {
                isTouching = true;
            }
        }
        if (isGusting)
        {
            if (Input.GetMouseButtonUp(0) || Input.touchCount == 0)
            {
                animator.SetBool("Gusting", false);
                isGusting = false;
                isTouching = false;

            }
        }
        Vector3 dir = Vector3.up; 
        if (destination)
        {
            dir = destination.transform.position - player.transform.position;
        }
        float angle = Vector2.SignedAngle(Vector2.up, dir);
        compass.transform.rotation = Quaternion.Euler(0, 0, angle);
        if (!isGusting)
        {
            timeSinceMove += Time.deltaTime;
            if (timeSinceMove >= timeBeforeCompass)
            {

                FindClosest();
                dir = destination.transform.position - player.transform.position;
                angle = Vector2.SignedAngle(Vector2.up, dir);
                compass.transform.rotation = Quaternion.Euler(0, 0, angle);
                compass.GetComponentInChildren<Animator>().ResetTrigger("ShowFlower");
                timeSinceMove = 0f;
                //                Debug.Log("Playing Compass animation");
                compass.GetComponentInChildren<Animator>().SetTrigger("ShowFlower");
            }
        }
        if (isGusting)
        {
            timeSinceMove = 0f;
            AudioManager.instance.PlayClipAfterDone("PinwheelTurn");
        }

    }
    void FixedUpdate()
    {
        if (isGusting)
        {
            blowWind();
        }

    }
    private void OnMouseDrag()
    {
        if (isTouching)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = objPosition;

        }
    }
    public void FindClosest()
    {
        foreach (GameObject obj in targets)
        {
            float objDistance = Vector2.Distance(obj.transform.position, player.transform.position);
            if (destination)
            {
                destDistance = Vector2.Distance(destination.transform.position, player.transform.position);
            } 
            else 
            {
                destDistance = 9999999f;
            }
            if (objDistance < destDistance)
            {
                destDistance = Vector2.Distance(obj.transform.position, player.transform.position);
//                Debug.Log(destination.name + " is farther away than " + obj.name);
                destination = obj;
            }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unSpin : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private float smoothing = .5f;
    [SerializeField] private float cameraSmoothSpeed = 2f;
    [SerializeField] private bool isSmoothingY;
    [SerializeField] private Vector2 direction;
    public bool isDonePreview;
    [SerializeField] private Vector2 previewPoint;
    [SerializeField] private float previewTime;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FloatyGirl>().gameObject.transform;
        previewPoint = gameObject.GetComponentInChildren<Transform>().position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDonePreview)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            direction = (player.position - transform.position);
            /*
                    if (transform.position.y > player.position.y)
                    {
                        isSmoothingY = true;
                        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, player.position.y + direction.y * Time.deltaTime, transform.position.z), ref velocity, smoothing, cameraSmoothSpeed);
                    }
                    if (transform.position.y < player.position.y)
                    {
                        isSmoothingY = true;
                        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, player.position.y + direction.y * Time.deltaTime, transform.position.z), ref velocity, smoothing, cameraSmoothSpeed);
                    }
                    else
                    {
                        isSmoothingY = false;
                        transform.position = Vector3.SmoothDamp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(player.position.x, player.position.y, transform.position.z), ref velocity, smoothing, cameraSmoothSpeed);
                    }
            */
            Vector3 dirXZ = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, player.position.y, transform.position.z), ref velocity, smoothing, cameraSmoothSpeed);
            Vector3 dirY = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, player.position.y, transform.position.z), ref velocity, smoothing, cameraSmoothSpeed * 2);
            transform.position = new Vector3(dirXZ.x, dirY.y, transform.position.z);
        }
    }
}

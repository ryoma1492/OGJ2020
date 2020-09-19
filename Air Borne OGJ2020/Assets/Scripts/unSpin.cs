using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unSpin : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private float smoothing = .5f;
    [SerializeField] private float cameraSmoothSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FloatyGirl>().gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, player.position.y, transform.position.z),ref velocity,smoothing,cameraSmoothSpeed);
    }
}

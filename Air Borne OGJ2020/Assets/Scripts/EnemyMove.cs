using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class EnemyMove : MonoBehaviour
{
    public static Transform player;
    [SerializeField] private Vector3 origin;
    private Vector3 originBoundLeft;
    private Vector3 originBoundRight;
    [SerializeField] private bool isGoingRight;


    private enum EnemyMovementType
    {
        Pursuit,
        HorizontalRight,
        VerticalUp,
        HorizontalLeft,
        VerticalDown,
        Sway,
        Return
    }
    [SerializeField] private bool DetectCollisions;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float delayBetweenMoves = 0f;
    [SerializeField] private EnemyMovementType movementType;
    [SerializeField] private float swayDistance = 5f;
    private float timeDelayTotal = 0f;
    private bool isDelaying = false;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FloatyGirl>().transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (DetectCollisions)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        origin = gameObject.transform.position;
        originBoundLeft = origin + new Vector3(-swayDistance, 0f, 0f);
        originBoundRight = origin + new Vector3(swayDistance, 0f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDelaying)
        {
            timeDelayTotal += Time.deltaTime;
            if (timeDelayTotal>=delayBetweenMoves)
            {
                timeDelayTotal = 0f;
                isDelaying = false;
            }
        } else
        {
            switch (movementType)
            {
                case EnemyMovementType.HorizontalLeft:
                    MoveLinear(Vector2.left);
                    break;
                case EnemyMovementType.VerticalUp:
                    MoveLinear(Vector2.up);
                    break;
                case EnemyMovementType.HorizontalRight:
                    MoveLinear(Vector2.right);
                    break;
                case EnemyMovementType.VerticalDown:
                    MoveLinear(Vector2.down);
                    break;
                case EnemyMovementType.Pursuit:
                    Pursue();
                    break;
                case EnemyMovementType.Return:
                    Return(origin);
                    break;
                default:
                    Sway();
                    break;
            }
            if (delayBetweenMoves > 0f)
            {
                isDelaying = true;
            }
        }
    }

    private void MoveLinear(Vector2 direction)
    {
        rb.AddForce(direction * movementSpeed);
    }
    private void Pursue()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * movementSpeed);
    }
    private void Return(Vector3 destination)
    {
        Vector2 direction = (destination - transform.position).normalized;
        rb.AddForce(direction * movementSpeed);
    }
    private void Sway()
    {
        if (Vector2.Distance(originBoundLeft, transform.position) <= .5f)
        {
            isGoingRight = true;
        }
        if (Vector2.Distance(originBoundRight, transform.position) <= .5f && isGoingRight)
        {
            isGoingRight = false;
        }
        if (!isGoingRight)
        {
            Return(originBoundLeft);
        }
        else
        {
            Return(originBoundRight);
        }

    }
}

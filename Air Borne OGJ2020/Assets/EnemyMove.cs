using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public static Transform player;


    private enum EnemyMovementType
    {
        Pursuit,
        HorizontalRight,
        VerticalUp,
        HorizontalLeft,
        VerticalDown,
        Sway
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
        rb.AddForce(direction * movementSpeed * Time.deltaTime);
    }
    private void Pursue()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * movementSpeed * Time.deltaTime);
    }
    private void Sway()
    {

    }
}

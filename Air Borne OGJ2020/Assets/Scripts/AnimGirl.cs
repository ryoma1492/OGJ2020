using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimGirl : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    [SerializeField] private bool isFalling;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (isFalling)
        {
            isFalling = false;
            animator.Play("Fall");
        }
    }
}

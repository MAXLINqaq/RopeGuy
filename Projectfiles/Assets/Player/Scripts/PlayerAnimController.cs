using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    private Rigidbody2D rb;
    private float Vx;
    private float Vy;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vx = Mathf.Abs(rb.velocity.x);
        Vy = Mathf.Abs(rb.velocity.y);
        if (Vx < 0.01f && Vy < 0.01f)
        {
            animator.SetBool("isIdle", true);
        }
        else { animator.SetBool("isIdle", false); }
        if (playerController.onGround)
        {
            animator.SetBool("isJumping", false);
            if (Vx > 0.01f)
            {
                animator.SetBool("isRunning", true);
            }
            else { animator.SetBool("isRunning", false); }
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerController maid;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        maid = GetComponent<PlayerController>();
    }

    private void Update()
    {
        anim.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Ground", maid.isGround);
        anim.SetBool("Jump", maid.isJump);
        anim.SetBool("Fall", maid.isFall);
        anim.SetBool("Crouch", maid.isCrouch);
        anim.SetBool("Dash", maid.isDashing);
    }

    #region Animation Event


    #endregion
}

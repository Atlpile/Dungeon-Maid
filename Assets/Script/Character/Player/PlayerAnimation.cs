using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerController maidController;
    private PlayerStatus maidStatus;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        maidController = GetComponent<PlayerController>();
        maidStatus = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        anim.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Ground", maidController.isGround);
        anim.SetBool("Jump", maidController.isJump);
        anim.SetBool("Fall", maidController.isFall);
        anim.SetBool("Crouch", maidController.isCrouch);
        anim.SetBool("Dash", maidController.isDashing);
        anim.SetBool("Dead", maidStatus.isDead);
    }

    #region Animation Event


    #endregion
}

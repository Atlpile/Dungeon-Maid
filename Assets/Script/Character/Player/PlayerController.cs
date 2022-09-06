using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 角色参数

    [Header("移动设置")]
    [SerializeField] private float startSpeed = 10f;
    [DisplayOnly][SerializeField] private float currentSpeed;
    [DisplayOnly] public float playerHorizontalMove;

    [Header("跳跃设置")]
    [SerializeField] private float jumpForce = 10f;
    [DisplayOnly][SerializeField] private int startJumpCount = 1;
    [DisplayOnly][SerializeField] private int currentJumpCount;

    [Header("角色状态")]
    [DisplayOnly] public bool isRight;
    [DisplayOnly] public bool isGround;
    [DisplayOnly] public bool isJump;
    [DisplayOnly] public bool isFall;
    [DisplayOnly] public bool isCrouch;
    [DisplayOnly] public bool isDashing;
    [DisplayOnly] private bool jumpPressed;
    [DisplayOnly] private bool crouchHeld;

    [Header("Reference")]
    [HideInInspector] public Rigidbody2D rb;
    private PlayerCheck maidCheck;
    private PlayerDash maidDash;
    private PlayerFire maidFire;
    private PlayerStatus maidStatus;

    #endregion


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        maidCheck = GetComponent<PlayerCheck>();
        maidDash = GetComponent<PlayerDash>();
        maidFire = GetComponent<PlayerFire>();
        maidStatus = GetComponent<PlayerStatus>();
    }

    private void Start()
    {
        currentSpeed = startSpeed;
        isRight = true;
    }

    private void Update()
    {
        InputCheck();
    }

    private void FixedUpdate()
    {
        maidCheck.GroundCheck();

        CrouchState();
        Jump();
        GroundMovement();

        if (isDashing) maidDash.Dash();
    }


    #region 角色基本功能

    private void GroundMovement()
    {
        Flip();

        playerHorizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(playerHorizontalMove * currentSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        if (playerHorizontalMove > 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            // transform.localScale = new Vector2(1, transform.localScale.y);
            isRight = true;
        }
        else if (playerHorizontalMove < 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            // transform.localScale = new Vector2(-1, transform.localScale.y);
            isRight = false;
        }
    }

    private void Jump()
    {
        JumpState();

        if (isGround && jumpPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            AudioManager.Instance.SoundPlay("player_jump", 0.5f);
        }
        else if (!isGround && jumpPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            AudioManager.Instance.SoundPlay("player_jump");
            currentJumpCount--;
        }

        jumpPressed = false;
    }

    private void JumpState()
    {
        if (isGround)
        {
            currentJumpCount = startJumpCount;
            isJump = false;
            isFall = false;
        }

        if (!isGround)
        {
            if (rb.velocity.y > 0.1)
            {
                isJump = true;
                isFall = false;
            }
            else if (rb.velocity.y < -0.1)
            {
                isJump = false;
                isFall = true;
            }
        }
    }

    private void Crouch()
    {
        isCrouch = true;
        currentSpeed = 0;
    }

    private void StandUp()
    {
        isCrouch = false;
        currentSpeed = startSpeed;
    }

    private void CrouchState()
    {
        if (isGround)
        {
            if (crouchHeld)
                Crouch();
            else if (!crouchHeld)
                StandUp();
        }

        // else if (!isGround && isHeadBlocked)                 //处于地面，头部被遮挡（修复了处于空中状态时执行下蹲操作）
        //     Crouch();
        // else if (!crouchHeld && isCrouch)
        //     StandUp();

    }

    private void InputCheck()
    {
        //Fire输入检测
        if (Input.GetButton("Fire"))
        {
            maidFire.currentStoreTime += Time.deltaTime;
        }
        else if (Input.GetButtonUp("Fire"))
        {
            //未超过蓄力攻击时间
            if (maidFire.currentStoreTime < maidFire.storeTime)
            {
                if (maidFire.currentFireCD <= 0)
                {
                    if (isCrouch)
                        maidFire.CrouchFireSmallBullet();
                    else if (!isCrouch)
                        maidFire.StandFireSmallBullet();

                    maidFire.currentFireCD = maidFire.fireCD;
                }
            }
            //超过蓄力攻击时间
            else if (maidFire.currentStoreTime >= maidFire.storeTime)
            {
                if (isCrouch)
                {
                    maidFire.CrouchFireMiddleBullet();
                }
                else if (!isCrouch)
                {
                    maidFire.StandFireMiddleBullet();
                }
            }
            maidFire.currentStoreTime = 0;
        }
        else
        {
            if (maidFire.currentFireCD > 0)
                maidFire.currentFireCD -= Time.deltaTime;
        }

        //Dash输入检测
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //若游戏当前时间 >= 上一次使用冲锋的时间点+冲刺CD时间（CD时间）
            if (Time.time >= (maidDash.lastDashTimePoint + maidDash.dashCD))
            {
                maidDash.ReadyToDash();
                //TODO:冲刺消耗蓝量
                maidStatus.LossMagic(5);

            }
        }

        //Crouch输入检测
        crouchHeld = Input.GetButton("Crouch");
        if (crouchHeld) return;

        //Jump输入检测
        if (Input.GetButtonDown("Jump") && isGround)
            jumpPressed = true;
        if (Input.GetButtonDown("Jump") && currentJumpCount > 0)
            jumpPressed = true;

    }

    #endregion
}

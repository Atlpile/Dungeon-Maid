using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private PlayerController maidController;
    private Collider2D dashColl;

    [Header("Dash参数")]
    [SerializeField] private float dashSpeed = 50f;
    [SerializeField] private float startDashTime = 0.3f;
    [SerializeField] private float currentDashTime;
    public int dashLossMagic = 10;
    public float dashCD = 0.5f;
    public float lastDashTimePoint = -10f;                       //上次使用冲刺的时间点

    private void Awake()
    {
        maidController = GetComponent<PlayerController>();
        dashColl = GetComponent<Collider2D>();
    }

    public void ReadyToDash()
    {
        maidController.isDashing = true;
        AudioManager.Instance.SoundPlay("player_dash");
        currentDashTime = startDashTime;
        lastDashTimePoint = Time.time;                                   //设置上次Dash的时间点
    }

    public void Dash()
    {
        if (currentDashTime > 0)
        {
            if (maidController.rb.velocity.y > 0 && !maidController.isGround)
            {
                maidController.rb.velocity = new Vector2(dashSpeed * maidController.playerHorizontalMove, maidController.rb.velocity.y);
            }


            if (transform.localEulerAngles.y > 0)
                maidController.rb.velocity = new Vector2(dashSpeed * -transform.localScale.x, maidController.rb.velocity.y);
            else
                maidController.rb.velocity = new Vector2(dashSpeed * transform.localScale.x, maidController.rb.velocity.y);

            currentDashTime -= Time.deltaTime;

            //在对象池中获取残影
            // ShadowPool.instance.GetFromPool();                                      
            PoolManager.Instance.GetDictObj("Prefab/shadow", (obj) => { });
        }

        //若冲刺的剩余时间 <= 0
        if (currentDashTime <= 0)
        {
            maidController.isDashing = false;

            //【Bug修复】修复Player下蹲滑行时的Bug
            maidController.rb.velocity = new Vector2(0, maidController.rb.velocity.y);

            //若人物不在地面（冲刺过程中仍在空中）
            if (!maidController.isGround)
            {
                maidController.rb.velocity = new Vector2(dashSpeed * maidController.playerHorizontalMove, maidController.rb.velocity.y);
            }
        }

    }
}

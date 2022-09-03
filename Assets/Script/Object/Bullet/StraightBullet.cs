using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : Bullet
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        InitBullet();
    }

    protected override void OnEnable()
    {
        StraightMove();
    }

    protected override void InitBullet()
    {
        bulletSpeed = 50f;
        destroyDistance = 300f;
    }

    protected override void FixedUpdate()
    {
        if (currentDestroyTime <= 0)
        {
            bulletAnim.SetTrigger("Destroy");
            currentDestroyTime = destroyTime;
        }
        else
        {
            //TODO:计秒器用协程实现
            currentDestroyTime -= Time.fixedDeltaTime;
        }
    }

    //直线移动
    private void StraightMove()
    {
        if (maidController.isRight)
            rb.velocity = Vector3.right * bulletSpeed;
        else if (!maidController.isRight)
            rb.velocity = -Vector3.right * bulletSpeed;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }


}


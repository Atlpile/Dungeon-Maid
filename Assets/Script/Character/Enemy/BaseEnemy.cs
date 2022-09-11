using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public E_EnemyType enemyType;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D coll;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        Init();
    }

    protected virtual void Init()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected void GroundMovement(bool isRight, float moveSpeed)
    {
        if (isRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            rb.velocity = Vector2.right * moveSpeed;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = Vector2.right * -moveSpeed;
        }
    }


}

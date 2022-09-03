using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [Header("子弹设置")]
    public float bulletSpeed;
    public float destroyDistance;
    public float destroyTime = 5f;
    public float currentDestroyTime;
    public float upForce;
    protected Vector3 startPos;

    [Header("Reference")]
    protected Rigidbody2D rb;
    protected Animator bulletAnim;
    protected Collider2D coll;
    protected PlayerController maidController;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletAnim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        maidController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    protected abstract void Start();

    protected abstract void OnEnable();

    protected abstract void FixedUpdate();

    protected abstract void InitBullet();


    //上抛下落

    //下落

    //跟踪
    private void MoveToPlayer()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            bulletAnim.SetTrigger("Destroy");
            AudioManager.Instance.SoundPlay("bullet_hitWall", 0.5f);
            rb.velocity = Vector2.zero;
            coll.enabled = false;
        }
    }

    //Animaton Event
    public void ReturnBullet()
    {
        Destroy(gameObject);
        // PoolManager.Instance.ReturnDictObj(this.gameObject.name, this.gameObject);
        bulletAnim.SetTrigger("Resume");
        coll.enabled = true;
        currentDestroyTime = destroyTime;
    }
}

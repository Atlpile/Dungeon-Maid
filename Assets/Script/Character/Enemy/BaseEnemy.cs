using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public E_EnemyType enemyType;
    [DisplayOnly] public E_EnemyState currentState;


    private BaseState baseState;
    public PatrolState patrolState = new PatrolState();
    public GuardState guardState = new GuardState();
    public AttackState attackState = new AttackState();
    public ChaseState chaseState = new ChaseState();
    public FlyState flyState = new FlyState();

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
        StartState();
    }

    protected virtual void Update()
    {
        if (baseState != null)
            baseState.OnUpdate(this);
    }

    protected virtual void FixedUpdate()
    {
        if (baseState != null)
            baseState.OnFixedUpdate(this);
    }

    public void SwitchState(BaseState state)
    {
        if (baseState != null)
        {
            baseState.OnExit(this);
        }

        baseState = state;
        baseState.OnStart(this);
    }

    private void StartState()
    {
        switch (enemyType)
        {
            case E_EnemyType.Ground:
                SwitchState(patrolState);
                break;
            case E_EnemyType.Fly:
                SwitchState(flyState);
                break;
            case E_EnemyType.Static:
                SwitchState(guardState);
                break;
            case E_EnemyType.Boss:
                break;
            case E_EnemyType.Guard:
                SwitchState(guardState);
                break;
        }
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

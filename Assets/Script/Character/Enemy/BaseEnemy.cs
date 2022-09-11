using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    private BaseState baseState;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D coll;

    public E_EnemyType enemyType;
    [DisplayOnly] public E_EnemyState currentState;
    public PatrolState patrolState = new PatrolState();
    public GuardState guardState = new GuardState();
    public AttackState attackState = new AttackState();
    public ChaseState chaseState = new ChaseState();
    public FlyState flyState = new FlyState();

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
                break;
            case E_EnemyType.Static:
                break;
            case E_EnemyType.Boss:
                break;
            case E_EnemyType.Guard:
                SwitchState(guardState);
                break;
        }
    }

}

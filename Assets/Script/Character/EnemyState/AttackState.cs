using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public override void OnStart(BaseEnemy baseEnemy)
    {
        baseEnemy.currentState = E_EnemyState.AttackState;
        Debug.Log("进入AttackState");
    }

    public override void OnUpdate(BaseEnemy baseEnemy)
    {

    }

    public override void OnFixedUpdate(BaseEnemy baseEnemy)
    {

    }

    public override void OnExit(BaseEnemy baseEnemy)
    {
        baseEnemy.currentState = E_EnemyState.Null;
        Debug.Log("退出AttackState");
    }
}

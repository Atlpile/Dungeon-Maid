using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardState : BaseState
{
    public override void OnStart(BaseEnemy baseEnemy)
    {
        baseEnemy.currentState = E_EnemyState.GuardState;

        Debug.Log("进入GuardState");


    }

    public override void OnUpdate(BaseEnemy baseEnemy)
    {
        // Debug.Log("执行GuardState");
    }

    public override void OnFixedUpdate(BaseEnemy baseEnemy)
    {

    }

    public override void OnExit(BaseEnemy baseEnemy)
    {
        baseEnemy.currentState = E_EnemyState.Null;

        Debug.Log("退出GuardState");
    }
}

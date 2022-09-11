using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public override void OnStart(BaseEnemy baseEnemy)
    {
        baseEnemy.currentState = E_EnemyState.PatrolState;

        Debug.Log("进入PatrolState");
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

        Debug.Log("退出PatrolState");
    }
}

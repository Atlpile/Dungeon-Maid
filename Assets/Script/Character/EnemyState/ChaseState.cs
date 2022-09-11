using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public override void OnStart(BaseEnemy baseEnemy)
    {
        baseEnemy.currentState = E_EnemyState.ChaseState;

        Debug.Log("进入ChaseState");
    }

    public override void OnUpdate(BaseEnemy baseEnemy)
    {
        throw new System.NotImplementedException();
    }

    public override void OnFixedUpdate(BaseEnemy baseEnemy)
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit(BaseEnemy baseEnemy)
    {
        baseEnemy.currentState = E_EnemyState.Null;
        Debug.Log("退出ChaseState");
    }
}

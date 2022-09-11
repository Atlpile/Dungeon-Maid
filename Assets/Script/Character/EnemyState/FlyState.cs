using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyState : BaseState
{
    public override void OnStart(BaseEnemy baseEnemy)
    {
        baseEnemy.currentState = E_EnemyState.FlyState;

        Debug.Log("进入FlyState");
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

        Debug.Log("退出FlyState");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void OnStart(BaseEnemy baseEnemy);

    public abstract void OnUpdate(BaseEnemy baseEnemy);

    public abstract void OnFixedUpdate(BaseEnemy baseEnemy);

    public abstract void OnExit(BaseEnemy baseEnemy);
}

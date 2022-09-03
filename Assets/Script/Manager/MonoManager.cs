using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoManager : BaseManager<MonoManager>
{
    public MonoController controller;

    public MonoManager()
    {
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }

    protected override void Awake()
    {
        base.Awake();
    }


}

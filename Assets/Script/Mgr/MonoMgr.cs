using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class MonoMgr : BaseManager<MonoMgr>
{
    private MonoController controller;

    public MonoMgr()
    {
        //保证了MonoController对象的唯一性
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }


    public void AddUpdateListener(UnityAction fun)
    {
        controller.AddUpdateListener(fun);
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        controller.RemoveUpdateListener(fun);
    }

    // public Coroutine StartCoroutine(IEnumerator routine)
    // {
    //     return controller.StartCoroutine(routine);
    // }

    // public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    // {
    //     return controller.StartCoroutine(methodName, value);
    // }

    // public Coroutine StartCoroutine(string methodName)
    // {
    //     return controller.StartCoroutine(methodName);
    // }
}
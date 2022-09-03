using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    public event UnityAction updateEvent;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (updateEvent != null)
        {
            updateEvent();
        }
    }

    //给外部用于添加帧更新事件的函数
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }

    //给外部用于移除帧更新事件的函数
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }
}

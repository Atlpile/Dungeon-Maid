using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : BaseManager<EventCenter>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private Dictionary<string, UnityAction<object>> eventDic = new Dictionary<string, UnityAction<object>>();

    //事件监听
    public void AddEventListener(string name, UnityAction<object> action)
    {
        //字典有事件监听
        if (eventDic.ContainsKey(name))
        {
            eventDic[name] += action;
        }
        //字典没有事件监听
        else
        {
            eventDic.Add(name, action);
        }
    }

    //事件触发
    public void EventTrigger(string name, object info)
    {
        //字典有事件，则执行委托
        if (eventDic.ContainsKey(name))
        {
            eventDic[name].Invoke(info);
        }
    }

    //事件移除
    public void RemoveEventListener(string name, UnityAction<object> action)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic[name] -= action;
        }
    }

    //清空事件（用于切换场景）
    public void Clear()
    {
        eventDic.Clear();
    }


}

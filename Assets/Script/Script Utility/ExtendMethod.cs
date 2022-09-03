using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendMethod
{
    public static T EM_TryGetValue<T>(this Dictionary<string, object> objectDic, string key)
    {
        if (objectDic.TryGetValue(key, out var value))
        {
            return (T)value;
        }

        Debug.LogError("Error:未找到该object的键名" + key + ", 请确保该键已被设置");
        return default(T);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool instance;
    public static ObjectPool Instance => instance;
    private Dictionary<string, GameObject> GameObjectDic;
    private Dictionary<string, List<IObject>> GameObjectPool;

    public ObjectPool()
    {
        GameObjectDic = new Dictionary<string, GameObject>();
        GameObjectPool = new Dictionary<string, List<IObject>>();
    }

    /// <summary>
    /// 获取Resource中的Object
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="name">Resource中的资源名称</param>
    /// <returns></returns>
    public GameObject AddObject(E_ResourceType type, string name)
    {
        if (GameObjectDic.ContainsKey(name))
            return GameObjectDic[name];

        GameObject gameObject = ResourceLoader.Instance.Load(type, name) as GameObject;
        if (gameObject == null)
        {
            Debug.LogWarning("ObjectPool:不能添加该对象名称的物体: " + name + "请检查你的ResourceType和对象名称是否正确.");
            return null;
        }
        gameObject.SetActive(false);
        GameObjectDic.Add(name, gameObject);
        GameObjectPool.Add(name, new List<IObject>());
        return gameObject;
    }

    public bool AddObject(string name, GameObject obj)
    {
        if (GameObjectDic.ContainsKey(name))
            return false;

        obj.SetActive(false);
        GameObjectDic.Add(name, obj);
        GameObjectPool.Add(name, new List<IObject>());
        return true;
    }


    private IObject CreateNew(string name, Dictionary<string, object> valueDic, Transform parent = null)
    {
        IObject component = Object.Instantiate(GameObjectDic[name], parent, worldPositionStays: false).GetComponent<IObject>();
        component.Create(valueDic);
        GameObjectPool[name].Add(component);
        return component;
    }

    private void Remove(string name, List<IObject> iobjects)
    {
        foreach (IObject iobject in iobjects)
        {
            GameObjectPool[name].Remove(iobject);
        }
    }

    public IObject GetObject(string name, Dictionary<string, object> valueDic, Transform parent = null)
    {
        if (!GameObjectDic.ContainsKey(name))
        {
            Debug.LogError("ObjectPool:未得到该object名称" + name + ", 请检查该object名称且确保该游戏对象已被添加.");
            return null;
        }

        //获取标有IObject接口的脚本
        List<IObject> list = new List<IObject>();
        foreach (IObject item in GameObjectPool[name])
        {
            try
            {
                if (!item.IsActive)
                {
                    item.Create(valueDic);
                    return item;
                }
            }
            catch
            {
                list.Add(item);
            }
        }
        Remove(name, list);
        return CreateNew(name, valueDic, parent);
    }
}

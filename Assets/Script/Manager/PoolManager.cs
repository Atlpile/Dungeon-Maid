using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolData
{
    public GameObject fatherObj;
    //对象池容器
    public List<GameObject> poolList = new List<GameObject>();

    //初始化对象池数据
    public PoolData(GameObject obj, GameObject poolRootObj)
    {
        //创建对象池的父对象，名称为传入的对象名
        fatherObj = new GameObject(obj.name);
        //设置创建的父对象，为对象池根节点的子物体
        fatherObj.transform.parent = poolRootObj.transform;
        ReturnPoolObj(obj);
    }

    /// <summary>
    /// 失活对象并返回至对象池
    /// </summary>
    /// <param name="obj"></param>
    public void ReturnPoolObj(GameObject obj)
    {
        //失活对象
        obj.SetActive(false);
        //存储失活的对象
        poolList.Add(obj);
        //设置为该名称父对象的子物体
        obj.transform.parent = fatherObj.transform;
    }

    /// <summary>
    /// 取出对象池的对象
    /// </summary>
    /// <returns></returns>
    public GameObject GetPoolObj()
    {
        GameObject obj = null;
        //将取出的obj设为列表中第一个对象，并在列表中移除
        obj = poolList[0];
        poolList.RemoveAt(0);
        //激活对象
        obj.SetActive(true);
        //断开根节点关系
        obj.transform.parent = obj.transform.parent;

        return obj;
    }
}

public class PoolManager : BaseManager<PoolManager>
{
    public Dictionary<string, PoolData> PoolDict = new Dictionary<string, PoolData>();
    private GameObject poolRootObj;

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 获取对象池中的物体
    /// </summary>
    /// <param name="resName">物体名称</param>
    /// <param name="callBack">创建完资源后对资源的设置</param>
    public void GetDictObj(string resName, UnityAction<GameObject> callBack)
    {
        //若对象池中有该对象种类，且有该对象
        if (PoolDict.ContainsKey(resName) && PoolDict[resName].poolList.Count > 0)
        {
            //则获取对象池中的对象
            callBack(PoolDict[resName].GetPoolObj());
        }
        //若对象池中没有该对象种类，则加载资源创建对象
        else
        {
            ResourcesManager.Instance.LoadAsync<GameObject>(resName, (o) =>
            {
                //使对象名和加载的资源名相同
                o.name = resName;
                callBack(o);
            });
        }
    }

    // public void GetDictAudio(string name, UnityAction<AudioClip> callBack)
    // {
    //     if (PoolDict.ContainsKey(name) && PoolDict[name].poolList.Count > 0)
    //     {
    //         callBack(PoolDict[name].GetPoolObj());
    //     }
    //     else
    //     {
    //         ResourcesManager.Instance.LoadAsync<AudioClip>(name, (audioClip) =>
    //         {
    //             audioClip.name = name;
    //             callBack(audioClip);
    //         });
    //     }
    // }

    /// <summary>
    /// 返回不用的物体至对象池
    /// </summary>
    /// <param name="name">物体名称</param>
    /// <param name="obj">返回的对象</param>
    public void ReturnDictObj(string name, GameObject obj)
    {
        //若PoolManager没有对象池根节点，则新建
        if (poolRootObj == null)
            poolRootObj = new GameObject("Pool");

        //若对象池中有该对象种类，则直接返回至对象池
        if (PoolDict.ContainsKey(name))
            PoolDict[name].ReturnPoolObj(obj);
        //若没有，则新加对象池种类
        else
            PoolDict.Add(name, new PoolData(obj, poolRootObj));
    }

    //过场景时，清除对象池
    public void ClearDictObj()
    {
        PoolDict.Clear();
        poolRootObj = null;
    }


}

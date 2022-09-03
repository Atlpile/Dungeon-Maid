using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesManager : BaseManager<ResourcesManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pathAndName"></param>
    /// <returns></returns>
    public T Load<T>(string pathAndName) where T : Object
    {
        T res = Resources.Load<T>(pathAndName);

        if (res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
    }

    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <param name="pathAndName"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    public void LoadAsync<T>(string pathAndName, UnityAction<T> callback) where T : Object
    {
        StartCoroutine(ReallyLoadAsync(pathAndName, callback));
    }

    private IEnumerator ReallyLoadAsync<T>(string pathAndName, UnityAction<T> callback) where T : Object
    {
        ResourceRequest res = Resources.LoadAsync<T>(pathAndName);
        yield return res;

        // if (res == null)
        //     Debug.LogError("ResourceManager: 未找到该资源名" + pathAndName + ", 请检查你的Resources的资源路径是否正确.");

        if (res.asset is GameObject)
            callback(GameObject.Instantiate(res.asset) as T);
        else
            callback(res.asset as T);
    }



}

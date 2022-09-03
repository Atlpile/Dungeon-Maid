using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader
{
    private static ResourceLoader instance;
    public static ResourceLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ResourceLoader();
            }
            return instance;
        }
    }

    private Dictionary<E_ResourceType, string> ResourcePath;
    private Dictionary<E_ResourceType, Type> ResourceTypeDic;

    public ResourceLoader()
    {
        ResourcePath = new Dictionary<E_ResourceType, string>
        {
            {E_ResourceType.UIPrefab,"UIPrefab/"},
            { E_ResourceType.UISprite,"UISprite/"},
            { E_ResourceType.Sprite,"Sprite/"},
            { E_ResourceType.Prefab,"Prefab/"},
            { E_ResourceType.Audio,"Audio/"},
            { E_ResourceType.Effect,"Effect/"},
            { E_ResourceType.TextAsset,"Text/"}
        };

        ResourceTypeDic = new Dictionary<E_ResourceType, Type>
        {
            {E_ResourceType.UISprite,typeof(Sprite)},
            {E_ResourceType.Sprite,typeof(Sprite)},
            {E_ResourceType.Prefab,typeof(GameObject)},
            {E_ResourceType.UIPrefab,typeof(GameObject)},
            {E_ResourceType.Audio,typeof(AudioClip)},
            {E_ResourceType.Effect,typeof(GameObject)},
            {E_ResourceType.TextAsset,typeof(TextAsset)}
        };
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="name">Resource路径下的资源名称</param>
    /// <returns></returns>
    public object Load(E_ResourceType type, string name)
    {
        object obj = Resources.Load(ResourcePath[type] + name, ResourceTypeDic[type]);
        if (obj == null)
        {
            Debug.LogError("ResourceLoader: 未找到该资源名" + name + ", 类型为" + Enum.GetName(typeof(E_ResourceType), type) + ", 请检查你的ResourceType和Name是否正确.");
        }
        return obj;
    }

    public void ReleaseResource()
    {
        Resources.UnloadUnusedAssets();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel<T> : MonoBehaviour where T : class
{
    private static T instance;
    public static T Instance => instance;

    private void Awake()
    {
        //通过类名.Instance得到脚本，且约束T的类型为Class
        instance = this as T;
    }


    public virtual void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TransitionManager : BaseManager<TransitionManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    //同步加载场景
    public void LoadScene(string name, UnityAction fun)
    {
        SceneManager.LoadScene(name);

        //加载完后，执行的内容
        fun();
    }

    //异步加载场景方法
    public void LoadSceneAsyn(string name, UnityAction fun)
    {
        StartCoroutine(IE_LoadSceneAsyn(name, fun));
    }

    //协程异步加载场景
    private IEnumerator IE_LoadSceneAsyn(string name, UnityAction fun)
    {
        var ao = SceneManager.LoadSceneAsync(name);
        //得到场景加载的进度
        while (!ao.isDone)
        {
            //事件中心更新进度情况，可以向外分发进度
            EventCenter.Instance.EventTrigger("进度条更新", ao.progress);
            //更新进度条
            yield return ao.progress;
        }

        //加载完后，执行的内容
        fun();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : BaseManager<InputController>
{
    protected override void Awake()
    {
        base.Awake();
    }


    private void MyUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            EventCenter.Instance.EventTrigger("某键按下", KeyCode.W);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            EventCenter.Instance.EventTrigger("某键抬起", KeyCode.W);
        }
    }
}

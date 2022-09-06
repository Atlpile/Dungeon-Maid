using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraBounds : MonoBehaviour
{
    public Cinemachine.CinemachineConfiner confiner;

    private void Awake()
    {
        confiner = GameObject.FindGameObjectWithTag("CineMachine").GetComponent<Cinemachine.CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CameraBound"))
        {
            confiner.m_BoundingShape2D = other;
            Debug.Log("进入CameraBound");
        }
    }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("CameraBound"))
    //     {
    //         confiner.m_BoundingShape2D = null;
    //         Debug.Log("退出CameraBound");
    //     }
    // }
}

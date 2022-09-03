using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D platform;
    private void Awake()
    {
        platform = GetComponent<PlatformEffector2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Space))
            {
                platform.rotationalOffset = 180;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        platform.rotationalOffset = 0;
    }
}

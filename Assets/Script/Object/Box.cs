using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public E_BoxType boxType;
    public float fadeTime = 2f;
    public float destroyTime = 2f;
    public bool isOpen;
    public bool canOpen;

    [Header("Reference")]
    private Animator boxAnim;

    private void Awake()
    {
        boxAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        isOpen = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CanOpenBox();

            //ATTENTION:使用GetKeyDown则输入不灵敏
            if (Input.GetKey(KeyCode.E))
            {
                if (!isOpen && canOpen)
                {
                    StartCoroutine(IE_BoxDisappear());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
        }
    }

    private void CanOpenBox()
    {
        switch (boxType)
        {
            case E_BoxType.Red:
                if (GameManager.Instance.hasRedKey) canOpen = true;
                break;
            case E_BoxType.Blue:
                if (GameManager.Instance.hasBlueKey) canOpen = true;
                break;
            case E_BoxType.Green:
                if (GameManager.Instance.hasGreenKey) canOpen = true;
                break;
            case E_BoxType.Yellow:
                if (GameManager.Instance.hasYellowKey) canOpen = true;
                break;
            case E_BoxType.Default:
                canOpen = true;
                break;
        }
    }

    private IEnumerator IE_BoxDisappear()
    {
        BoxOpen();
        yield return new WaitForSeconds(fadeTime);

        BoxFade();
        yield return new WaitForSeconds(destroyTime);

        BoxDestroy();
    }

    private void BoxOpen()
    {
        isOpen = true;
        boxAnim.Play("Open");

        //TODO:掉出道具
        AudioManager.Instance.SoundPlay("box_open");
    }

    private void BoxFade()
    {
        boxAnim.Play("Fade");
    }

    private void BoxDestroy()
    {
        Destroy(this.gameObject);
    }
}



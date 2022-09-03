using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public Transform player;
    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;
    private Color color;

    [Header("时间控制参数")]
    public float activeTime;
    public float activeStart;

    [Header("不透明度控制")]
    public float alphaSet;
    public float alphaMultiplier;
    private float alpha;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.sprite = playerSprite.sprite;

        //当前残影的transform属性，为player的transform属性
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;

    }

    void FixedUpdate()
    {
        alpha *= alphaMultiplier;

        color = new Color(0.5f, 0.5f, 1, alpha);

        thisSprite.color = color;

        if (Time.time >= activeStart + activeTime)
        {
            PoolManager.Instance.ReturnDictObj(this.gameObject.name, this.gameObject);
        }
    }
}

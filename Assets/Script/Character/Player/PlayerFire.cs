using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("子弹设置")]
    public GameObject smallBullet;
    public GameObject middleBullet;
    public int middle_lossMagic;
    public int small_lossMagic;

    [Header("攻击设置")]
    [SerializeField] private Transform standFirePoint;
    [SerializeField] private Transform crouchFirePoint;
    public float fireCD = 0.25f;
    public float currentFireCD;
    public float storeTime;
    public float currentStoreTime;

    [Header("Reference")]
    private Animator maidAnim;
    private PlayerStatus playerStatus;

    private void Awake()
    {
        maidAnim = GetComponent<Animator>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    private void Start()
    {
        currentFireCD = fireCD;
    }


    public void StandFireSmallBullet()
    {
        //开启子弹对象池
        // PoolManager.Instance.GetDictObj("Prefab/bullet_small", (bullet) =>
        // {
        //     bullet.transform.position = standFirePoint.position;
        //     bullet.transform.rotation = standFirePoint.rotation;
        // });
        Instantiate(smallBullet, standFirePoint.position, standFirePoint.rotation);

        maidAnim.SetTrigger("Attack");
        AudioManager.Instance.SoundPlay("throw_2");
        playerStatus.LossMagic(small_lossMagic);
    }

    public void CrouchFireSmallBullet()
    {
        Instantiate(smallBullet, crouchFirePoint.position, crouchFirePoint.rotation);
        maidAnim.SetTrigger("Attack");
        AudioManager.Instance.SoundPlay("throw_2");
        playerStatus.LossMagic(small_lossMagic);
    }

    public void StandFireMiddleBullet()
    {
        Instantiate(middleBullet, standFirePoint.position, standFirePoint.rotation);
        maidAnim.SetTrigger("Attack");
        AudioManager.Instance.SoundPlay("throw_2");
        playerStatus.LossMagic(middle_lossMagic);
    }

    public void CrouchFireMiddleBullet()
    {
        Instantiate(middleBullet, crouchFirePoint.position, crouchFirePoint.rotation);
        maidAnim.SetTrigger("Attack");
        AudioManager.Instance.SoundPlay("throw_2");
        playerStatus.LossMagic(middle_lossMagic);
    }

    public void StandFireRay()
    {
        //TODO:发射激光
    }

    public void CrouchFireRay()
    {
        //TODO:发射激光
    }


}

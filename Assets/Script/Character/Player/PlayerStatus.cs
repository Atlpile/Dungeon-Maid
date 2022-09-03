using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public PlayerData maidData;

    [Header("角色参数")]
    public float invicibleTime = 1f;
    public float velocityX = 15f;
    public float velocityY = 15f;
    public float resumeMagicCD = 0.5f;

    [Header("Player状态")]
    public bool isDead;
    public bool isInvicible;
    public bool canHurt;

    private Animator maidAnim;
    private Rigidbody2D rb;

    private void Awake()
    {
        maidAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        canHurt = true;

        StartCoroutine(IE_ResumeMagic());
    }


    public void LoadPlayerData()
    {

    }

    public void PlayerHurt(int damage, Transform attacker)
    {
        StartCoroutine(IE_PlayerHurt(damage, attacker));
    }

    private IEnumerator IE_PlayerHurt(int damage, Transform attacker)
    {
        maidAnim.SetTrigger("Hurt");
        AudioManager.Instance.SoundPlay("player_hurt");
        TakePlayerDamage(damage);
        HurtForce(attacker);
        canHurt = false;

        yield return new WaitForSeconds(invicibleTime);
        canHurt = true;
    }

    private void HurtForce(Transform attacker)
    {
        if (attacker.transform.position.x < this.transform.position.x)
            //FIXME:X轴没有移动速度
            rb.velocity = new Vector2(transform.position.x + velocityX, transform.position.y + velocityY);
        else
            rb.velocity = new Vector2(-velocityX, velocityY);

    }

    private void TakePlayerDamage(int damage)
    {
        maidData.currentHP = Mathf.Max(maidData.currentHP - damage, 0);
    }

    public void LossMagic(int magic)
    {
        if (maidData.currentMP > 0)
        {
            if (maidData.currentMP - magic > 0)
                maidData.currentMP -= magic;
        }
    }

    private IEnumerator IE_ResumeMagic()
    {
        while (true)
        {
            yield return new WaitForSeconds(resumeMagicCD);

            if (maidData.currentMP < maidData.maxMP)
                maidData.currentMP++;
        }
    }

    public void PlayerDead()
    {

    }

}

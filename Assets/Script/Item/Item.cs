using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public E_ItemType itemType;
    public bool isGravity;
    public float fadeSpeed;

    private PlayerStatus maid;
    private Rigidbody2D rb;
    private Collider2D coll;
    private SpriteRenderer sp;

    public int CurrentHP
    {
        get { return maid.maidData.currentHP; }
        set { maid.maidData.currentHP = value; }
    }
    public int MaxHP
    {
        get { return maid.maidData.maxHP; }
        set { maid.maidData.maxHP = value; }
    }
    public int CurrentMP
    {
        get { return maid.maidData.currentMP; }
        set { maid.maidData.currentMP = value; }
    }
    public int MaxMP
    {
        get { return maid.maidData.maxMP; }
        set { maid.maidData.maxMP = value; }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (isGravity)
            rb.bodyType = RigidbodyType2D.Dynamic;
        else
            rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            maid = other.gameObject.GetComponent<PlayerStatus>();
            GetItem();
            Destroy(this.gameObject);
        }
    }

    private void GetItem()
    {
        switch (itemType)
        {
            case E_ItemType.Coin:
                maid.maidData.coinNum++;
                AudioManager.Instance.SoundPlay("coin_1");
                break;
            case E_ItemType.CoinPack:
                maid.maidData.coinNum += 10;
                AudioManager.Instance.SoundPlay("coin_2");
                break;
            case E_ItemType.Bottle_HP:
                AddCurrentHP();
                AudioManager.Instance.SoundPlay("item_drink");
                break;
            case E_ItemType.Bottle_MP:
                AddCurrentMP();
                AudioManager.Instance.SoundPlay("item_drink");
                break;
            case E_ItemType.Bottle_FULL:
                AddCurrentHP();
                AddCurrentMP();
                AudioManager.Instance.SoundPlay("item_drink");
                break;
            case E_ItemType.Scroll_ATK:
                maid.maidData.maxATK += 20;
                AudioManager.Instance.SoundPlay("item_scroll");
                break;
            case E_ItemType.Scroll_DEF:
                maid.maidData.maxDEF += 20;
                AudioManager.Instance.SoundPlay("item_scroll");
                break;
            case E_ItemType.Scroll_HP:
                maid.maidData.maxHP += 20;
                maid.maidData.currentHP += 20;
                AudioManager.Instance.SoundPlay("item_scroll");
                break;
            case E_ItemType.Scroll_MP:
                maid.maidData.maxMP += 20;
                maid.maidData.currentMP += 20;
                AudioManager.Instance.SoundPlay("item_scroll");
                break;
        }
    }

    private void AddCurrentHP()
    {
        if (CurrentHP < MaxHP)
        {
            if ((CurrentHP += 50) >= MaxHP)
                CurrentHP = MaxHP;
            else
                CurrentHP += 50;
        }
    }

    private void AddCurrentMP()
    {
        if (CurrentMP < MaxMP)
        {
            if ((CurrentMP += 50) >= MaxMP)
                CurrentMP = MaxMP;
            else
                CurrentMP += 50;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    private PlayerController maid;

    [Header("地面检测")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 5f;

    private void Awake()
    {
        maid = GetComponent<PlayerController>();
    }

    public void GroundCheck()
    {
        maid.isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

}

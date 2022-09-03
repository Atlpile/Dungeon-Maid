using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    public bool hasRedKey;
    public bool hasBlueKey;
    public bool hasYellowKey;
    public bool hasGreenKey;

    public int coinNum;

    protected override void Awake()
    {
        base.Awake();
    }



}

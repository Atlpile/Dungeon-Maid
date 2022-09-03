using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance { get => instance; }

    public PlayerData maidData;
    public GameData gameData;

    public GameDataManager()
    {
        maidData = PlayerPrefsDataManager.Instance.LoadData(typeof(PlayerData), "PlayerData") as PlayerData;
    }

    #region 提供给外部,用于改变数据的API

    public void LoadGame()
    {
        maidData = PlayerPrefsDataManager.Instance.LoadData(typeof(PlayerData), "PlayerData") as PlayerData;
    }

    public void ReduceHP(int HP)
    {
        maidData.currentHP -= HP;
    }

    #endregion
}

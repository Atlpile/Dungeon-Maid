
public enum E_ResourceType { UIPrefab, UISprite, Sprite, Prefab, Audio, Effect, TextAsset }

public enum E_SoundType { BGM, SoundEffect, Voice }

public enum E_BoxType { Red, Blue, Green, Yellow, Default }
public enum E_KeyType { Red, Blue, Green, Yellow }
public enum E_ItemType
{
    Coin, CoinPack,
    Bottle_HP, Bottle_MP, Bottle_FULL,
    Scroll_HP, Scroll_MP, Scroll_ATK, Scroll_DEF
}

public enum E_PlatformType
{
    MoveX, MoveY, Static
}

public enum E_EnemyState
{
    Null, PatrolState, ChaseState, GuardState, FlyState, AttackState
}
public enum E_EnemyType
{
    Ground, Fly, Static, Boss, Guard
}

namespace BattleSystem.Const
{
    public enum BattleBehaviorType
    {
        Level,
        Buff,
        Bullet,
        GroundEffect,
        Halo,
        BattleShopTower,
        Core,
        Hero,
        Monster,
        Tower
    }

    
    public enum BulletType
    {
        HorizontalEndpointConfirmation = 1,
        ParabolicEndpointConfirmation,
        LaserLockOnEnemy,
        MissileLockOnEnemy,
        GroundEffectOnPath,
        CircularPathNoExistence,
        FallSkyOnEnemy,
        TouchHitEnemy
    }

}
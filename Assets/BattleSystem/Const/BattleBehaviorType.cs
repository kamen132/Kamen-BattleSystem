namespace BattleSystem.Const
{
    public enum BattleBehaviorType
    {
        Buff,
        Bullet,
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
    
    public enum BattleUnitTargetType
    {
        Default,
        Tower,
        Core,
        Monster
    }


    public enum BattleParamType
    {
        Dmg,
        Radius,
        SpeedDec,
        EnemyNum,
        DmgInc,
        Chance,
        Hp,
    }
}
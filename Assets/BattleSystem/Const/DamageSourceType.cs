namespace BattleSystem.Const
{
    public enum DamageSourceType
    {
        None,
        Buff,
        Ally,
        GroundEffect
    }

    
    public enum SkillEffectType
    {
        Laser,
        CommonEffect,
        Explode,
        Column
    }

    public enum NumericVariableType
    {
        Hp = 102,
        HpPct = 103,
        Heal = 106,
        DmgBase = 111,
        Dmg = 112,
        DmgPct = 113,
        AtkSpd = 115,
        AtkSpdPct = 117,
        CritChance = 121,
        CritChancePct = 123,
        CritDmg = 125,
    }
    
    public enum NumericSourceType
    {
        Base,
        Ability,
        Rune,
        Talent,
        Difficulty,
        Buff,
        GiftCard,
        CoreAbility,
        SinceGrowth
    }

}
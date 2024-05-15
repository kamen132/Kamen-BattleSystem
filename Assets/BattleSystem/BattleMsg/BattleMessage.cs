using BattleSystem.Const;
using BattleSystem.Tools;
using KamenMessage.RunTime.Basic.Message;
using UnityEngine;

namespace BattleSystem.BattleMsg
{
    public class BattleEndDto : MessageModel
    {
        public bool IsVictory { get; set; }

        public bool NShowResult { get; set; }
    }
    
    public class BattleRoundStartDto : MessageModel
    {
    }
    
    public class ReadyBattleDto : MessageModel
    {
        public bool SkipSelectCore { get; set; }
    }
    
    public class BattleClearDto : MessageModel
    {
    }

    public class CreateEffectDto : MessageModel
    {
        public ResourceTag ResourceTag;

        public Vector3 Position;

        public bool NoClean { get; set; }
    }
    
    public class CreateSkillEffectDto : MessageModel
    {
        public SkillEffectType SkillEffectType { get; set; }
        public ResourceTag ResourceTag { get; set; }
        public Vector3 EffectPos { get; set; }
        public int Radius { get; set; }

        public Vector3 BornPos { get; set; }

        public Vector3 TargetPos { get; set; }

        public int Duration { get; set; }
    }
}
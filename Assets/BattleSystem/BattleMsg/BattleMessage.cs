using KamenMessage.RunTime.Basic.Message;

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

}
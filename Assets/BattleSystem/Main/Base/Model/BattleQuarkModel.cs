using KamenMessage.RunTime.Basic.Model;

namespace BattleSystem.Main.Base.Model
{
	public abstract class BattleQuarkModel : ManualModel
	{
		public string Guid { get; private set; }
		public BattleQuarkModel()
		{
			Guid = System.Guid.NewGuid().ToString("N");
		}
	}
}


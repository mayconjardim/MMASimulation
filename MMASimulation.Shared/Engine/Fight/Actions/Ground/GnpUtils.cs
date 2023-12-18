using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Ground
{
	public static class GnpUtils
	{

		public static double GetGnPBonusByGuard(FightAttributes fightAttributes)
		{
			double result = 0;

			switch (fightAttributes.GuardType)
			{
				case 0:
					result = 3 * fightAttributes.NumHooks;
					break;
				case 1:
					result = 5;
					break;
				case 2:
					result = 1;
					break;
				case 3:
					result = -1;
					break;
				case 4:
					result = 0;
					break;
				case 5:
					result = -3;
					break;
				case 6:
					result = -5;
					break;
			}

			return result;
		}


	}
}

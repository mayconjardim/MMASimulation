using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;

namespace MMASimulation.Shared.Engine.Fight.Actions.Ground
{
	public static class TakedownUtils
	{

		public static int TakedownType(Fighter act)
		{
			const double NO_SKILL_PROB = 0.25;
			double wrestlingProb = RandomUtils.GetRandom();
			double judoProb = RandomUtils.GetRandom();

			if (act.FighterStyles.WrestlingTD)
			{
				wrestlingProb *= NO_SKILL_PROB;
			}

			if (act.FighterStyles.JudoTD)
			{
				judoProb *= NO_SKILL_PROB;
			}

			if (wrestlingProb > judoProb)
			{
				return Sim.WRESTLING;
			}
			else
			{
				return Sim.JUDO;
			}
		}


	}
}

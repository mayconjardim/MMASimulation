using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Clinch
{
	public static class GetClinchActions
	{
		public static int ClinchAction(Fighter act, Fighter pas)
		{
			int prob = new Random().Next(1, 101);

			int dirtyBoxing = act.FighterStrategies.StratDirtyBoxing;
			int thai = dirtyBoxing + act.FighterStrategies.StratThaiClinch;
			int avoidProb = thai + act.FighterStrategies.StratAvoidClinch;

			if (prob <= dirtyBoxing)
			{
				return GetDirtyBoxingAction(act);
			}
			else if (prob <= thai)
			{
				return GetThaiAction(act);
			}
			else if (prob <= avoidProb)
			{
				return Sim.ACT_BREAKCLINCH;
			}
			else
			{
				return Sim.ACT_TAKEDOWNCLINCH;
			}
		}

		public static int GetDirtyBoxingAction(Fighter act)
		{
			const double PUNCH_PROB = 1.25;

			double kneeProb = act.FighterStrategies.StratKicking + RandomUtils.GetRandom();
			double punchProb = (act.FighterStrategies.StratPunching + RandomUtils.GetRandom()) * PUNCH_PROB;

			if (kneeProb > punchProb)
			{
				return Sim.ACT_GRAPPLING_KNEE;
			}
			else
			{
				return Sim.ACT_DIRTYBOXING;
			}
		}

		public static int GetThaiAction(Fighter act)
		{
			const double KNEE_PROB = 1.25;

			double kneeProb = (act.FighterStrategies.StratKicking + RandomUtils.GetRandom()) * KNEE_PROB;
			double punchProb = act.FighterStrategies.StratPunching + RandomUtils.GetRandom();

			if (kneeProb > punchProb)
			{
				return Sim.ACT_THAICLINCH_KNEES;
			}
			else
			{
				return Sim.ACT_THAICLINCH_PUNCHES;
			}
		}

		public static int GetClinchType(Fighter act)
		{
			const double NO_SKILL_PROB = 0.5;
			double thaiClinch = RandomUtils.GetRandom();
			double dirtyClinch = RandomUtils.GetRandom();
			double grapplingClinch = RandomUtils.GetRandom();

			if (!act.FighterStyles.ThaiClinch)
			{
				thaiClinch *= NO_SKILL_PROB;
			}

			if (!act.FighterStyles.DirtyBoxing)
			{
				dirtyClinch *= NO_SKILL_PROB;
			}

			if (grapplingClinch > thaiClinch && grapplingClinch > dirtyClinch)
			{
				return Sim.SIMPLE_GRAPPLING;
			}
			else if (dirtyClinch > thaiClinch && dirtyClinch > grapplingClinch)
			{
				return Sim.CLINCH_DIRTY_BOXING;
			}
			else if (thaiClinch > dirtyClinch && thaiClinch > grapplingClinch)
			{
				return Sim.THAI_CLINCH;
			}

			return Sim.SIMPLE_GRAPPLING;
		}


		public static int GetClinchPunchType(Fighter act)
		{
			const double NO_SKILL_PROB = 0.5;
			const double IN_CLINCH_TYPE_PROB = 1.5;

			double thaiProb = RandomUtils.GetRandom();
			double dirtyProb = RandomUtils.GetRandom();
			double grapplingProb = RandomUtils.GetRandom();

			if (!act.FighterStyles.ThaiClinch)
			{
				thaiProb *= NO_SKILL_PROB;
			}
			else if (act.FighterStyles.ClinchType == Sim.THAI_CLINCH)
			{
				thaiProb *= IN_CLINCH_TYPE_PROB;
			}

			if (!act.FighterStyles.DirtyBoxing)
			{
				dirtyProb *= NO_SKILL_PROB;
			}
			else if (act.FighterStyles.ClinchType == Sim.CLINCH_DIRTY_BOXING)
			{
				dirtyProb *= IN_CLINCH_TYPE_PROB;
			}

			int result = Sim.GRAPPLING_ATTACK;

			if (grapplingProb > thaiProb && grapplingProb > dirtyProb)
			{
				result = Sim.GRAPPLING_ATTACK;
			}
			else if (dirtyProb > thaiProb && dirtyProb > grapplingProb)
			{
				result = Sim.DIRTY_BOXING;
			}
			else if (thaiProb > dirtyProb && thaiProb > grapplingProb)
			{
				result = Sim.THAI_ATTACK;
			}

			return result;
		}



		public static void RefBreakClinch(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
		{

			if (fightAttributes.BoutFinished)
			{
				return;
			}

			//Quebra o clinche após movimentos falhos
			if (fightAttributes.InTheClinch && Sim.BREAKCLINCHFREQUENCY > act.FighterFightAttributes.Rush * 2)
			{
				if (RandomUtils.FixedRandomInt(Sim.BREAKCLINCHPROB) > 5)
				{
					Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("RefBreakClinch")), Pbp, fightAttributes);
					fightAttributes.InTheClinch = false;
				}
			}
		}



	}
}

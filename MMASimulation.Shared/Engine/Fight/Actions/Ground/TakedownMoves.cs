using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
using MMASimulation.Shared.Engine.Fight.Actions.Counter;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Enums;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Ground
{
	public static class TakedownMoves
	{

		public static void ActTakedown(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
		{

			int attackLevel = DuringFighterUtils.GetAttackLevel(act, pas, act.FighterRatings.Takedowns, pas.FighterRatings.TakedownsDef, fightAttributes);

			switch (attackLevel)
			{
				case 1:
				case 2:
				case 3:
					Comment.GetComment(ReadTxts.ReadFileToList("TakeDown1"), fightAttributes);
					break;
			}

			Comment.DoComment(act, pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);

			fightAttributes.HitLocation = Comment.ExtractHitLocation(fightAttributes.FullComment);

			StatsUtils.UpdateStatistic(fightAttributes.Statistics, GetFighterActions.GetFighterNumber(act, fightAttributes), StatisticsTypes.stTakedowns, 0, Comment.ExtractHitsLanded(fightAttributes.FullComment));

			double at = RandomUtils.FixedRandomInt(act.FighterRatings.Takedowns) + act.FighterRatings.AttackBonus(act.FighterFightAttributes);
			at += RandomUtils.GeSmallRandom();
			at = DuringFighterUtils.GetGasTankFactor(act, at);
			at -= DuringFighterUtils.GetHurtFactor(act);

			double def = RandomUtils.FixedRandomInt(pas.FighterRatings.TakedownsDef) + pas.FighterRatings.DefenseBonus(pas.FighterFightAttributes);
			def += RandomUtils.GeSmallRandom();
			def = DuringFighterUtils.GetGasTankFactor(pas, def);
			def -= DuringFighterUtils.GetHurtFactor(pas);

			if (def >= at)
			{
				Comment.DoComment(act, pas, Comment.ExtractFailureComment(fightAttributes.FullComment), Pbp, fightAttributes);

				if (!fightAttributes.IsCounter)
				{
					fightAttributes.IsCounter = CheckActions.CheckCounterAttack(act, pas, fightAttributes.CounterProb, fightAttributes);
					if (fightAttributes.IsCounter)
					{
						CounterActions.DoCounterAttack(pas, act, Pbp, fightAttributes);
					}
					else
					{
						PositionUtils.ProcessAfterMovePosition(act, pas, Comment.ExtractFinalFailurePosition(fightAttributes.FullComment), fightAttributes);
					}
				}
				else
				{
					fightAttributes.IsCounter = false;
					PositionUtils.ProcessAfterMovePosition(act, pas, Comment.ExtractFinalFailurePosition(fightAttributes.FullComment), fightAttributes);

				}
			}
			else
			{
				Comment.DoComment(act, pas, Comment.ExtractComment(fightAttributes.FullComment), Pbp, fightAttributes);

				act.FighterFightAttributes.RoundsInTheGround = Sim.MINSROUNDSINTHEGROUND;

				double damageDone = ((at - def) * act.FighterRatings.DamageBonus(act.FighterFightAttributes) * attackLevel) / 4;
				DuringFighterUtils.DamageFighter(act, pas, damageDone, fightAttributes, fightAttributes.Statistics);

				act.FighterFightAttributes.IncreasePoints(fightAttributes.CurrentRound, Sim.SUCCESSFULTAKEDOWNPOINTS);

				PositionUtils.ProcessAfterMovePosition(act, pas, Comment.ExtractFinalSuccessPosition(fightAttributes.FullComment), fightAttributes);

				int injuryType = CheckActions.CheckInjury(act, pas, damageDone, fightAttributes.InjuryProb, fightAttributes);
				if (injuryType != Sim.INJURYORCUTFALSE)
				{
					DuringFighterUtils.ProcessInjury(act, pas, injuryType, Pbp, fightAttributes);
				}

				injuryType = CheckActions.CheckCut(act, pas, damageDone, fightAttributes.CutProb, fightAttributes);
				if (injuryType != Sim.INJURYORCUTFALSE)
				{
					DuringFighterUtils.ProcessCut(act, pas, injuryType, Pbp, fightAttributes);
				}

				if (CheckActions.CheckKO(act, pas, damageDone, fightAttributes.KOSubProb, fightAttributes))
				{
					DuringFighterUtils.ProcessKO(act, pas, Pbp, fightAttributes);
				}

				StatsUtils.UpdateStatistic(fightAttributes.Statistics, GetFighterActions.GetFighterNumber(act, fightAttributes), StatisticsTypes.stTakedowns, 0, Comment.ExtractHitsLanded(fightAttributes.FullComment));
			}
		}


	}
}

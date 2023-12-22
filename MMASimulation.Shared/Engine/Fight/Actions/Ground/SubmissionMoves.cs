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
	public static class SubmissionMoves
	{
		public static void ActSubmission(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
		{
			double at, def, damageDone;


			if (fightAttributes.FighterOnTop == GetFighterActions.GetFighterNumber(act, fightAttributes))
			{
				switch (fightAttributes.GuardType)
				{
					case 0:
						Comment.GetComment(ReadTxts.ReadFileToList("RearSub1"), fightAttributes);
						break;
					case 1:
						Comment.GetComment(ReadTxts.ReadFileToList("FullMountSub1"), fightAttributes);
						break;
					case 2:
					case 3:
						Comment.GetComment(ReadTxts.ReadFileToList("SideMountSub1"), fightAttributes);
						break;
					case 4:
						Comment.GetComment(ReadTxts.ReadFileToList("OpenGuardSub1"), fightAttributes);
						break;
					default:
						GroundMoves.ActPositioning(act, pas, Pbp, fightAttributes);
						return;
				}
			}
			else
			{
				switch (fightAttributes.GuardType)
				{
					case -1:
						Comment.GetComment(ReadTxts.ReadFileToList("StandUpSub1"), fightAttributes);
						break;
					case 3:
					case 4:
					case 5:
					case 6:
						Comment.GetComment(ReadTxts.ReadFileToList("ClosedGuardSub1"), fightAttributes);
						break;
					default:
						GroundMoves.ActPositioning(act, pas, Pbp, fightAttributes);
						return;
				}
			}

			fightAttributes.SubLocation = Comment.ExtractHitLocation(fightAttributes.FullComment);

			Comment.DoComment(act, pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);

			fightAttributes.HitLocation = Comment.ExtractHitLocation(fightAttributes.FullComment);

			StatsUtils.UpdateStatistic(fightAttributes.Statistics, GetFighterActions.GetFighterNumber(act, fightAttributes), StatisticsTypes.stSubmission, 1, 0);

			at = RandomUtils.FixedRandomInt(act.FighterRatings.Submission) + act.FighterRatings.AttackBonus(act.FighterFightAttributes) + GetSubBonusByGuard();
			switch (new Random().Next(4))
			{
				case 0:
					at += RandomUtils.FixedRandomInt(act.FighterRatings.Strength / 2);
					break;
				case 1:
					at += RandomUtils.FixedRandomInt(act.FighterRatings.Agility / 2);
					break;
				case 2:
					at += RandomUtils.FixedRandomInt(act.FighterRatings.GroundGame / 2);
					break;
				case 3:
					at += RandomUtils.FixedRandomInt(act.FighterRatings.SubDefense / 2);
					break;
			}
			at += RandomUtils.GeSmallRandom();
			at = DuringFighterUtils.GetGasTankFactor(act, at);
			at -= DuringFighterUtils.GetHurtFactor(act) + Sim.SUBMALUS;

			def = RandomUtils.FixedRandomInt(pas.FighterRatings.SubDefense) + act.FighterRatings.DefenseBonus(act.FighterFightAttributes);
			switch (new Random().Next(4))
			{
				case 0:
					def += RandomUtils.FixedRandomInt(pas.FighterRatings.Strength / 2);
					break;
				case 1:
					def += RandomUtils.FixedRandomInt(pas.FighterRatings.Agility / 2);
					break;
				case 2:
					def += RandomUtils.FixedRandomInt(pas.FighterRatings.Submission / 2);
					break;
				case 3:
					def += RandomUtils.FixedRandomInt(pas.FighterRatings.GroundGame / 2);
					break;
			}
			def += RandomUtils.GeSmallRandom();
			def = DuringFighterUtils.GetGasTankFactor(pas, def);
			def -= DuringFighterUtils.GetHurtFactor(pas);

			if (def >= at)
			{
				Comment.DoComment(act, pas, Comment.ExtractFailureComment(fightAttributes.FullComment), Pbp, fightAttributes);

				if (act.FighterFightAttributes.OnTheGround)
				{
					act.FighterStyles.Stalling += 1;
				}

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
				damageDone = (at - def) * act.FighterRatings.DamageBonus(act.FighterFightAttributes);
				DuringFighterUtils.DamageFighter(act, pas, damageDone, fightAttributes, fightAttributes.Statistics);

				if (act.FighterFightAttributes.OnTheGround)
				{
					act.FighterStyles.Stalling = 0;
				}

				act.FighterFightAttributes.IncreasePoints(fightAttributes.CurrentRound, Sim.LOCKINSUBMISSIONPOINTS);

				fightAttributes.HitLocation = Comment.ExtractHitLocation(fightAttributes.FullComment);

				if (CheckSubmission(act, pas, damageDone, ExtractKOSubProb(FullComment)))
				{
					Comment.DoComment(act, pas, Comment.ExtractComment(fightAttributes.FullComment), Pbp, fightAttributes);
					fightAttributes.BoutFinished = true;
					fightAttributes.FinishedType = ReadTxts.ReadListToComment("Misc", Sim.SUB);
					fightAttributes.FinishMode = Sim.RES_SUB;
					fightAttributes.FinishedDescription = Comment.ExtractMoveName(fightAttributes.FullComment);
					fightAttributes.FighterWinner = GetFighterActions.GetFighterNumber(act, fightAttributes);
				}
				else
				{
					ActLockSubmission(act, pas);
				}

				PositionUtils.ProcessAfterMovePosition(act, pas, Comment.ExtractFinalFailurePosition(fightAttributes.FullComment), fightAttributes);


				StatsUtils.UpdateStatistic(fightAttributes.Statistics, GetFighterActions.GetFighterNumber(act, fightAttributes), StatisticsTypes.stSubmission, 0, 1);
			}
		}

	}
}

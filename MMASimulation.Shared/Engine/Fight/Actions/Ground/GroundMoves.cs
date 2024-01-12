using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
using MMASimulation.Shared.Engine.Fight.Actions.Counter;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Ground
{
	public static class GroundMoves
	{

		public static void ActPositioning(Fighter Act, Fighter Pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
		{
			double At, Def;

			int attackLevel = DuringFighterUtils.GetAttackLevel(Act, Pas, Act.FighterRatings.Punching, Pas.FighterRatings.Dodging, fightAttributes);

			switch (fightAttributes.GuardType)
			{
				case 0:
					if (fightAttributes.FighterOnTop == GetFighterActions.GetFighterNumber(Act, fightAttributes))
					{
						Comment.GetComment(ReadTxts.ReadFileToList("InRearMountMoves"), fightAttributes);

					}
					else
					{
						Comment.GetComment(ReadTxts.ReadFileToList("DefInRearMountMoves"), fightAttributes);

					}
					break;
				case 1:
					if (fightAttributes.FighterOnTop == GetFighterActions.GetFighterNumber(Act, fightAttributes))
					{
						Comment.GetComment(ReadTxts.ReadFileToList("InFullMountMoves"), fightAttributes);
					}
					else
					{
						Comment.GetComment(ReadTxts.ReadFileToList("DefInFullMountMoves"), fightAttributes);
					}
					break;
				case 2:
					if (fightAttributes.FighterOnTop == GetFighterActions.GetFighterNumber(Act, fightAttributes))
					{
						Comment.GetComment(ReadTxts.ReadFileToList("InSideMountMoves"), fightAttributes);
					}
					else
					{
						Comment.GetComment(ReadTxts.ReadFileToList("DefInSideMountMoves"), fightAttributes);
					}
					break;
				case 3:
					if (fightAttributes.FighterOnTop == GetFighterActions.GetFighterNumber(Act, fightAttributes))
					{
						Comment.GetComment(ReadTxts.ReadFileToList("InHalfGuardMoves"), fightAttributes);
					}
					else
					{
						Comment.GetComment(ReadTxts.ReadFileToList("DefInHalfGuardMoves"), fightAttributes);
					}
					break;
				case 4:
					if (fightAttributes.FighterOnTop == GetFighterActions.GetFighterNumber(Act, fightAttributes))
					{
						Comment.GetComment(ReadTxts.ReadFileToList("InOpenGuardMoves"), fightAttributes);
					}
					else
					{
						Comment.GetComment(ReadTxts.ReadFileToList("DefInOpenGuardMoves"), fightAttributes);
					}
					break;
				case 5:
					if (fightAttributes.FighterOnTop == GetFighterActions.GetFighterNumber(Act, fightAttributes))
					{
						Comment.GetComment(ReadTxts.ReadFileToList("InClosedGuardMoves"), fightAttributes);
					}
					else
					{
						Comment.GetComment(ReadTxts.ReadFileToList("DefInClosedGuardMoves"), fightAttributes);
					}
					break;
				case 6:
					if (fightAttributes.FighterOnTop == GetFighterActions.GetFighterNumber(Act, fightAttributes))
					{
						Comment.GetComment(ReadTxts.ReadFileToList("InButterflyGuardMoves"), fightAttributes);
					}
					else
					{
						Comment.GetComment(ReadTxts.ReadFileToList("DefInButterflyGuardMoves"), fightAttributes);
					}
					break;
			}

			Comment.DoComment(Act, Pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);


			At = RandomUtils.FixedRandomInt(Act.FighterRatings.Punching) + Act.FighterRatings.AttackBonus(Act.FighterFightAttributes);
			int randomIndexAt = new Random().Next(4);

			switch (randomIndexAt)
			{
				case 0:
					At += RandomUtils.FixedRandomInt(Act.FighterRatings.Strength / 2);
					break;
				case 1:
					At += RandomUtils.FixedRandomInt(Act.FighterRatings.Agility / 2);
					break;
				case 2:
					At += RandomUtils.FixedRandomInt(Act.FighterRatings.Dodging / 2);
					break;
				case 3:
					At += RandomUtils.FixedRandomInt(Act.FighterRatings.ClinchGrappling / 2);
					break;
			}

			At += RandomUtils.GeSmallRandom();
			At = DuringFighterUtils.GetGasTankFactor(Act, At);
			At -= DuringFighterUtils.GetHurtFactor(Act);

			// Defensive value
			Def = RandomUtils.FixedRandomInt(Pas.FighterRatings.Dodging) + Pas.FighterRatings.DefenseBonus(Pas.FighterFightAttributes);
			int randomIndexDef = new Random().Next(4);

			switch (randomIndexDef)
			{
				case 0:
					Def += RandomUtils.FixedRandomInt(Pas.FighterRatings.Strength / 2);
					break;
				case 1:
					Def += RandomUtils.FixedRandomInt(Pas.FighterRatings.Agility / 2);
					break;
				case 2:
					Def += RandomUtils.FixedRandomInt(Pas.FighterRatings.Dodging / 2);
					break;
				case 3:
					Def += RandomUtils.FixedRandomInt(Pas.FighterRatings.ClinchGrappling / 2);
					break;
			}

			Def += RandomUtils.GeSmallRandom();
			Def = DuringFighterUtils.GetGasTankFactor(Pas, Def);
			Def -= DuringFighterUtils.GetHurtFactor(Pas);

			if (Def >= At)
			{
				Comment.DoComment(Act, Pas, Comment.ExtractFailureComment(fightAttributes.FullComment), Pbp, fightAttributes);
				Act.FighterStyles.Stalling += 1;

				if (!fightAttributes.IsCounter)
				{
					fightAttributes.IsCounter = CheckActions.CheckCounterAttack(Act, Pas, fightAttributes.CounterProb, fightAttributes);

					if (fightAttributes.IsCounter)
					{
						CounterActions.DoCounterAttack(Pas, Act, Pbp, fightAttributes);

					}
					else
					{
						PositionUtils.ProcessAfterMovePosition(Act, Pas, Comment.ExtractFinalFailurePosition(fightAttributes.FullComment), fightAttributes);

					}
				}
				else
				{
					fightAttributes.IsCounter = false;
					PositionUtils.ProcessAfterMovePosition(Act, Pas, Comment.ExtractFinalFailurePosition(fightAttributes.FullComment), fightAttributes);
				}
			}
			else
			{
				Comment.DoComment(Act, Pas, Comment.ExtractComment(fightAttributes.FullComment), Pbp, fightAttributes);

				Act.FighterFightAttributes.IncreasePoints(fightAttributes.CurrentRound, Sim.MOVEPOINTS);

				PositionUtils.ProcessAfterMovePosition(Act, Pas, Comment.ExtractFinalSuccessPosition(fightAttributes.FullComment), fightAttributes);

			}
		}

		public static void ActLnP(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
		{

			Comment.GetComment(ReadTxts.ReadFileToList("LnP"), fightAttributes);

			double at = RandomUtils.FixedRandomInt(act.FighterRatings.GroundGame) + act.FighterRatings.AttackBonus(act.FighterFightAttributes);
			at += RandomUtils.FixedRandomInt(act.FighterRatings.Control / 2);

			int randomIndexDef = new Random().Next(4);
			switch (randomIndexDef)
			{
				case 0: at += RandomUtils.FixedRandomInt(act.FighterRatings.Strength / 2); break;
				case 1: at += RandomUtils.FixedRandomInt(act.FighterRatings.Agility / 2); break;
				case 2: at += RandomUtils.FixedRandomInt(act.FighterRatings.SubDefense / 2); break;
				case 3: at += RandomUtils.FixedRandomInt(act.FighterRatings.Submission / 2); break;
			}

			at += RandomUtils.GeSmallRandom();
			at = DuringFighterUtils.GetGasTankFactor(act, at);
			at -= DuringFighterUtils.GetHurtFactor(act);

			double def = RandomUtils.FixedRandomInt(pas.FighterRatings.GroundGame) + pas.FighterRatings.DefenseBonus(pas.FighterFightAttributes);
			switch (randomIndexDef)
			{
				case 0: def += RandomUtils.FixedRandomInt(pas.FighterRatings.Strength / 2); break;
				case 1: def += RandomUtils.FixedRandomInt(pas.FighterRatings.SubDefense / 2); break;
				case 2: def += RandomUtils.FixedRandomInt(pas.FighterRatings.Aggressiveness / 2); break;
				case 3: def += RandomUtils.FixedRandomInt(pas.FighterRatings.Submission / 2); break;
			}
			def += RandomUtils.GeSmallRandom();
			def = DuringFighterUtils.GetGasTankFactor(pas, def);
			def -= DuringFighterUtils.GetHurtFactor(pas);

			if (def >= at)
			{
				Comment.DoComment(act, pas, Comment.ExtractFailureComment(fightAttributes.FullComment), Pbp, fightAttributes);

				if (!fightAttributes.IsCounter)
				{
					fightAttributes.IsCounter = CheckCounterAttack(act, pas, CounterProb);
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

				act.FighterStyles.Stalling += 2;

				act.FighterFightAttributes.RecoverStamina(2);

				PositionUtils.ProcessAfterMovePosition(act, pas, Comment.ExtractFinalSuccessPosition(fightAttributes.FullComment), fightAttributes);
			}
		}


	}
}

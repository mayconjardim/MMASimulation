using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
using MMASimulation.Shared.Engine.Fight.Actions.Clinch;
using MMASimulation.Shared.Engine.Fight.Actions.Stand;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Counter
{
	public static class CounterActions
	{
		public static void DoCounterAttack(Fighter Act, Fighter Pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
		{
			int Counter1, Counter2, CounterProb1, CounterProb2, FinalMove, ClinchMove;

			Counter1 = Comment.ExtractCounterMove1(fightAttributes.FullComment);
			Counter2 = Comment.ExtractCounterMove2(fightAttributes.FullComment);
			CounterProb1 = GetMoveProb.CounterMoveProb(Act, Counter1);
			CounterProb2 = GetMoveProb.CounterMoveProb(Act, Counter2);

			FinalMove = (CounterProb1 > CounterProb2) ? Counter1 : Counter2;

			Comment.DoComment(Act, Pas, Comment.ReturnComment(ReadTxts.ReadFileToList("Counter")), Pbp, fightAttributes);

			switch (FinalMove)
			{
				case 1:
					PunchActions.ActPunch(Act, Pas, Pbp, fightAttributes);
					break;
				case 2:
					KickActions.ActKick(Act, Pas, Pbp, fightAttributes);
					break;
				case 3:
					if (fightAttributes.InTheClinch)
					{
						ClinchMove = GetClinchActions.ClinchAction(Act, Pas);

						switch (ClinchMove)
						{
							case Sim.ACT_DIRTYBOXING:
								ClinchMoves.ActPunchClinch(Act, Pas, Sim.DIRTY_BOXING, Pbp, fightAttributes);
								break;
							case Sim.ACT_THAICLINCH_KNEES:
								ClinchMoves.ActKickClinch(Act, Pas, Sim.THAI_ATTACK, Pbp, fightAttributes);
								break;
							case Sim.ACT_THAICLINCH_PUNCHES:
								ClinchMoves.ActPunchClinch(Act, Pas, Sim.THAI_ATTACK, Pbp, fightAttributes);
								break;
							case Sim.ACT_TAKEDOWNCLINCH:
								ClinchMoves.ActClinchTakedown(Act, Pas, Pbp, fightAttributes);
								break;
							case Sim.ACT_BREAKCLINCH:
								ClinchMoves.ActBreakClinch(Act, Pas, Pbp, fightAttributes);
								break;
							default:
								ClinchMoves.ActPunchClinch(Act, Pas, -1);
								break;
						}
					}
					else
					{
						ActClinch(Act, Pas);
					}
					break;
				case 4:
					ActTakedown(Act, Pas);
					break;
				case 5:
					ActSubmission(Act, Pas);
					break;
				case 6:
					ActSubmission(Act, Pas);
					break;
				case 7:
					ActSubmission(Act, Pas);
					break;
				case 8:
					if (fightAttributes.FighterOnTop == GetFighterNumber(Act))
					{
						ActGnP(Act, Pas);
					}
					else
					{
						ActStrikesFromGuard(Act, Pas);
					}
					break;
				case 9:
					ActSubmission(Act, Pas);
					break;
				case 10:
					ActPositioning(Act, Pas);
					break;
				case 11:
					ActStandKickToGround(Act, Pas);
					break;
				case 12:
					ActGroundKicksToStand(Act, Pas);
					break;
				case 13:
					ActStrikesFromGuard(Act, Pas);
					break;
				case 14:
					ActMoveToGround(Act, Pas);
					break;
				case 15:
					ActStandUp(Act, Pas);
					break;
			}
		}

	}
}

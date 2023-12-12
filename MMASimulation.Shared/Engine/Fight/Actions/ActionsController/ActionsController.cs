using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.actions.actionsController;
using MMASimulation.Shared.Engine.Fight.Actions.Clinch;
using MMASimulation.Shared.Engine.Fight.Actions.Stand;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.ActionsController
{
	public static class ActionsController
	{

		public static void ActionsControllers(Fighter fighter1, Fighter fighter2, FightAttributes fightAttributes, int timeInc, List<FightPBP> Pbp)
		{
			List<Fighter> fighters = [fighter1, fighter2];

			int fighterActive, fighterPassive;
			int fighterAction1, fighterAction2, fighterAction;
			bool tempInTheClinch;
			bool fighter1OnTheGround, fighter2OnTheGround;
			bool f1Ground, f2Ground;

			// Aumenta o tempo no chão  para os lutadores
			if (fighters[0].FighterFightAttributes.OnTheGround)
			{
				StatsUtils.UpdateTimeOnGround(fightAttributes.Statistics, 0, timeInc);
			}
			if (fighters[1].FighterFightAttributes.OnTheGround)
			{
				StatsUtils.UpdateTimeOnGround(fightAttributes.Statistics, 1, timeInc);
			}

			tempInTheClinch = fightAttributes.InTheClinch;
			fighter1OnTheGround = fighters[0].FighterFightAttributes.OnTheGround;
			fighter2OnTheGround = fighters[1].FighterFightAttributes.OnTheGround;

			fighterAction1 = GetFighterActions.FighterAction(fighters[0], fighters[1], fightAttributes);
			fighterAction2 = GetFighterActions.FighterAction(fighters[1], fighters[0], fightAttributes);

			//Verifica  se ambos estão na mesma condição de atordoamento
			if (fighters[0].FighterFightAttributes.Dazed == fighters[1].FighterFightAttributes.Dazed)
			{
				if (!fighters[0].FighterFightAttributes.OnTheGround && !fighters[1].FighterFightAttributes.OnTheGround)
				{
					fighterActive = GetFightersInitiatives.StandUpInitiative(fighters[0], fighters[1]);
				}
				else
				{
					fighterActive = GetFightersInitiatives.GroundInitiative(fighters[0], fighters[1]);
				}
			}
			//Caso contrário, o lutador atordoado concede a iniciativa
			else
			{
				fighterActive = fighters[0].FighterFightAttributes.Dazed ? 1 : 0;
			}

			fighterPassive = (fighterActive == 1) ? 0 : 1;
			fighterAction = (fighterActive == 1) ? fighterAction2 : fighterAction1;


			SpecificComments.WriteGuard(fighters[fighterActive], fighters[fighterPassive], fightAttributes, Pbp);
			SpecificComments.MakeColorComments(fighters[fighterActive], fighters[fighterPassive], fightAttributes, Pbp);

			if (GetStandActions.CheckPunchesExchange(fighters[fighterActive], fighters[fighterPassive], fightAttributes))
			{
				fighterAction = Moves.ACT_PUNCHEXCHANGE;
			}

			f1Ground = fighters[fighterActive].FighterFightAttributes.OnTheGround;
			f2Ground = fighters[fighterPassive].FighterFightAttributes.OnTheGround;

			//O lutador ativo realiza a ação
			int fighterActions = GetFighterActions.FighterAction(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive),
				DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive), fightAttributes);

			switch (fighterActions)
			{
				case Moves.ACT_PUNCHES:
					PunchActions.ActPunch(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive), Pbp, fightAttributes);
					break;
				case Moves.ACT_KICKS:
					KickActions.ActKick(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive), Pbp, fightAttributes);
					break;
				case Moves.ACT_CLINCH:
					ClinchMoves.ActClinch(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive), Pbp, fightAttributes);
					break;
				case Moves.ACT_TAKEDOWNS:
					actTakedown(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_DIRTYBOXING:
					ClinchMoves.ActPunchClinch(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive), Sim.DIRTY_BOXING, Pbp, fightAttributes);
					break;
				case Moves.ACT_TAKEDOWNCLINCH:
					ClinchMoves.ActClinchTakedown(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive), Pbp, fightAttributes);
					break;
				case Moves.ACT_BREAKCLINCH:
					ClinchMoves.ActBreakClinch(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive), Pbp, fightAttributes);
					break;
				case Moves.ACT_GNP:
					actGnP(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_POSITIONING:
					actPositioning(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_SUBMISSION:
					actSubmission(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_STANDUP:
					actStandUp(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_LNP:
					actLnP(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_FANCYPUNCH:
					actFancyPunch(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_FANCYKICK:
					actFancyKick(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_HEADBUTT:
					actHeadButt(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_POKE:
					actPoke(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_REST:
					actRest(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_GROINKICK:
					actGroinKick(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_SLAM:
					actSlam(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_SUPPLEX:
					actSupplex(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_SOCCERKICKS:
					actSoccerKicks(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_STOMPS:
					actStomps(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_STANDKICK:
					actStandKickToGround(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_MOVETOGROUND:
					actMoveToGround(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_STRIKESFROMGUARD:
					actStrikesFromGuard(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_GROUNDKICK:
					actGroundKicksToStand(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_RESTCLINCH:
					actRestInClinch(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_NOACTION:
					actNoAction(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_ALLOWSTAND:
					actAllowToStand(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_PUNCHEXCHANGE:
					actPunchesExchange(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_PULLGUARD:
					actPullGuard(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_GNPELBOWS:
					actGnPElbows(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_CAPITALIZESTAND:
					actCapitalizeStanding(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_CAPITALIZEGROUND:
					actCapitalizeGround(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_KNEESONGROUND:
					actKneesOnGround(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_FANCYSUB:
					actFancySubmission(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive));
					break;
				case Moves.ACT_THAICLINCH_PUNCHES:
					ClinchMoves.ActPunchClinch(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive),
						Sim.THAI_ATTACK, Pbp, fightAttributes);
					break;
				case Moves.ACT_THAICLINCH_KNEES:
					ClinchMoves.ActKickClinch(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive),
						Sim.THAI_ATTACK, Pbp, fightAttributes);
					break;
				case Moves.ACT_GRAPPLING_PUNCH:
					ClinchMoves.ActPunchClinch(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive),
							 Sim.GRAPPLING_ATTACK, Pbp, fightAttributes);
					break;
				case Moves.ACT_GRAPPLING_KNEE:
					ClinchMoves.ActKickClinch(DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterActive), DuringFighterUtils.FighterActiveOrPassive(fighter1, fighter2, fighterPassive),
							Sim.GRAPPLING_ATTACK, Pbp, fightAttributes);
					break;
			}

			//Métodos extras
			if (!fightAttributes.BoutFinished)
			{
				DuringFighterUtils.RefStandFighters(fighters[fighterActive], fighters[fighterPassive], Pbp, fightAttributes);
				ClinchMoves.ActKeepClinch(fighters[fighterActive], fighters[fighterPassive], Pbp, fightAttributes);
				MakeStaggeredComment(fighters[fighterActive], fighters[fighterPassive], Pbp, fightAttributes);
				MakeStandUpComment(fighters[fighterActive], fighters[fighterPassive], !f1Ground, !f2Ground, Pbp, fightAttributes);
			}

			//Cansaço dos lutadores
			ProcessStaminaLoss(fighters[fighterActive], true);
			ProcessStaminaLoss(fighters[fighterPassive], false);


			//Recupera do estado atordoado
			RecoverForDazed(fighters[fighterActive]);
			RecoverForDazed(fighters[fighterPassive]);

			//Atualização de status
			if (tempInTheClinch && !fightAttributes.InTheClinch)
			{
				fightAttributes.Statistics[0].TempDamageClinch = 0;
				fightAttributes.Statistics[1].TempDamageClinch = 0;
			}

			if (fighter1OnTheGround && !fighters[0].FighterFightAttributes.OnTheGround)
			{
				fightAttributes.Statistics[0].TempDamageGround = 0;
			}

			if (fighter2OnTheGround && !fighters[1].FighterFightAttributes.OnTheGround)
			{
				fightAttributes.Statistics[1].TempDamageGround = 0;
			}

			if (!fighters[fighterActive].FighterFightAttributes.OnTheGround)
			{
				fighters[fighterActive].FighterStyles.Stalling = 0;
				fighters[fighterActive].FighterFightAttributes.RoundsInTheGround = 0;
			}
			else
			{
				fighters[fighterActive].FighterFightAttributes.RoundsInTheGround += 1;
			}

			if (!fighters[fighterPassive].FighterFightAttributes.OnTheGround)
			{
				fighters[fighterPassive].FighterStyles.Stalling = 0;
				fighters[fighterPassive].FighterFightAttributes.RoundsInTheGround = 0;
			}
			else
			{
				fighters[fighterPassive].FighterFightAttributes.RoundsInTheGround += 1;
			}

			CheckFightPerformance(fighters[fighterActive], fighters[fighterPassive]);
			CheckFightPerformance(fighters[fighterPassive], fighters[fighterActive]);
			CheckPainAndTiredness(fighters[fighterActive]);
			CheckPainAndTiredness(fighters[fighterPassive]);
			CheckMoral(fighters[fighterActive]);
			CheckMoral(fighters[fighterPassive]);

			if (!fightAttributes.BoutFinished)
			{
				ProcessTowelThrow(fighters[fighterPassive], fighters[fighterActive]);
				RefRestartCentreRing(fighters[fighterPassive], fighters[fighterActive]);
			}

			UpdateHPandStamina();
			UpdatePerformance();
			UpdateMoral();
		}

	}
}

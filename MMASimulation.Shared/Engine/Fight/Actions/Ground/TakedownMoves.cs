using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
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

			double at = RandomUtils.FixedRandomInt(act.GetTakedown()) + act.GetAttackBonus();
			at += RandomUtils.GetRandomValue(act, 4);
			at = GetGasTankFactor(act, at);
			at -= GetHurtFactor(act);

			double def = RandomUtils.FixedRandomIn(pas.GetTakedownDef()) + pas.GetDefenseBonus();
			def += GetRandomValue(pas, 4);
			def = GetGasTankFactor(pas, def);
			def -= GetHurtFactor(pas);

			if (def >= at)
			{
				DoComment(act, pas, ExtractFailureComment(FullComment));

				if (!IsCounter)
				{
					IsCounter = CheckCounterAttack(act, pas, CounterProb);
					if (IsCounter)
					{
						DoCounterAttack(pas, act);
					}
					else
					{
						ProcessAfterMovePosition(act, pas, ExtractFinalFailurePosition(FullComment));
					}
				}
				else
				{
					IsCounter = false;
					ProcessAfterMovePosition(act, pas, ExtractFinalFailurePosition(FullComment));
				}
			}
			else
			{
				DoComment(act, pas, ExtractComment(FullComment));
				act.RoundsInTheGround = ApplicationUtils.MINSROUNDSINTHEGROUND;

				double damageDone = ((at - def) * act.GetDamageBonus() * attackLevel) / 4;
				DamageFighter(act, pas, damageDone);

				act.IncreasePoints(Bout.CurrentRound, ApplicationUtils.SUCCESSFULTAKEDOWNPOINTS);

				ProcessAfterMovePosition(act, pas, ExtractFinalSuccessPosition(FullComment));

				int injuryType = CheckInjury(act, pas, damageDone, InjuryProb);
				if (injuryType != ApplicationUtils.INJURYORCUTFALSE)
				{
					ProcessInjury(act, pas, injuryType);
				}

				injuryType = CheckCut(act, pas, damageDone, CutProb);
				if (injuryType != ApplicationUtils.INJURYORCUTFALSE)
				{
					ProcessCut(act, pas, injuryType);
				}

				if (CheckKO(act, pas, damageDone, KOSubProb))
				{
					ProcessKO(act, pas);
				}

				Bout.UpdateStatistic(GetFighterNumber(act), StatisticType.Takedowns, 0, ExtractHitsLanded(FullComment));
			}
		}


	}
}

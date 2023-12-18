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
	public static class GnpMoves
	{

		public static void ActGnP(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
		{

			int attackLevel = DuringFighterUtils.GetAttackLevel(act, pas, act.FighterRatings.GroundGame, pas.FighterRatings.Dodging, fightAttributes);


			switch (attackLevel)
			{
				case 1:
					Comment.GetComment(ReadTxts.ReadFileToList("GnP1"), fightAttributes);
					break;
				case 2:
					Comment.GetComment(ReadTxts.ReadFileToList("GnP2"), fightAttributes);
					break;
				case 3:
					Comment.GetComment(ReadTxts.ReadFileToList("GnP3"), fightAttributes);
					break;
			}


			Comment.DoComment(act, pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);

			fightAttributes.HitLocation = Comment.ExtractHitLocation(fightAttributes.FullComment);

			StatsUtils.UpdateStatistic(fightAttributes.Statistics, GetFighterActions.GetFighterNumber(act, fightAttributes), StatisticsTypes.stGnP, 0, Comment.ExtractHitsLanded(fightAttributes.FullComment));


			double at = RandomUtils.FixedRandomInt(act.FighterRatings.Gnp) + act.FighterRatings.AttackBonus(act.FighterFightAttributes) + GetGnPBonusByGuard();

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
					at += RandomUtils.FixedRandomInt(act.FighterRatings.Punching / 2);
					break;
			}

			at += RandomUtils.GeSmallRandom();
			at = DuringFighterUtils.GetGasTankFactor(act, at);
			at -= DuringFighterUtils.GetHurtFactor(act);

			double def = RandomUtils.FixedRandomInt(pas.FighterRatings.Dodging) + pas.FighterRatings.DefenseBonus(pas.FighterFightAttributes);

			switch (new Random().Next(4))
			{
				case 0:
					def += RandomUtils.FixedRandomInt(pas.FighterRatings.Strength / 2);
					break;
				case 1:
					def += RandomUtils.FixedRandomInt(pas.FighterRatings.Agility / 2);
					break;
				case 2:
					def += RandomUtils.FixedRandomInt(pas.FighterRatings.Dodging / 2);
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

				act.FighterStyles.Stalling++;

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
				switch (attackLevel)
				{
					case 1:
					case 2:
					case 3:
						Comment.DoComment(act, pas, Comment.ExtractComment(fightAttributes.FullComment), Pbp, fightAttributes);
						break;
				}

				act.FighterStyles.Stalling = 0;

				double damageDone = (at - def) * act.FighterRatings.DamageBonus(act.FighterFightAttributes) * attackLevel;
				DuringFighterUtils.DamageFighter(act, pas, damageDone, fightAttributes, fightAttributes.Statistics);

				if (CheckActions.CheckKO(act, pas, damageDone, fightAttributes.KOSubProb, fightAttributes))
				{
					DuringFighterUtils.ProcessKO(act, pas, Pbp, fightAttributes);
				}

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

				PositionUtils.ProcessAfterMovePosition(act, pas, Comment.ExtractFinalSuccessPosition(fightAttributes.FullComment), fightAttributes);

				StatsUtils.UpdateStatistic(fightAttributes.Statistics, GetFighterActions.GetFighterNumber(act, fightAttributes), StatisticsTypes.stGnP, 0, Comment.ExtractHitsLanded(fightAttributes.FullComment));
			}
		}


	}
}

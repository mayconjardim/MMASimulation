using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Stand
{
	public static class StandActions
	{
		public static void ActStandUp(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
		{
			double at, def;


			Comment.GetComment(ReadTxts.ReadFileToList("StandUp"), fightAttributes);

			Comment.DoComment(act, pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);


			at = RandomUtils.FixedRandomInt(act.FighterRatings.Agility) + act.FighterRatings.DefenseBonus(act.FighterFightAttributes);
			at += RandomUtils.FixedRandomInt(act.FighterRatings.Control / 2);
			switch (new Random().Next(4))
			{
				case 0: at += RandomUtils.FixedRandomInt(act.FighterRatings.Strength / 2); break;
				case 1: at += RandomUtils.FixedRandomInt(act.FighterRatings.GroundGame / 2); break;
				case 2: at += RandomUtils.FixedRandomInt(act.FighterRatings.Dodging / 2); break;
				case 3: at += RandomUtils.FixedRandomInt(act.FighterRatings.ClinchGrappling / 2); break;
			}

			at += RandomUtils.GeSmallRandom();
			at = DuringFighterUtils.GetGasTankFactor(act, at);
			at -= DuringFighterUtils.GetHurtFactor(act);

			// Defensive value
			def = RandomUtils.FixedRandomInt(pas.FighterRatings.Aggressiveness) + pas.FighterRatings.AttackBonus(pas.FighterFightAttributes);
			// def += RandomUtils.FixedRandomInt(pas.Control / 2);
			switch (new Random().Next(4))
			{
				case 0: def += RandomUtils.FixedRandomInt(pas.FighterRatings.Strength / 2); break;
				case 1: def += RandomUtils.FixedRandomInt(pas.FighterRatings.Agility / 2); break;
				case 2: def += RandomUtils.FixedRandomInt(pas.FighterRatings.Kicking / 2); break;
				case 3: def += RandomUtils.FixedRandomInt(pas.FighterRatings.GroundGame / 2); break;
			}

			def += RandomUtils.GeSmallRandom();
			def = DuringFighterUtils.GetGasTankFactor(pas, def);
			def -= DuringFighterUtils.GetHurtFactor(pas);

			// Checking damage
			if (def >= at)
			{
				Comment.DoComment(act, pas, Comment.ExtractFailureComment(fightAttributes.FullComment), Pbp, fightAttributes);
				pas.FighterStyles.Stalling++;
			}
			else
			{
				Comment.DoComment(act, pas, Comment.ExtractComment(fightAttributes.FullComment), Pbp, fightAttributes);

				PositionUtils.ProcessAfterMovePosition(act, pas, 17, fightAttributes);

				if ((RandomUtils.GetRandom() < DuringFighterUtils.GetGasTankFactor(pas, pas.FighterRatings.Agility))
					&& (!act.FighterFightAttributes.OnTheGround) && (pas.FighterFightAttributes.OnTheGround))
				{
					ActStandUp(pas, act, Pbp, fightAttributes);
				}
			}
		}


	}
}

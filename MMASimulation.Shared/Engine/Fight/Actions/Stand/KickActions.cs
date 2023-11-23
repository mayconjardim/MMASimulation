using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
using MMASimulation.Shared.Engine.Fight.Actions.Counter;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Enums;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Stand
{
    public static class KickActions
    {

        public static void ActKick(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {

            int attackLevel = DuringFighterUtils.GetAttackLevel(act, pas, act.FighterRatings.Kicking, pas.FighterRatings.Dodging, fightAttributes);

            switch (attackLevel)
            {
                case 1:
                    Comment.GetComment(ReadTxts.ReadFileToList("Kick1"), fightAttributes);
                    break;
                case 2:
                    Comment.GetComment(ReadTxts.ReadFileToList("Kick2"), fightAttributes);
                    break;
                case 3:
                    Comment.GetComment(ReadTxts.ReadFileToList("Kick3"), fightAttributes);
                    break;
            }

            Comment.DoComment(act, pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);

            fightAttributes.HitLocation = Comment.ExtractHitLocation(fightAttributes.FullComment);

            StatsUtils.UpdateStatistic(fightAttributes.Statistics, GetFighterActions.GetFighterNumber(act, fightAttributes), StatisticsTypes.stKicks, Comment.ExtractHitsLaunched(fightAttributes.FullComment), 0);

            // Attacking value
            double at = RandomUtils.FixedRandomInt(act.FighterRatings.Kicking) + act.FighterRatings.AttackBonus(act.FighterFightAttributes);
            at = at - Sim.KICKMALUS * attackLevel;
            switch (RandomUtils.GetRandomValue(4))
            {
                case 0:
                    at += RandomUtils.FixedRandomInt(act.FighterRatings.Strength / 2);
                    break;
                case 1:
                    at += RandomUtils.FixedRandomInt(act.FighterRatings.Agility / 2);
                    break;
                case 2:
                    at += RandomUtils.FixedRandomInt(act.FighterRatings.Dodging / 2);
                    break;
                case 3:
                    at += RandomUtils.FixedRandomInt(act.FighterRatings.Aggressiveness / 2);
                    break;
            }
            at = at + RandomUtils.GeSmallRandom();
            at = DuringFighterUtils.GetGasTankFactor(act, at);
            at = at - DuringFighterUtils.GetHurtFactor(act);

            // Defensive value
            double def = RandomUtils.FixedRandomInt(pas.FighterRatings.Dodging) + pas.FighterRatings.DefenseBonus(pas.FighterFightAttributes);
            switch (RandomUtils.GetRandomValue(4))
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
                    def += RandomUtils.FixedRandomInt(pas.FighterRatings.Control / 2);
                    break;
            }
            def = def + RandomUtils.GeSmallRandom();
            def = DuringFighterUtils.GetGasTankFactor(pas, def);
            def = def - DuringFighterUtils.GetHurtFactor(pas);

            if (def >= at)
            {
                Comment.DoComment(act, pas, Comment.ExtractFailureComment(fightAttributes.FullComment), Pbp, fightAttributes);

                // Counter attack
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
                // Do comments
                Comment.DoComment(act, pas, Comment.ExtractComment(fightAttributes.FullComment), Pbp, fightAttributes);

                // Damage
                double damageDone = (at - def) * act.FighterRatings.DamageBonus(act.FighterFightAttributes) * attackLevel * Sim.KICKDAMAGEBONUS;
                DuringFighterUtils.DamageFighter(act, pas, damageDone, fightAttributes, fightAttributes.Statistics);

                PositionUtils.ProcessAfterMovePosition(act, pas, Comment.ExtractFinalSuccessPosition(fightAttributes.FullComment), fightAttributes);

                // Check KO
                if (CheckActions.CheckKO(act, pas, damageDone, fightAttributes.KOSubProb, fightAttributes))
                {
                    DuringFighterUtils.ProcessKO(act, pas, Pbp, fightAttributes);
                }

                int injuryType = CheckActions.CheckInjury(act, pas, damageDone, fightAttributes.InjuryProb, fightAttributes);
                if (injuryType != Sim.INJURYORCUTFALSE)
                {
                    DuringFighterUtils.ProcessInjury(act, pas, injuryType, Pbp, fightAttributes);
                }

                // Check Cut
                injuryType = CheckActions.CheckCut(act, pas, damageDone, fightAttributes.CutProb, fightAttributes);
                if (injuryType != Sim.INJURYORCUTFALSE)
                {
                    DuringFighterUtils.ProcessCut(act, pas, injuryType, Pbp, fightAttributes);
                }

                // Modifying statistics
                StatsUtils.UpdateStatistic(fightAttributes.Statistics, GetFighterActions.GetFighterNumber(act, fightAttributes), StatisticsTypes.stKicks, 0, Comment.ExtractHitsLanded(fightAttributes.FullComment));
            }
        }


    }
}

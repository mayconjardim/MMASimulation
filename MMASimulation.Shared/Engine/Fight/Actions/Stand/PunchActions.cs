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
    public static class PunchActions
    {

        public static void ActPunch(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            double At, Def, DamageDone;
            int AttackLevel, InjuryType;

            AttackLevel = DuringFighterUtils.GetAttackLevel(act, pas, act.FighterRatings.Punching, pas.FighterRatings.Dodging, fightAttributes);

            switch (AttackLevel)
            {
                case 1:
                    Comment.GetComment(ReadTxts.ReadFileToList("Punch1"), fightAttributes);
                    break;
                case 2:
                    Comment.GetComment(ReadTxts.ReadFileToList("Punch2"), fightAttributes);
                    break;
                case 3:
                    Comment.GetComment(ReadTxts.ReadFileToList("Punch3"), fightAttributes);
                    break;
            }

            Comment.DoComment(act, pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);

            fightAttributes.HitLocation = Comment.ExtractHitLocation(fightAttributes.FullComment);

            StatsUtils.UpdateStatistic(fightAttributes.Statistics, GetFighterActions.GetFighterNumber(act, fightAttributes), StatisticsTypes.stPunches, Comment.ExtractHitsLaunched(fightAttributes.FullComment), 0);

            //Valor ofensivo
            At = RandomUtils.FixedRandomInt(act.FighterRatings.Punching) + act.FighterRatings.AttackBonus(act.FighterFightAttributes);
            switch (RandomUtils.GetRandomValue(4))
            {
                case 0:
                    At += RandomUtils.FixedRandomInt(act.FighterRatings.Strength / 2);
                    break;
                case 1:
                    At += RandomUtils.FixedRandomInt(act.FighterRatings.Agility / 2);
                    break;
                case 2:
                    At += RandomUtils.FixedRandomInt(act.FighterRatings.Dodging / 2);
                    break;
                case 3:
                    At += RandomUtils.FixedRandomInt(act.FighterRatings.ClinchStriking / 2);
                    break;
            }
            At += RandomUtils.GeSmallRandom();
            At = DuringFighterUtils.GetGasTankFactor(act, At);
            At -= DuringFighterUtils.GetHurtFactor(act);

            // Valor defensivo 
            Def = RandomUtils.FixedRandomInt(pas.FighterRatings.Dodging) + pas.FighterRatings.DefenseBonus(pas.FighterFightAttributes);
            switch (RandomUtils.GetRandomValue(4))
            {
                case 0:
                    Def += RandomUtils.FixedRandomInt(pas.FighterRatings.Strength / 2);
                    break;
                case 1:
                    Def += RandomUtils.FixedRandomInt(pas.FighterRatings.Agility / 2);
                    break;
                case 2:
                    Def += RandomUtils.FixedRandomInt(pas.FighterRatings.Punching / 2);
                    break;
                case 3:
                    Def += RandomUtils.FixedRandomInt(pas.FighterRatings.ClinchStriking / 2);
                    break;
            }
            Def += RandomUtils.GeSmallRandom(); ;
            Def = DuringFighterUtils.GetGasTankFactor(pas, Def);
            Def -= DuringFighterUtils.GetHurtFactor(pas);

            //Verificando dano
            if (Def >= At)
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
                switch (AttackLevel)
                {
                    case 1:
                    case 2:
                    case 3:
                        Comment.DoComment(act, pas, Comment.ExtractComment(fightAttributes.FullComment), Pbp, fightAttributes);
                        break;
                }

                // Dano
                DamageDone = (At - Def) * act.FighterRatings.DamageBonus(act.FighterFightAttributes) * AttackLevel;
                DuringFighterUtils.DamageFighter(act, pas, DamageDone, fightAttributes, fightAttributes.Statistics);

                PositionUtils.ProcessAfterMovePosition(act, pas, Comment.ExtractFinalSuccessPosition(fightAttributes.FullComment), fightAttributes);

                // Verificar KO
                if (CheckActions.CheckKO(act, pas, DamageDone, fightAttributes.KOSubProb, fightAttributes))
                {
                    DuringFighterUtils.ProcessKO(act, pas, Pbp, fightAttributes);
                }

                // Verificar Injury
                InjuryType = CheckActions.CheckInjury(act, pas, DamageDone, fightAttributes.InjuryProb, fightAttributes);
                if (InjuryType != Sim.INJURYORCUTFALSE)
                {
                    DuringFighterUtils.ProcessInjury(act, pas, InjuryType, Pbp, fightAttributes);
                }

                // Verificar Cut
                InjuryType = CheckActions.CheckCut(act, pas, DamageDone, fightAttributes.CutProb, fightAttributes);
                if (InjuryType != Sim.INJURYORCUTFALSE)
                {
                    DuringFighterUtils.ProcessCut(act, pas, InjuryType, Pbp, fightAttributes);
                }

                // Stats
                StatsUtils.UpdateStatistic(fightAttributes.Statistics, GetFighterActions.GetFighterNumber(act, fightAttributes), StatisticsTypes.stPunches, 0, Comment.ExtractHitsLanded(fightAttributes.FullComment));
            }
        }


    }
}

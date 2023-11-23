using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
using MMASimulation.Shared.Engine.Fight.Actions.Counter;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;
using System.Diagnostics.Metrics;

namespace MMASimulation.Shared.Engine.Fight.Actions.Clinch
{
    public static class ClinchMoves
    {

        public static void ActKeepClinch(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            if (fightAttributes.RoundFinished)
            {
                return;
            }

            if (fightAttributes.InTheClinch)
            {
                if (RandomUtils.GetRandomValue(100) <= pas.FighterStrategies.StratAvoidClinch)
                {
                    fightAttributes.IsCounter = true;
                    ActBreakClinch(pas, act, Pbp, fightAttributes);
                }
            }
        }

        public static void ActBreakClinch(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            double At, Def;

            Comment.GetComment(ReadTxts.ReadFileToList("BreakClinch1"), fightAttributes);

            Comment.DoComment(act, pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);

            At = RandomUtils.FixedRandomInt(act.FighterRatings.ClinchMean());
            At += act.FighterRatings.DefenseBonus(act.FighterFightAttributes);

            switch (RandomUtils.GetRandomValue(4))
            {
                case 0:
                    At += RandomUtils.FixedRandomInt(act.FighterRatings.Strength / 2);
                    break;
                case 1:
                    At += RandomUtils.FixedRandomInt(act.FighterRatings.Agility / 2);
                    break;
                case 2:
                    At += RandomUtils.FixedRandomInt(act.FighterRatings.ClinchGrappling / 2);
                    break;
                case 3:
                    At += RandomUtils.FixedRandomInt(act.FighterRatings.ClinchStriking / 2);
                    break;
            }

            At += RandomUtils.GeSmallRandom();
            At = DuringFighterUtils.GetGasTankFactor(act, At);
            At -= DuringFighterUtils.GetHurtFactor(act);

            // Valor defensivo
            Def = RandomUtils.FixedRandomInt(pas.FighterRatings.ClinchMean());
            Def += pas.FighterRatings.AttackBonus(pas.FighterFightAttributes);

            switch (RandomUtils.GetRandomValue(4))
            {
                case 0:
                    Def += RandomUtils.FixedRandomInt(pas.FighterRatings.Strength / 2);
                    break;
                case 1:
                    Def += RandomUtils.FixedRandomInt(pas.FighterRatings.Agility / 2);
                    break;
                case 2:
                    Def += RandomUtils.FixedRandomInt(pas.FighterRatings.ClinchGrappling / 2);
                    break;
                case 3:
                    Def += RandomUtils.FixedRandomInt(pas.FighterRatings.Aggressiveness / 2);
                    break;
            }

            Def += RandomUtils.GeSmallRandom();
            Def = DuringFighterUtils.GetGasTankFactor(pas, Def);
            Def -= DuringFighterUtils.GetHurtFactor(pas);


            // Verificando dano
            if (Def >= At)
            {
                Comment.DoComment(act, pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);

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
                Comment.DoComment(act, pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);
                PositionUtils.ProcessAfterMovePosition(act, pas, Comment.ExtractFinalSuccessPosition(fightAttributes.FullComment), fightAttributes);
            }
        }


        public static void ActPunchClinch(Fighter act, Fighter pas, int ClinchType, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            const double GRAPPLING_MOD = 0.6;

            double At, Def, DamageDone, DamageMod;
            int attackLevel, InjuryType;

            attackLevel = DuringFighterUtils.GetAttackLevel(act, pas, (act.FighterRatings.Punching + act.FighterRatings.ClinchStriking) / 2,
                    (pas.FighterRatings.Dodging + pas.FighterRatings.ClinchStriking) / 2, fightAttributes);


            if (ClinchType == -1)
            {
                ClinchType = GetClinchActions.GetClinchPunchType(act);
            }
            DamageMod = 1;



            switch (ClinchType)
            {
                case Sim.THAI_ATTACK:
                    switch (attackLevel)
                    {
                        case 1:
                            Comment.GetComment(ReadTxts.ReadFileToList("ThaiPunch1"), fightAttributes);
                            break;
                        case 2:
                            Comment.GetComment(ReadTxts.ReadFileToList("ThaiPunch2"), fightAttributes);
                            break;
                        case 3:
                            Comment.GetComment(ReadTxts.ReadFileToList("ThaiPunch3"), fightAttributes);
                            break;
                    }
                    DamageMod = 1;
                    break;

                case Sim.DIRTY_BOXING:
                    switch (attackLevel)
                    {
                        case 1:
                            Comment.GetComment(ReadTxts.ReadFileToList("DirtyBoxing1"), fightAttributes);
                            break;
                        case 2:
                            Comment.GetComment(ReadTxts.ReadFileToList("DirtyBoxing2"), fightAttributes);
                            break;
                        case 3:
                            Comment.GetComment(ReadTxts.ReadFileToList("DirtyBoxing3"), fightAttributes);
                            break;
                    }
                    DamageMod = 1;
                    break;

                case Sim.GRAPPLING_ATTACK:
                    switch (attackLevel)
                    {
                        case 1:
                            Comment.GetComment(ReadTxts.ReadFileToList("GrapplingPunch1"), fightAttributes);
                            break;
                        case 2:
                            Comment.GetComment(ReadTxts.ReadFileToList("GrapplingPunch2"), fightAttributes);
                            break;
                        case 3:
                            Comment.GetComment(ReadTxts.ReadFileToList("GrapplingPunch2"), fightAttributes);
                            break;
                    }
                    DamageMod = GRAPPLING_MOD;
                    break;
            }

            Comment.DoComment(act, pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);

            fightAttributes.HitLocation = Comment.ExtractHitLocation(fightAttributes.FullComment);

            At = RandomUtils.FixedRandomInt((act.FighterRatings.Punching + act.FighterRatings.ClinchStriking) / 2) + act.FighterRatings.AttackBonus(act.FighterFightAttributes);

            switch (RandomUtils.GetRandomValue(4))
            {
                case 0: At += RandomUtils.FixedRandomInt(act.FighterRatings.Strength / 2); break;
                case 1: At += RandomUtils.FixedRandomInt(act.FighterRatings.Agility / 2); break;
                case 2: At += RandomUtils.FixedRandomInt(act.FighterRatings.Dodging / 2); break;
                case 3: At += RandomUtils.FixedRandomInt(act.FighterRatings.ClinchStriking / 2); break;
            }

            At += RandomUtils.GeSmallRandom();
            At = DuringFighterUtils.GetGasTankFactor(act, At);
            At -= DuringFighterUtils.GetHurtFactor(act);

            Def = RandomUtils.FixedRandomInt((pas.FighterRatings.Dodging + pas.FighterRatings.ClinchStriking) / 2) + pas.FighterRatings.DefenseBonus(pas.FighterFightAttributes);

            switch (RandomUtils.GetRandomValue(4))
            {
                case 0: Def += RandomUtils.FixedRandomInt(pas.FighterRatings.Strength / 2); break;
                case 1: Def += RandomUtils.FixedRandomInt(pas.FighterRatings.Agility / 2); break;
                case 2: Def += RandomUtils.FixedRandomInt(pas.FighterRatings.ClinchGrappling / 2); break;
                case 3: Def += RandomUtils.FixedRandomInt(pas.FighterRatings.ClinchStriking / 2); break;
            }

            Def += RandomUtils.GeSmallRandom();
            Def = DuringFighterUtils.GetGasTankFactor(pas, Def);
            Def -= DuringFighterUtils.GetHurtFactor(pas);

            if (Def >= At)
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
                        GetClinchActions.RefBreakClinch(act, pas, Pbp, fightAttributes);
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
                    case 1: Comment.DoComment(act, pas, Comment.ExtractComment(fightAttributes.FullComment), Pbp, fightAttributes); break;
                    case 2: Comment.DoComment(act, pas, Comment.ExtractComment(fightAttributes.FullComment), Pbp, fightAttributes); ; break;
                    case 3: Comment.DoComment(act, pas, Comment.ExtractComment(fightAttributes.FullComment), Pbp, fightAttributes); break;
                }

                DamageDone = (At - Def) * act.FighterRatings.DamageBonus(act.FighterFightAttributes) * attackLevel * DamageMod;
                DuringFighterUtils.DamageFighter(act, pas, DamageDone, fightAttributes, fightAttributes.Statistics);

                PositionUtils.ProcessAfterMovePosition(act, pas, Comment.ExtractFinalFailurePosition(fightAttributes.FullComment), fightAttributes);


                if (CheckActions.CheckKO(act, pas, DamageDone, fightAttributes.KOSubProb, fightAttributes))
                {
                    DuringFighterUtils.ProcessKO(act, pas, Pbp, fightAttributes);
                }

                int injuryType = CheckActions.CheckInjury(act, pas, DamageDone, fightAttributes.InjuryProb, fightAttributes);
                if (injuryType != Sim.INJURYORCUTFALSE)
                {
                    DuringFighterUtils.ProcessInjury(act, pas, injuryType, Pbp, fightAttributes);
                }

                injuryType = CheckActions.CheckCut(act, pas, DamageDone, fightAttributes.CutProb, fightAttributes);
                if (injuryType != Sim.INJURYORCUTFALSE)
                {
                    DuringFighterUtils.ProcessCut(act, pas, injuryType, Pbp, fightAttributes);
                }
            }
        }

    }


}


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
            int AttackLevel, InjuryType;

            AttackLevel = GetAttackLevel(act, pas, (act.GetPunching() + act.GetClinchStriking()) / 2,
                    (pas.GetDodging() + pas.GetClinchStriking()) / 2);

            if (ClinchType == -1)
            {
                ClinchType = GetClinchActions.GetClinchPunchType(act);
            }
            DamageMod = 1;

            switch (ClinchType)
            {
                case Sim.THAI_ATTACK:
                    switch (AttackLevel)
                    {
                        case 1: GetComment(Comments.ThaiPunch1); break;
                        case 2: GetComment(Comments.ThaiPunch2); break;
                        case 3: GetComment(Comments.ThaiPunch3); break;
                    }
                    DamageMod = 1;
                    break;

                case Sim.DIRTY_BOXING:
                    switch (AttackLevel)
                    {
                        case 1: GetComment(Comments.DirtyBoxing1); break;
                        case 2: GetComment(Comments.DirtyBoxing2); break;
                        case 3: GetComment(Comments.DirtyBoxing3); break;
                    }
                    DamageMod = 1;
                    break;

                case Sim.GRAPPLING_ATTACK:
                    switch (AttackLevel)
                    {
                        case 1: GetComment(Comments.GrapplingPunch1); break;
                        case 2: GetComment(Comments.GrapplingPunch2); break;
                        case 3: GetComment(Comments.GrapplingPunch2); break;
                    }
                    DamageMod = GRAPPLING_MOD;
                    break;
            }

            DoComment(act, pas, ExtractInitComment(FullComment));

            HitLocation = ExtractHitLocation(FullComment);

            At = fixedRandomInt((act.GetPunching() + act.GetClinchStriking()) / 2) + act.GetAttackBonus();

            switch (Random.Next(4))
            {
                case 0: At += fixedRandomInt(act.GetStrength() / 2); break;
                case 1: At += fixedRandomInt(act.GetAgility() / 2); break;
                case 2: At += fixedRandomInt(act.GetDodging() / 2); break;
                case 3: At += fixedRandomInt(act.GetClinchStriking() / 2); break;
            }

            At += ApplicationUtils.GetSRandom();
            At = GetGasTankFactor(act, At);
            At -= GetHurtFactor(act);

            Def = fixedRandomInt((pas.GetDodging() + pas.GetClinchStriking()) / 2) + pas.GetDefenseBonus();

            switch (Random.Next(4))
            {
                case 0: Def += fixedRandomInt(pas.GetStrength() / 2); break;
                case 1: Def += fixedRandomInt(pas.GetAgility() / 2); break;
                case 2: Def += fixedRandomInt(pas.GetClinchGrappling() / 2); break;
                case 3: Def += fixedRandomInt(pas.GetClinchStriking() / 2); break;
            }

            Def += ApplicationUtils.GetSRandom();
            Def = GetGasTankFactor(pas, Def);
            Def -= GetHurtFactor(pas);

            if (Def >= At)
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
                        RefBreakClinch(act, pas);
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
                switch (AttackLevel)
                {
                    case 1: DoComment(act, pas, ExtractComment(FullComment)); break;
                    case 2: DoComment(act, pas, ExtractComment(FullComment)); break;
                    case 3: DoComment(act, pas, ExtractComment(FullComment)); break;
                }

                DamageDone = (At - Def) * act.GetDamageBonus() * AttackLevel * DamageMod;
                DamageFighter(act, pas, DamageDone);

                ProcessAfterMovePosition(act, pas, ExtractFinalSuccessPosition(FullComment));

                if (CheckKO(act, pas, DamageDone, KOSubProb))
                {
                    ProcessKO(act, pas);
                }

                InjuryType = CheckInjury(act, pas, DamageDone, InjuryProb);
                if (InjuryType != ApplicationUtils.INJURYORCUTFALSE)
                {
                    ProcessInjury(act, pas, InjuryType);
                }

                InjuryType = CheckCut(act, pas, DamageDone, CutProb);
                if (InjuryType != ApplicationUtils.INJURYORCUTFALSE)
                {
                    ProcessCut(act, pas, InjuryType);
                }
            }
        }

        private void HandleClinchType(Comment punch1, Comment punch2, Comment punch3, ref double damageMod, double grapplingMod = 1)
        {
            switch (AttackLevel)
            {
                case 1: GetComment(punch1); break;
                case 2: GetComment(punch2); break;
                case 3: GetComment(punch3); break;
            }
            damageMod = ClinchType == GRAPPLING_ATTACK ? grapplingMod : 1;
        }
    }


}
}

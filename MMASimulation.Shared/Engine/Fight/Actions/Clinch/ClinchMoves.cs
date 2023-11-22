using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

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
                    fightAttributes.IsCounter = CheckActions.CheckCounterAttack(act, pas, CounterProb, fightAttributes);

                    if (fightAttributes.IsCounter)
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
                    fightAttributes.IsCounter = false;
                    ProcessAfterMovePosition(act, pas, ExtractFinalFailurePosition(FullComment));
                }
            }
            else
            {
                Comment.DoComment(act, pas, Comment.ExtractInitComment(fightAttributes.FullComment), Pbp, fightAttributes);
                ProcessAfterMovePosition(act, pas, ExtractFinalSuccessPosition(FullComment));
            }
        }


    }
}

using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.ActionsController
{
    public static class CheckActions
    {
        public static bool CheckCounterAttack(Fighter act, Fighter pas, double prob, FightAttributes fightAttributes)
        {
            double At, Def;
            int Counter1, Counter2;

            //Se não houver contra-golpe o resultado é falso
            Counter1 = Comment.ExtractCounterMove1(fightAttributes.FullComment);
            Counter2 = Comment.ExtractCounterMove2(fightAttributes.FullComment);

            if ((Counter1 == 0) && (Counter2 == 0))
            {
                return false;
            }

            // Valor do contra-golpe de ataque
            At = RandomUtils.FixedRandomInt(act.FighterRatings.Control);
            At -= prob;
            At -= RandomUtils.FixedRandomInt(act.FighterRatings.Aggressiveness);
            At += RandomUtils.GeSmallRandom();

            // Valor do contra-golpee defensivo
            Def = RandomUtils.FixedRandomInt(pas.FighterRatings.Aggressiveness);
            Def += RandomUtils.FixedRandomInt(pas.FighterRatings.Control / 2);
            Def /= Sim.COUNTERATTACKCUT;

            // Resultado
            if (RandomUtils.FixedRandomInt(Def) > At)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckKO(Fighter act, Fighter pas, double DamageDone, int Prob, FightAttributes fightAttributes)
        {
            const double DAZED_PROB = 1.5;
            bool result = false;

            DamageDone = DuringFighterUtils.UpsetSystem(act, pas, DamageDone, fightAttributes);

            double At = DamageDone / (Sim.KOPROBCUT + fightAttributes.KOFreq);
            At += Prob;

            // Defense KO Res
            double Def = (pas.FighterRatings.GetKoResistance(pas.FighterFightAttributes) + RandomUtils.FixedRandomInt(pas.FighterFightAttributes.CurrentHP / 5)) / Sim.KOFREQUENCY;

            // Resolution
            if (At > Def)
            {
                if (RandomUtils.FixedRandomInt(At) > RandomUtils.FixedRandomInt(Def))
                {
                    result = true;
                    fightAttributes.FinishMode = Sim.RES_KO;
                }
            }
            else if (At * DAZED_PROB > Def)
            {
                pas.FighterFightAttributes.KoResistanceMod--;
                result = false;
                pas.FighterFightAttributes.Dazed = true;
            }

            if (!result)
            {
                result = CheckTKO(act, pas, DamageDone, Prob);
                fightAttributes.FinishMode = Sim.RES_TKO;
            }

            return result;
        }

        public static bool CheckTKO(Fighter act, Fighter pas, double DamageDone, int Prob)
        {
            bool result = false;

            if (pas.FighterFightAttributes.Dazed && act.FighterFightAttributes.Rush > Sim.TKORUSHMINIMUN && pas.FighterFightAttributes.CurrentHP < Sim.TKOMINHITPOINTS)
            {
                if (RandomUtils.GetBalancedRandom(Sim.TKOFREQUENCY) < 7) // 7 = Referee.TKOAwareness
                {
                    result = true;
                }
            }

            return result;
        }

        public static int CheckInjury(Fighter act, Fighter pas, double DamageDone, int InjuryProb, FightAttributes fightAttributes)
        {
            const int MAX_INJURY = 20;


            int Prob = (int)Math.Round(DamageDone / (Sim.INJURYCUT + fightAttributes.InjuryFreq)) + InjuryProb + RandomUtils.GetRandom();

            int value = RandomUtils.GetRandomValue(MAX_INJURY) * (act.FighterFightAttributes.InjuryResistance + 2);
            int injuryLimitB = RandomUtils.GetBalancedRandom(value + Sim.BIGINJURIES);
            int injuryLimitS = RandomUtils.GetBalancedRandom(value + Sim.SMALLINJURIES);

            if (Prob >= injuryLimitB)
            {
                return Sim.BIGINJURYORCUTTRUE;
            }
            else if (Prob >= injuryLimitS)
            {
                return Sim.SMALLINJURYORCUTTRUE;
            }
            else
            {
                return Sim.INJURYORCUTFALSE;
            }
        }


    }
}

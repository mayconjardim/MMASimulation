using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.FightUtils
{
    public static class DuringFighterUtils
    {

        public static int GetHurtFactor(Fighter act)
        {
            int result = 0;
            double hurtFactor = act.FighterFightAttributes.CurrentHP / act.FighterRatings.Toughness;

            switch (Convert.ToInt32(Math.Round(hurtFactor * 10)))
            {
                case int n when n >= -99 && n <= 10:
                    result = 10;
                    break;
                case int n when n >= 11 && n <= 15:
                    result = 5;
                    break;
                case int n when n >= 16 && n <= 20:
                    result = 4;
                    break;
                case int n when n >= 21 && n <= 25:
                    result = 3;
                    break;
                case int n when n >= 26 && n <= 30:
                    result = 2;
                    break;
                case int n when n >= 31 && n <= 45:
                    result = 1;
                    break;
                case int n when n >= 46 && n <= 99:
                    result = 0;
                    break;
            }

            return result * Sim.HURTFACTOR;
        }

        public static int GetGasTankFactor(Fighter act, double Value)
        {
            double reducingFactor = act.FighterFightAttributes.CurrentStamina * Sim.FATIGUECUT / (act.FighterRatings.Conditioning * 5);
            return (int)Math.Round(Value * reducingFactor);
        }

        public static int GetPercentage(double max, double actual)
        {
            int result = 0;

            if (max > 0)
            {
                double aux = 100.0 * actual / max;
                result = (int)Math.Round(aux);
            }

            return result;
        }

        public static int GetTotalTime(FightAttributes fightAttributes)
        {
            int result;

            if (fightAttributes.CurrentRound > 1)
            {
                result = 5 * 60;

                if (fightAttributes.CurrentRound > 2)
                {
                    result += (fightAttributes.CurrentRound - 1) * 5 + fightAttributes.CurrentTime;
                }
                else
                {
                    result += fightAttributes.CurrentTime;
                }
            }
            else
            {
                result = fightAttributes.CurrentTime;
            }

            return result;
        }

        public static double GetFightAction(Fighter fighter1, Fighter fighter2, FightAttributes attributes)
        {

            return (fighter1.FighterFightAttributes.TotalPoints() + fighter2.FighterFightAttributes.TotalPoints() / (GetTotalTime(attributes) + 1));
        }

        public static void RefStandFighters(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {

            List<Fighter> fighters = [act, pas];

            if (fightAttributes.BoutFinished)
            {
                return;
            }

            // Levante-se dos lutadores quando eles estiverem parados no chão
            if (GetFighterOnGround(act, pas) == 2)
            {

                if (5 < act.FighterStyles.Stalling + pas.FighterStyles.Stalling)
                {
                    act.FighterFightAttributes.OnTheGround = false;
                    pas.FighterFightAttributes.OnTheGround = false;
                    act.FighterStyles.Stalling = 0;
                    pas.FighterStyles.Stalling = 0;

                    Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("RefStandUp")), Pbp, fightAttributes);
                }
            }

            // Levante-se dos lutadores quando um deles estiver no chão
            else if (GetFighterOnGround(act, pas) >= 0 && GetFighterOnGround(act, pas) <= 1)
            {

                if (fighters[GetFighterOnGround(act, pas)].FighterFightAttributes.RoundsInTheGround > 1)
                {
                    if (5 + RandomUtils.GeSmallRandom()
                        < (fighters[GetFighterOnGround(act, pas)].FighterFightAttributes.RoundsInTheGround) * Sim.REFTENDENCYTOSTANDUP)
                    {
                        act.FighterFightAttributes.OnTheGround = false;
                        pas.FighterFightAttributes.OnTheGround = false;
                        act.FighterStyles.Stalling = 0;
                        pas.FighterStyles.Stalling = 0;
                        fighters[0].FighterFightAttributes.RoundsInTheGround = 0;
                        fighters[1].FighterFightAttributes.RoundsInTheGround = 0;

                        if (act.FighterFightAttributes.OnTheGround)
                        {
                            Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("RefStandUpOneFighter")), Pbp, fightAttributes);

                        }
                        else
                        {
                            Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("RefStandUpOtherFighter")), Pbp, fightAttributes);

                        }
                    }
                }
            }
        }

        public static int GetFighterOnGround(Fighter fighter1, Fighter fighter2)
        {
            if (fighter1.FighterFightAttributes.OnTheGround && fighter2.FighterFightAttributes.OnTheGround)
            {
                return 2;
            }
            else if (!fighter1.FighterFightAttributes.OnTheGround && fighter2.FighterFightAttributes.OnTheGround)
            {
                return 1;
            }
            else if (fighter1.FighterFightAttributes.OnTheGround && !fighter2.FighterFightAttributes.OnTheGround)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

    }
}

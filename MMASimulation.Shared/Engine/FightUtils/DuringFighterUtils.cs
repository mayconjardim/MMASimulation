using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Models.Fighters;

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

    }
}

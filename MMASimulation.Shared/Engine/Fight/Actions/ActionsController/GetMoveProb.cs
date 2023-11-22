using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;

namespace MMASimulation.Shared.Engine.Fight.Actions.ActionsController
{
    public static class GetMoveProb
    {

        public static int CounterMoveProb(Fighter act, int counterMove)
        {
            // Retorna um valor de probabilidade para um contra-ataque baseado nas estratégias do lutador
            int result = 0;

            switch (counterMove)
            {
                case 1:
                    result = RandomUtils.FixedRandomInt(act.FighterStrategies.StratPunching);
                    break;
                case 2:
                    result = RandomUtils.FixedRandomInt(act.FighterStrategies.StratKicking);
                    break;
                case 3:
                case 6:
                case 7:
                    result = RandomUtils.FixedRandomInt(act.FighterStrategies.StratSub);
                    break;
                case 4:
                case 12:
                case 14:
                    result = RandomUtils.FixedRandomInt(act.FighterStrategies.StratTakedowns);
                    break;
                case 5:
                case 9:
                    result = RandomUtils.FixedRandomInt(act.FighterStrategies.StratSub);
                    break;
                case 8:
                case 13:
                    result = RandomUtils.FixedRandomInt(act.FighterStrategies.StratGNP);
                    break;
                case 10:
                    result = RandomUtils.FixedRandomInt(act.FighterStrategies.StratPositioning);
                    break;
                case 11:
                case 15:
                    result = RandomUtils.FixedRandomInt(act.FighterStrategies.StratStandUp);
                    break;
            }

            return result;
        }

    }
}

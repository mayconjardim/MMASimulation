using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;

namespace MMASimulation.Shared.Engine.Fight.Actions.ActionsController
{
    public static class GetFightersInitiatives
    {
        public static int StandUpInitiative(Fighter act, Fighter pas, int bonus1, int bonus2)
        {

            //Fighter1 Iniciativa 
            double fighter1Ini = RandomUtils.GetBalancedRandom(act.FighterRatings.Aggressiveness + act.FighterRatings.Control / 4);
            fighter1Ini += RandomUtils.GetBalancedRandom(act.FighterRatings.Agility / 2);
            fighter1Ini += RandomUtils.GetBalancedRandom(act.FighterFightAttributes.CurrentStamina / 10);
            fighter1Ini += RandomUtils.GetRandom() * 1.5;
            fighter1Ini += act.FighterRatings.InitiativeBonus(act.FighterFightAttributes.Rush);
            fighter1Ini += act.FighterRatings.Mean() / 8;
            fighter1Ini -= DuringFighterUtils.GetHurtFactor(act);

            //Fighter2 Iniciativa
            double fighter2Ini = RandomUtils.GetBalancedRandom(pas.FighterRatings.Aggressiveness + pas.FighterRatings.Control / 4);
            fighter2Ini += RandomUtils.GetBalancedRandom(pas.FighterRatings.Agility / 2);
            fighter2Ini += RandomUtils.GetBalancedRandom(pas.FighterFightAttributes.CurrentStamina / 10);
            fighter2Ini += RandomUtils.GetRandom() * 1.5;
            fighter2Ini += pas.FighterRatings.InitiativeBonus(pas.FighterFightAttributes.Rush);
            fighter2Ini += pas.FighterRatings.Mean() / 8;
            fighter2Ini -= DuringFighterUtils.GetHurtFactor(pas);

            int result = -1;
            if (fighter1Ini > fighter2Ini)
            {
                result = 0;
                if (act.FighterFightAttributes.Rush < Sim.MAXRUSH)
                {
                    act.FighterFightAttributes.Rush += 1;
                }
                pas.FighterFightAttributes.Rush = 0;
            }
            else
            {
                result = 1;
                if (pas.FighterFightAttributes.Rush < Sim.MAXRUSH)
                {
                    pas.FighterFightAttributes.Rush += 1;
                }
                act.FighterFightAttributes.Rush = 0;
            }

            return result;
        }


    }
}

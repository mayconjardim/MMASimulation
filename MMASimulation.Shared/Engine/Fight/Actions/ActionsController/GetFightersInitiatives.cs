using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;

namespace MMASimulation.Shared.Engine.Fight.actions.actionsController
{
    public static class GetFightersInitiatives
    {
        public static int StandUpInitiative(Fighter act, Fighter pas)
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

        public static int GroundInitiative(Fighter act, Fighter pas)
        {

            //Fighter1 Iniciativa 
            double Fighter1Ini = RandomUtils.GetBalancedRandom(act.FighterRatings.Aggressiveness + act.FighterRatings.Control / 4);
            Fighter1Ini += RandomUtils.GetBalancedRandom(act.FighterFightAttributes.CurrentStamina / 10);
            Fighter1Ini += RandomUtils.GetBalancedRandom(act.FighterRatings.GroundGame / 2);
            Fighter1Ini += RandomUtils.GetRandom() * 1.5;
            Fighter1Ini += act.FighterRatings.InitiativeBonus(act.FighterFightAttributes.Rush);
            Fighter1Ini -= DuringFighterUtils.GetHurtFactor(act);

            //Fighter2 Iniciativa
            double Fighter2Ini = RandomUtils.GetBalancedRandom(pas.FighterRatings.Aggressiveness + pas.FighterRatings.Control / 4);
            Fighter2Ini += RandomUtils.GetBalancedRandom(pas.FighterFightAttributes.CurrentStamina / 10);
            Fighter2Ini += RandomUtils.GetBalancedRandom(pas.FighterRatings.GroundGame / 2);
            Fighter2Ini += RandomUtils.GetRandom() * 1.5;
            Fighter2Ini += pas.FighterRatings.InitiativeBonus(pas.FighterFightAttributes.Rush); ;
            Fighter2Ini -= DuringFighterUtils.GetHurtFactor(pas);

            int result = -1;
            if (Fighter1Ini > Fighter2Ini)
            {
                if (act.FighterFightAttributes.Rush < Sim.MAXRUSH)
                {
                    act.FighterFightAttributes.Rush = act.FighterFightAttributes.Rush + 1;
                }
                pas.FighterFightAttributes.Rush = 0;
            }
            else
            {
                result = 1;
                if (pas.FighterFightAttributes.Rush < Sim.MAXRUSH)
                {
                    pas.FighterFightAttributes.Rush = pas.FighterFightAttributes.Rush + 1;
                }
                act.FighterFightAttributes.Rush = 0;
            }

            return result;
        }

    }
}

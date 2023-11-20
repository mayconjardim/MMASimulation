using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.ActionsController
{
    public static class GetFighterActions
    {
        private static int GetFighterAction(Fighter act, Fighter pas, FightAttributes fightAttributes)
        {
            Random random = new Random();

            if (random.Next(Sim.ACTIONFREQUENCY) < act.FighterRatings.Aggressiveness + act.FighterFightAttributes.Rush
                || fightAttributes.InTheClinch || act.FighterFightAttributes.OnTheGround || pas.FighterFightAttributes.OnTheGround)
            {
                if (act.FighterFightAttributes.OnTheGround && pas.FighterFightAttributes.OnTheGround)
                {
                    return GetGroundAction(act, pas);
                }
                else if (!act.FighterFightAttributes.OnTheGround && pas.FighterFightAttributes.OnTheGround)
                {
                    return GetStandToGroundAction(act, pas);
                }
                else if (act.FighterFightAttributes.OnTheGround && !pas.FighterFightAttributes.OnTheGround)
                {
                    return GetGroundToStandAction(act, pas);
                }
                else if (fightAttributes.InTheClinch)
                {
                    return GetClinchAction(act, pas);
                }
                else
                {
                    return GetStandUpAction(act, pas);
                }
            }
            else
            {
                return Sim.ACT_NOACTION;
            }
        }


    }
}

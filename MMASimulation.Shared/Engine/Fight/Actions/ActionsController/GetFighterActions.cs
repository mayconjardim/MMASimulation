using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.Actions.Ground;
using MMASimulation.Shared.Engine.Fight.Actions.Stand;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.ActionsController
{
    public static class GetFighterActions
    {
        public static int FighterAction(Fighter act, Fighter pas, FightAttributes fightAttributes)
        {
            Random random = new Random();

            if (random.Next(Sim.ACTIONFREQUENCY) < act.FighterRatings.Aggressiveness + act.FighterFightAttributes.Rush
                || fightAttributes.InTheClinch || act.FighterFightAttributes.OnTheGround || pas.FighterFightAttributes.OnTheGround)
            {
                if (act.FighterFightAttributes.OnTheGround && pas.FighterFightAttributes.OnTheGround)
                {
                    return GetGroundActions.GroundAction(act, pas, fightAttributes);
                }
                else if (!act.FighterFightAttributes.OnTheGround && pas.FighterFightAttributes.OnTheGround)
                {
                    return GetStandToGroundActions.StandToGroundAction(act, pas, fightAttributes);
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
        public static int GetFighterNumber(Fighter act, FightAttributes fightAttributes)
        {
            if (act.Id == fightAttributes.TempFigher1.Id)
            {
                return 0;
            }
            else if (act.Id == fightAttributes.TempFigher2.Id)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

    }
}

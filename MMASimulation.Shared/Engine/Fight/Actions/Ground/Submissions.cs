using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Ground
{
    public static class Submissions
    {

        public static bool GetSubmissionAvailable(Fighter act, FightAttributes fightAttributes)
        {
            if (act.FighterStyles.TechSubs)
            {
                return true;
            }
            else if (act.FighterStyles.EasySubs && fightAttributes.FighterOnTop == GetFighterActions.GetFighterNumber(act, fightAttributes) &&
                    (fightAttributes.GuardType == Sim.FULL_MOUNT || fightAttributes.GuardType == Sim.REAR_MOUNT))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int GetSubmissionProbByPosition(Fighter act, FightAttributes fightAttributes)
        {
            const double FULL_MOUNT = 1.15;
            const double CLOSED_GUARD = 0.7;
            const double SIDE_MOUNT = 0.9;
            const double OPEN_GUARD = 0.75;
            const double HALF_GUARD = 0.75;

            double prob = act.FighterStrategies.StratSub;

            switch (fightAttributes.GuardType)
            {
                case 1:
                    prob *= FULL_MOUNT;
                    break;
                case 2:
                    prob *= SIDE_MOUNT;
                    break;
                case 3:
                    prob *= HALF_GUARD;
                    break;
                case 4:
                    prob *= OPEN_GUARD;
                    break;
                case 5:
                    prob *= CLOSED_GUARD;
                    break;
            }

            return (int)Math.Round(prob);
        }

    }
}

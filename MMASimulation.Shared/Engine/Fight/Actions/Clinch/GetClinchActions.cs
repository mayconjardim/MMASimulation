using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;

namespace MMASimulation.Shared.Engine.Fight.Actions.Clinch
{
    public static class GetClinchActions
    {
        public static int ClinchAction(Fighter act, Fighter pas)
        {
            int prob = new Random().Next(1, 101);

            int dirtyBoxing = act.FighterStrategies.StratDirtyBoxing;
            int thai = dirtyBoxing + act.FighterStrategies.StratThaiClinch;
            int avoidProb = thai + act.FighterStrategies.StratAvoidClinch;

            if (prob <= dirtyBoxing)
            {
                return GetDirtyBoxingAction(act);
            }
            else if (prob <= thai)
            {
                return GetThaiAction(act);
            }
            else if (prob <= avoidProb)
            {
                return Sim.ACT_BREAKCLINCH;
            }
            else
            {
                return Sim.ACT_TAKEDOWNCLINCH;
            }
        }

        public static int GetDirtyBoxingAction(Fighter act)
        {
            const double PUNCH_PROB = 1.25;

            double kneeProb = act.FighterStrategies.StratKicking + RandomUtils.GetRandom();
            double punchProb = (act.FighterStrategies.StratPunching + RandomUtils.GetRandom()) * PUNCH_PROB;

            if (kneeProb > punchProb)
            {
                return Sim.ACT_GRAPPLING_KNEE;
            }
            else
            {
                return Sim.ACT_DIRTYBOXING;
            }
        }

        public static int GetThaiAction(Fighter act)
        {
            const double KNEE_PROB = 1.25;

            double kneeProb = (act.FighterStrategies.StratKicking + RandomUtils.GetRandom()) * KNEE_PROB;
            double punchProb = act.FighterStrategies.StratPunching + RandomUtils.GetRandom();

            if (kneeProb > punchProb)
            {
                return Sim.ACT_THAICLINCH_KNEES;
            }
            else
            {
                return Sim.ACT_THAICLINCH_PUNCHES;
            }
        }

    }
}

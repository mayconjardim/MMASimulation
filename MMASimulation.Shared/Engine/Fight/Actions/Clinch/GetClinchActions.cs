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

        public static int GetClinchPunchType(Fighter act)
        {
            const double NO_SKILL_PROB = 0.5;
            const double IN_CLINCH_TYPE_PROB = 1.5;

            double thaiProb = RandomUtils.GetRandom();
            double dirtyProb = RandomUtils.GetRandom();
            double grapplingProb = RandomUtils.GetRandom();

            if (!act.FighterStyles.ThaiClinch)
            {
                thaiProb *= NO_SKILL_PROB;
            }
            else if (act.FighterStyles.ClinchType == Sim.THAI_CLINCH)
            {
                thaiProb *= IN_CLINCH_TYPE_PROB;
            }

            if (!act.FighterStyles.DirtyBoxing)
            {
                dirtyProb *= NO_SKILL_PROB;
            }
            else if (act.FighterStyles.ClinchType == Sim.CLINCH_DIRTY_BOXING)
            {
                dirtyProb *= IN_CLINCH_TYPE_PROB;
            }

            int result = Sim.GRAPPLING_ATTACK;

            if (grapplingProb > thaiProb && grapplingProb > dirtyProb)
            {
                result = Sim.GRAPPLING_ATTACK;
            }
            else if (dirtyProb > thaiProb && dirtyProb > grapplingProb)
            {
                result = Sim.DIRTY_BOXING;
            }
            else if (thaiProb > dirtyProb && thaiProb > grapplingProb)
            {
                result = Sim.THAI_ATTACK;
            }

            return result;
        }


    }
}

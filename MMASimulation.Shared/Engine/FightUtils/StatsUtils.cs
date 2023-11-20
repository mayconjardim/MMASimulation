using MMASimulation.Shared.Enums;
using MMASimulation.Shared.Models.Utils;

namespace MMASimulation.Shared.Engine.FightUtils
{
    public static class StatsUtils
    {

        public static void UpdateDamageDone(Statistics[] Statistics, int nFighter, double damage, bool clinch, bool ground)
        {
            if (!clinch && !ground)
            {
                Statistics[nFighter].DamageDone += damage;
            }
            else if (clinch)
            {
                IncreaseClinchDamage(Statistics, damage, nFighter);
            }
            else if (ground)
            {
                IncreaseGroundDamage(Statistics, damage, nFighter);
            }
        }

        public static void UpdateDamageReceived(Statistics[] Statistics, int nFighter, double damage, bool clinch, bool ground)
        {
            if (!clinch && !ground)
            {
                Statistics[nFighter].DamageReceived += damage;
            }
            else if (clinch)
            {
                IncreaseRClinchDamage(Statistics, damage, nFighter);
            }
            else if (ground)
            {
                IncreaseRGroundDamage(Statistics, damage, nFighter);
            }
        }

        public static void UpdateStatistic(Statistics[] Statistics, int nFighter, StatisticsTypes stat, int launched, int landed)
        {
            switch (stat)
            {
                case StatisticsTypes.stPunches:
                    Statistics[nFighter].PunchesLaunched += launched;
                    Statistics[nFighter].PunchesLanded += landed;
                    break;
                case StatisticsTypes.stKicks:
                    Statistics[nFighter].KicksLaunched += launched;
                    Statistics[nFighter].KicksLanded += landed;
                    break;
                case StatisticsTypes.stClinch:
                    Statistics[nFighter].ClinchStrLaunches += launched;
                    Statistics[nFighter].ClinchStrLanded += landed;
                    break;
                case StatisticsTypes.stGnP:
                    Statistics[nFighter].GnPStrLaunched += launched;
                    Statistics[nFighter].GnPStrLanded += landed;
                    break;
                case StatisticsTypes.stSubmission:
                    Statistics[nFighter].SubmissionsAttemps += launched;
                    Statistics[nFighter].SubmissionsAchieved += landed;
                    break;
                case StatisticsTypes.stTakedowns:
                    Statistics[nFighter].TakedownsAttemps += launched;
                    Statistics[nFighter].TakedownsAchieved += landed;
                    break;
                case StatisticsTypes.stGrappling:
                    Statistics[nFighter].GrapplingAttemps += launched;
                    Statistics[nFighter].GrapplingAchieved += landed;
                    break;
            }
        }

        public static void UpdateTimeOnGround(Statistics[] Statistics, int nFighter, int timeInc)
        {
            Statistics[nFighter].TimeOnTheGround += timeInc;
        }

        public static void IncreaseClinchDamage(Statistics[] Statistics, double damage, int nFighter)
        {
            Statistics[nFighter].DamageClinch += damage;
            Statistics[nFighter].DamageDone += damage;
        }

        public static void IncreaseGroundDamage(Statistics[] Statistics, double damage, int nFighter)
        {
            Statistics[nFighter].DamageGround += damage;
            Statistics[nFighter].DamageDone += damage;
        }

        public static void IncreaseRClinchDamage(Statistics[] Statistics, double damage, int nFighter)
        {
            Statistics[nFighter].TempDamageClinch += damage;
            Statistics[nFighter].DamageRClinch += damage;
            Statistics[nFighter].DamageReceived += damage;
        }

        public static void IncreaseRGroundDamage(Statistics[] Statistics, double damage, int nFighter)
        {
            Statistics[nFighter].TempDamageGround += damage;
            Statistics[nFighter].DamageRGround += damage;
            Statistics[nFighter].DamageReceived += damage;
        }

    }
}

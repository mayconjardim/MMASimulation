﻿namespace MMASimulation.Shared.Models.Utils
{
    public class Statistics
    {
        public double DamageDone { get; set; }
        public double DamageReceived { get; set; }
        public double DamageClinch { get; set; }
        public double DamageGround { get; set; }
        public double DamageRClinch { get; set; }
        public double DamageRGround { get; set; }
        public int TimeOnTheGround { get; set; }
        public int PunchesLaunched { get; set; }
        public int PunchesLanded { get; set; }
        public int KicksLaunched { get; set; }
        public int KicksLanded { get; set; }
        public int ClinchStrLaunches { get; set; }
        public int ClinchStrLanded { get; set; }
        public int GnPStrLaunched { get; set; }
        public int GnPStrLanded { get; set; }
        public int SubmissionsAttemps { get; set; }
        public int SubmissionsAchieved { get; set; }
        public int TakedownsAttemps { get; set; }
        public int TakedownsAchieved { get; set; }
        public int GrapplingAttemps { get; set; }
        public int GrapplingAchieved { get; set; }
        public double TempDamageClinch { get; set; }
        public double TempDamageGround { get; set; }

    }
}

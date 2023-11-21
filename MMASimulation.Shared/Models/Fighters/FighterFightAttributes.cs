namespace MMASimulation.Shared.Models.Fighters
{
    public class FighterFightAttributes
    {
        public double StrengthMod { get; set; }
        public double AgilityMod { get; set; }
        public double ConditioningMod { get; set; }
        public double KoResistanceMod { get; set; }
        public double ToughnessMod { get; set; }
        public double PunchingMod { get; set; }
        public double KickingMod { get; set; }
        public double ClinchStrikingMod { get; set; }
        public double ClinchGrapplingMod { get; set; }
        public double TakedownsMod { get; set; }
        public double GnpMod { get; set; }
        public double SubmissionMod { get; set; }
        public double GroundGameMod { get; set; }
        public double AggressivenessMod { get; set; }
        public double ControlMod { get; set; }
        public double MotivationMod { get; set; }
        public double DodgingMod { get; set; }
        public double SubDefenseMod { get; set; }
        public double TakedownsDefMod { get; set; }
        public double DamageMod { get; set; }
        public double InitMod { get; set; }

        public int PainAndTiredness { get; set; }
        public int FearManagement { get; set; }
        public int FightSpirit { get; set; }
        public int FightPerformance { get; set; }
        public int LatestResults { get; set; }

        public double FaceInjury { get; set; }
        public double LeftArmInjury { get; set; }
        public double RightArmInjury { get; set; }
        public double BackInjury { get; set; }
        public double RightLegInjury { get; set; }
        public double LeftLegInjury { get; set; }
        public double TorsoInjury { get; set; }

        public double AggPower { get; set; }
        public double Defense { get; set; }
        public int CareerStatus { get; set; }
        public double CurrentHP { get; set; }
        public double CurrentStamina { get; set; }
        public double StaminaLoss { get; set; }
        public double Accuracy { get; set; }
        public bool OnTheGround { get; set; }
        public bool Dazed { get; set; }
        public bool UseElbows { get; set; }
        public long DirtyMoveMalus { get; set; }
        public int Rush { get; set; }
        public int ActionsInGround { get; set; }
        public int ActionsInClinch { get; set; }
        public int ActionsInStandUp { get; set; }
        public double TempDamageGround { get; set; }
        public double TempDamageClinch { get; set; }
        public int RoundsInTheGround { get; set; }
        public double TrainingStatus { get; set; }
        public int InjuryResistance { get; set; }
        public int CutResistance { get; set; }
        public int Cuts { get; set; }
        public double Moral { get; set; }

        // Pontuacao
        public int[] RoundStandUpPoints { get; set; } = new int[10];
        public int[] RoundGroundPoints { get; set; } = new int[10];
        public int[] RoundAggPoints { get; set; } = new int[10];
        public int[] RoundTechPoints { get; set; } = new int[10];
        public int[] PointsPenalization { get; set; } = new int[10];

        public FighterFightAttributes()
        {
            StrengthMod = 0.0;
            AgilityMod = 0.0;
            ConditioningMod = 0.0;
            KoResistanceMod = 0.0;
            ToughnessMod = 0.0;
            PunchingMod = 0.0;
            KickingMod = 0.0;
            ClinchStrikingMod = 0.0;
            ClinchGrapplingMod = 0.0;
            TakedownsMod = 0.0;
            GnpMod = 0.0;
            SubmissionMod = 0.0;
            GroundGameMod = 0.0;
            AggressivenessMod = 0.0;
            ControlMod = 0.0;
            MotivationMod = 0.0;
            DodgingMod = 0.0;
            SubDefenseMod = 0.0;
            TakedownsDefMod = 0.0;
            DamageMod = 0.0;
            InitMod = 0.0;

            PainAndTiredness = 0;
            FearManagement = 0;
            FightSpirit = 0;
            FightPerformance = 0;
            LatestResults = 0;

            FaceInjury = 0.0;
            LeftArmInjury = 0.0;
            RightArmInjury = 0.0;
            BackInjury = 0.0;
            RightLegInjury = 0.0;
            LeftLegInjury = 0.0;
            TorsoInjury = 0.0;

            AggPower = 0.0;
            Defense = 0.0;
            CareerStatus = 2;
            CurrentHP = 0.0;
            CurrentStamina = 0.0;
            StaminaLoss = 0.0;
            Accuracy = 0.0;
            OnTheGround = false;
            Dazed = false;
            UseElbows = false;
            DirtyMoveMalus = 0L;
            Rush = 0;
            ActionsInGround = 0;
            ActionsInClinch = 0;
            ActionsInStandUp = 0;
            TempDamageGround = 0.0;
            TempDamageClinch = 0.0;
            RoundsInTheGround = 0;
            TrainingStatus = 0.0;
            InjuryResistance = 0;
            CutResistance = 0;
            Cuts = 0;
            Moral = 0.0;
        }

        public void ClearRoundPoints(int nRound)
        {
            RoundStandUpPoints[nRound] = 0;
            RoundGroundPoints[nRound] = 0;
            RoundAggPoints[nRound] = 0;
            RoundTechPoints[nRound] = 0;
        }

        public void ClearAllRoundPoints()
        {
            for (int i = 1; i <= 5; i++)
            {
                ClearRoundPoints(i);
            }
        }

        public bool CheckDirtyMove(double Aggressiveness, int DirtyFighting)
        {
            const int MAX_RANDOM = 120;
            int modifiers = 0;

            if (CurrentHP < 50)
            {
                modifiers += 1;
            }
            else if (CurrentHP < 20)
            {
                modifiers += 2;
            }

            if (CurrentStamina < 50)
            {
                modifiers += 1;
            }
            else if (CurrentStamina < 20)
            {
                modifiers += 2;
            }

            modifiers += (int)Math.Round(Aggressiveness / 7.0);

            modifiers *= DirtyFighting;

            return (new Random().NextDouble() * MAX_RANDOM <= modifiers);
        }


    }

}

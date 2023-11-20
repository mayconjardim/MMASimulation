using System.ComponentModel.DataAnnotations.Schema;

namespace MMASimulation.Shared.Models.Fights
{
    public class FightAttributes
    {

        public bool BoutFinished { get; set; }
        public bool InTheClinch { get; set; }
        public int TimeCurrent { get; set; }
        public int GuardType { get; set; }
        public int FighterOnTop { get; set; }
        public int CounterProb { get; set; }
        public int InjuryProb { get; set; }
        public int KOSubProb { get; set; }
        public int CutProb { get; set; }
        public int Randomness { get; set; }
        public int KOFreq { get; set; }
        public int FinishMode { get; set; }
        public bool Elbows { get; set; }
        public bool Stomps { get; set; }
        public bool SoccerKicks { get; set; }
        public bool IsCounter { get; set; }
        public int HitLocation { get; set; }
        public string FinishedType { get; set; } = string.Empty;
        public string FinishedDescription { get; set; } = string.Empty;
        public int FighterWinner { get; set; }
        public string MoveName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(2048)")]
        public string FullComment { get; set; } = string.Empty;
        public string Side { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string InjuryComment { get; set; } = string.Empty;
        public int SubLocation { get; set; }
        public int InjuryFreq { get; set; }
        public int NumHooks { get; set; }
        public int SubFreq { get; set; }
        public bool IsLockingASub { get; set; }

        // Clinch
        public int ClinchDirtyBoxing { get; set; }
        public int ThaiClinch { get; set; }
        public int SimpleGrappling { get; set; }

        // Clinch Attack
        public int ThaiAttack { get; set; }
        public int DirtyBoxing { get; set; }
        public int GrapplingAttack { get; set; }

        // Fight Tempo e Rounds
        public bool NoTimeLimits { get; set; }
        public int MinsForRound { get; set; }
        public bool Catchweight { get; set; }
        public int CurrentRound { get; set; }
        public bool RoundFinished { get; set; }
        public bool IsTournament { get; set; }
        public bool ColorComments { get; set; }
        public bool CrowdBoo { get; set; }
        public int FightSeconds { get; set; }
        public int CurrentTime { get; set; }



        public FightAttributes()
        {
            BoutFinished = false;
            InTheClinch = false;
            TimeCurrent = 0;
            GuardType = 0;
            FighterOnTop = 0;
            CounterProb = 0;
            InjuryProb = 0;
            KOSubProb = 0;
            CutProb = 0;
            Randomness = 0;
            KOFreq = 0;
            FinishMode = 0;
            Elbows = false;
            Stomps = false;
            SoccerKicks = false;
            IsCounter = false;
            HitLocation = 0;
            FinishedType = string.Empty;
            FinishedDescription = string.Empty;
            FighterWinner = -1;
            MoveName = string.Empty;

            FullComment = string.Empty;
            Side = string.Empty;
            Location = string.Empty;
            InjuryComment = string.Empty;
            SubLocation = 0;
            InjuryFreq = 0;
            NumHooks = 0;
            SubFreq = 0;
            IsLockingASub = false;

            // Clinch
            ClinchDirtyBoxing = 0;
            ThaiClinch = 1;
            SimpleGrappling = 2;

            // Clinch Attack
            ThaiAttack = 1;
            DirtyBoxing = 2;
            GrapplingAttack = 3;

            // Fight Tempo e Rounds
            NoTimeLimits = false;
            MinsForRound = 5;
            Catchweight = false;
            CurrentRound = 1;
            RoundFinished = false;
            IsTournament = false;
            ColorComments = true;
            CrowdBoo = false;
            FightSeconds = 0;
            CurrentTime = 0;

        }

    }
}

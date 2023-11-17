namespace MMASimulation.Shared.Dtos.Fighters
{
    public class FighterStylesDto
    {

        public int Id { get; set; }
        public int FighterId { get; set; }
        public required FighterDto Fighter { get; set; }

        public int FancyPunches { get; set; }
        public int FightingStyle { get; set; }
        public int TacticalStyle { get; set; }
        public int FancyKicks { get; set; }
        public int FancySubmissions { get; set; }
        public int DirtyFighting { get; set; }
        public int Stalling { get; set; }

        public bool EasySubs { get; set; }
        public bool TechSubs { get; set; }
        public bool UseKneesGround { get; set; }
        public bool UseStomps { get; set; }
        public bool UseSoccerKicks { get; set; }
        public bool PullsGuard { get; set; }

        public int ClinchType { get; set; }
        public bool DirtyBoxing { get; set; }
        public bool ThaiClinch { get; set; }
        public bool JudoTD { get; set; }
        public bool WrestlingTD { get; set; }

    }
}

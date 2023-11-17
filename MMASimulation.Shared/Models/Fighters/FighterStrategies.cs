namespace MMASimulation.Shared.Models.Fighters
{
    public class FighterStrategies
    {

        public int Id { get; set; }
        public int FighterId { get; set; }
        public required Fighter Fighter { get; set; }

        // General
        public int StratPunching { get; set; }
        public int StratKicking { get; set; }
        public int StratClinching { get; set; }
        public int StratTakedowns { get; set; }

        // Clinch
        public int StratDirtyBoxing { get; set; }
        public int StratThaiClinch { get; set; }
        public int StratClinchTakedowns { get; set; }
        public int StratAvoidClinch { get; set; }

        // Chão
        public int StratGNP { get; set; }
        public int StratSub { get; set; }
        public int StratPositioning { get; set; }
        public int StratLNP { get; set; }
        public int StratStandUp { get; set; }

    }
}

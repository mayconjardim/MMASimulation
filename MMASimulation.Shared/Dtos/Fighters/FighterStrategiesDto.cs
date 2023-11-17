namespace MMASimulation.Shared.Dtos.Fighters
{
    public class FighterStrategiesDto
    {

        public int Id { get; set; }
        public int FighterId { get; set; }
        public required FighterDto Fighter { get; set; }

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

        public FighterStrategiesDto()
        {
            // Geral
            StratPunching = 25;
            StratKicking = 25;
            StratClinching = 25;
            StratTakedowns = 25;

            // Clinch
            StratDirtyBoxing = 25;
            StratThaiClinch = 25;
            StratClinchTakedowns = 25;
            StratAvoidClinch = 25;

            // Chão
            StratGNP = 20;
            StratSub = 20;
            StratPositioning = 20;
            StratLNP = 20;
            StratStandUp = 20;
        }

    }
}

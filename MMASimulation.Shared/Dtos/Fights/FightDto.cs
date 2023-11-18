using MMASimulation.Shared.Dtos.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Dtos.Fights
{
    public class FightDto
    {

        public int Id { get; set; }
        public int NumberRounds { get; set; }
        public bool TitleBout { get; set; } = false;
        public bool GeneratePBP { get; set; } = false;
        public bool Happened { get; set; } = false;
        public int Fighter1Id { get; set; }
        public FighterDto Fighter1 { get; set; }
        public int Fighter2Id { get; set; }
        public FighterDto Fighter2 { get; set; }

        public List<FightPBP> Pbp { get; set; } = new List<FightPBP>();

    }
}

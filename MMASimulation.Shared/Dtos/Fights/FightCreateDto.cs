namespace MMASimulation.Shared.Dtos.Fights
{
    public class FightCreateDto
    {

        public int Fighter1Id { get; set; }
        public int Fighter2Id { get; set; }
        public int NumberRounds { get; set; }
        public bool TitleBout { get; set; } = false;

    }
}

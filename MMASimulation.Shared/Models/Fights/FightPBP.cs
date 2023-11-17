namespace MMASimulation.Shared.Models.Fights
{
    public class FightPBP
    {

        public long Id { get; set; }
        public long FightId { get; set; }
        public required Fight Fight { get; set; }
        public string PbpData { get; set; } = string.Empty;

    }
}

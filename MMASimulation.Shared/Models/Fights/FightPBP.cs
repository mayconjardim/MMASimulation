using System.ComponentModel.DataAnnotations.Schema;

namespace MMASimulation.Shared.Models.Fights
{
    public class FightPBP
    {

        public int Id { get; set; }
        public int FightId { get; set; }

        [ForeignKey(nameof(FightId))]
        public required Fight Fight { get; set; }

        public string PbpData { get; set; } = string.Empty;

    }
}

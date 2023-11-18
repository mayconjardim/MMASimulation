using System.ComponentModel.DataAnnotations.Schema;

namespace MMASimulation.Shared.Models.Fights
{
    public class FightPBP
    {

        public int Id { get; set; }
        public Fight? Fight { get; set; }
        public string PbpData { get; set; } = string.Empty;

    }
}

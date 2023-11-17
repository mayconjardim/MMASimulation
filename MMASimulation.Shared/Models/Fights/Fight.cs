﻿using MMASimulation.Shared.Models.Fighters;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMASimulation.Shared.Models.Fights
{
    public class Fight
    {

        public int Id { get; set; }
        public int NumberRounds { get; set; }
        public bool TitleBout { get; set; } = false;
        public bool GeneratePBP { get; set; } = false;
        public bool Happened { get; set; } = false;

        public int Fighter1Id { get; set; }
        [ForeignKey(nameof(Fighter1Id))]
        public required Fighter Fighter1 { get; set; }

        public int Fighter2Id { get; set; }
        [ForeignKey(nameof(Fighter2Id))]
        public required Fighter Fighter2 { get; set; }

        [ForeignKey("FightId")]
        public List<FightPBP> Pbp { get; set; } = new List<FightPBP>();

        [NotMapped]
        public FightAttributes? FightAttributes { get; set; }

    }
}

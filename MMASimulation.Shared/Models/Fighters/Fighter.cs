using System.ComponentModel.DataAnnotations.Schema;

namespace MMASimulation.Shared.Models.Fighters
{
    public class Fighter
    {

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Wins { get; set; } = 0;
        public int Loss { get; set; } = 0;
        public int Draw { get; set; } = 0;
        public string Face { get; set; } = string.Empty;

        public required FighterRatings FighterRatings { get; set; }
        public required FighterStrategies FighterStrategies { get; set; } = new FighterStrategies();
        public required FighterStyles FighterStyles { get; set; } = new FighterStyles();

        [NotMapped]
        public FighterFightAttributes? FighterFightAttributes { get; set; }

    }
}
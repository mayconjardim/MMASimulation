using System.ComponentModel.DataAnnotations.Schema;

namespace MMASimulation.Shared.Models.Fighters
{
    public class Fighter
    {

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Wins { get; set; }
        public int Loss { get; set; }
        public int Draw { get; set; }

        public required FighterRatings FighterRatings { get; set; }
        public required FighterStrategies FighterStrategies { get; set; }
        public required FighterStyles FighterStyles { get; set; }

        [NotMapped]
        public FighterFightAttributes? FighterFightAttributes { get; set; }

    }
}

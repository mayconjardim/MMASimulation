using MMASimulation.Shared.Enums;

namespace MMASimulation.Shared.Dtos.Fighters
{
    public class FighterCreateDto
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public WeightClass WeightClass { get; set; }

        public FighterRatingsDto? FighterRatings { get; set; }

    }
}

using MMASimulation.Shared.Models.Utils.Faces;

namespace MMASimulation.Shared.Dtos.Fighters
{
    public class FighterCreateDto
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Wins { get; set; }
        public int Loss { get; set; }
        public int Draw { get; set; }

        public FighterRatingsDto? FighterRatings { get; set; }
        public FighterStrategiesDto? FighterStrategies { get; set; }
        public FighterStylesDto? FighterStyles { get; set; }
        public Face? Face { get; set; }

    }
}

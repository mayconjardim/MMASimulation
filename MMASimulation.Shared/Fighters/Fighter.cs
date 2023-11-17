namespace MMASimulation.Shared.Fighters
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

    }
}

namespace MMASimulation.Shared.Dtos.Fighters
{
    public class FighterRatingsDto
    {

        public int Id { get; set; }
        public int FighterId { get; set; }
        public required FighterDto Fighter { get; set; }

        // Luta em pé
        public double Punching { get; set; }
        public double Kicking { get; set; }
        public double ClinchStriking { get; set; }
        public double ClinchGrappling { get; set; }
        public double Takedowns { get; set; }

        // Luta no Chão
        public double Gnp { get; set; }
        public double Submission { get; set; }
        public double GroundGame { get; set; }

        // Defesa
        public double Dodging { get; set; }
        public double SubDefense { get; set; }
        public double TakedownsDef { get; set; }

        // Mental
        public double Aggressiveness { get; set; }
        public double Control { get; set; }
        public double Motivation { get; set; }

        // Fisicas
        public double Strength { get; set; }
        public double Agility { get; set; }
        public double Conditioning { get; set; }
        public double KoResistance { get; set; }
        public double Toughness { get; set; }

    }
}

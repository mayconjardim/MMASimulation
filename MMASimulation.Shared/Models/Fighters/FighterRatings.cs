namespace MMASimulation.Shared.Models.Fighters
{
    public class FighterRatings
    {

        public int Id { get; set; }
        public int FighterId { get; set; }
        public required Fighter Fighter { get; set; }

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

        public double Ranking()
        {

            Random random = new Random();
            int number = random.Next(1, 6);

            double statsRanking = Punching + Kicking + ClinchStriking + Takedowns + ClinchGrappling + Aggressiveness
                    + Control + Motivation + Dodging + TakedownsDef + SubDefense + Strength + Toughness + Agility
                    + KoResistance + Conditioning + GroundGame + Submission + Gnp / 19;

            statsRanking += number;
            return statsRanking;
        }

        public double InitiativeBonus(int Rush)
        {
            double result = (Agility / 4) + (Aggressiveness / 6) + Rush;
            result = result * 100 / 100;
            return result;
        }

        public double Mean()
        {
            double result = (DefenseMean() + FitnessMean() + GroundMean() + MentalMean() + StrikingMean())
                    / 5;
            return result;
        }

        public double ClinchMean()
        {
            return (ClinchStriking + ClinchGrappling) / 2;
        }

        public double GroundMean()
        {
            double result = (GroundGame + Submission + Gnp) * 100 / 60;
            return result;
        }

        public double MentalMean()
        {
            double result = (Aggressiveness + Control + Motivation) * 100 / 60;
            return result;
        }

        public double StrikingMean()
        {
            double result = (Punching + Kicking + ClinchStriking + ClinchGrappling + Takedowns)
                    * 100 / 100;
            return result;
        }

        public double FitnessMean()
        {
            double result = Strength + Toughness + Agility + KoResistance + Conditioning;
            return result;
        }

        public double DefenseMean()
        {
            double result = (Dodging + TakedownsDef + SubDefense) * 100 / 60;
            return result;
        }

    }
}

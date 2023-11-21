namespace MMASimulation.Shared.Engine.FightUtils
{
    public static class RandomUtils
    {

        private static Random random = new Random();

        public static int FixedRandomInt(double value)
        {
            if (value < 0)
            {
                return 0;
            }

            int aux = (int)value;
            double doubleValue = value - aux;

            return (int)(aux / 2 + (random.NextDouble() * (aux / 2)) + 1 + doubleValue);
        }

        public static int GetRandomValue(int value)
        {
            return random.Next(value);
        }

        public static int GetRandom()
        {
            int BIGRANDOM = 20;
            int Randomness = 0;
            return random.Next(BIGRANDOM + Randomness) + 1;
        }

        public static int SetLimits(int actual, int max, int min)
        {
            if (actual > max)
            {
                actual = max;
            }
            else if (actual < min)
            {
                actual = min;
            }
            return actual;
        }
    }
}

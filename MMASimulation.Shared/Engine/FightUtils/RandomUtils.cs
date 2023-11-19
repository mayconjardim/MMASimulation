using System;

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


    }
}

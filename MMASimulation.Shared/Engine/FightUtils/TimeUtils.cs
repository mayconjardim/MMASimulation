namespace MMASimulation.Shared.Engine.FightUtils
{
    public static class TimeUtils
    {

        public static string GetTime(int currentTime)
        {
            int minutes = currentTime / 60;
            int seconds = currentTime % 60;

            string result = $"{minutes:D}:{seconds:D2}";

            return result;
        }

    }
}

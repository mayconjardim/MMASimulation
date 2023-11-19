using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Models.Fighters;

namespace MMASimulation.Shared.Engine.FightUtils
{
    public static class TimeUtils
    {
        public static int DeltaTime(Fighter fighter1, Fighter fighter2)
        {
            int result = RandomUtils.FixedRandomInt(Sim.TIMEADVANCE) - ((fighter1.FighterFightAttributes.Rush + fighter2.FighterFightAttributes.Rush) / 2);
            if (result < 1)
            {
                result = 1;
            }
            return result;
        }

        public static string GetTime(int currentTime)
        {
            int minutes = currentTime / 60;
            int seconds = currentTime % 60;

            string result = $"{minutes:D}:{seconds:D2}";

            return result;
        }

        public static string SecondsToMinutes(int currentTime, int currentRound)
        {
            int min = currentTime / 60;
            int sec = currentTime % 60;
            string clock = "The clock says ";
            string says = " in the round";

            if (sec > 9)
            {
                return clock + min + ":" + sec + says;
            }
            else
            {
                return clock + min + ":0" + sec + says;
            }
        }

    }
}

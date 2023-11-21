using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Stand
{
    public static class GetStandToGroundActions
    {
        public static int StandToGroundAction(Fighter act, Fighter pas, FightAttributes fightAttributes)
        {
            int goToGroundProb = RandomUtils.FixedRandomInt(act.FighterStrategies.StratTakedowns);
            int kickProb = RandomUtils.FixedRandomInt((act.FighterStrategies.StratKicking + act.FighterStrategies.StratStandUp) / 2);

            int result;
            if (goToGroundProb > kickProb)
            {
                result = Sim.ACT_MOVETOGROUND;
            }
            else
            {
                result = Sim.ACT_STANDKICK;
            }

            if (RandomUtils.GetRandom() > act.FighterRatings.Aggressiveness)
            {
                result = Sim.ACT_ALLOWSTAND;
            }

            //Soccer Kick 
            int soccerKickProb = 0;
            if (result == Sim.ACT_STANDKICK && fightAttributes.SoccerKicks && act.FighterStyles.UseSoccerKicks &&
                RandomUtils.FixedRandomInt(act.FighterRatings.Aggressiveness) + Sim.SOCCERKICKSFREQUENCY > 20)
            {
                soccerKickProb = RandomUtils.FixedRandomInt(act.FighterRatings.Kicking);
            }

            //Soccer Stomps
            int stompProb = 0;
            if (result == Sim.ACT_STANDKICK && fightAttributes.Stomps && act.FighterStyles.UseStomps &&
                RandomUtils.FixedRandomInt(act.FighterRatings.Aggressiveness) + Sim.STOPMSFREQUENCY > 20)
            {
                stompProb = RandomUtils.FixedRandomInt(act.FighterRatings.Kicking);
            }

            if (result == Sim.ACT_STANDKICK)
            {
                if (soccerKickProb > stompProb)
                {
                    result = Sim.ACT_SOCCERKICKS;
                }
                else if (stompProb > soccerKickProb)
                {
                    result = Sim.ACT_STOMPS;
                }
            }

            //Lutadores agressivos tentarão capitalizar
            if (pas.FighterFightAttributes.Dazed)
            {
                if (RandomUtils.FixedRandomInt(act.FighterRatings.Aggressiveness) > Sim.CAPITALIZEPROB)
                {
                    result = Sim.ACT_CAPITALIZEGROUND;
                }
            }

            return result;
        }

    }
}

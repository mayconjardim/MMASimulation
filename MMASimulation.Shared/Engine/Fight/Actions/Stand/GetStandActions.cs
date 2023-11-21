using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Stand
{
    public static class GetStandActions
    {
        public static int StandUpAction(Fighter act, Fighter pas, FightAttributes fightAttributes)
        {
            int prob = new Random().Next(1, 101);

            int punchProb = act.FighterStrategies.StratPunching;
            int kickProb = punchProb + act.FighterStrategies.StratKicking;
            int clinchProb = kickProb + act.FighterStrategies.StratClinching;

            int result;
            if (prob <= punchProb)
            {
                result = Sim.ACT_PUNCHES;
            }
            else if (prob <= kickProb)
            {
                result = Sim.ACT_KICKS;
            }
            else if (prob <= clinchProb)
            {
                result = Sim.ACT_CLINCH;
            }
            else
            {
                result = Sim.ACT_TAKEDOWNS;
            }

            // Dirty fighting
            if (act.FighterFightAttributes.CheckDirtyMove(act.FighterRatings.Aggressiveness, act.FighterStyles.DirtyFighting))
            {
                result = Sim.ACT_POKE;
            }

            // Fancy Punch Prob
            else if (act.FighterStyles.FancyPunches > 0 && result == Sim.ACT_PUNCHES)
            {
                if (RandomUtils.GetRandom() < act.FighterRatings.Agility &&
                    RandomUtils.GetRandom() < act.FighterRatings.Punching &&
                    RandomUtils.GetRandom() < act.FighterStyles.FancyPunches * Sim.FANCYMOVEPROB)
                {
                    result = Sim.ACT_FANCYPUNCH;
                }
            }

            //Fancy Kick Prob
            else if (act.FighterStyles.FancyKicks > 0 && result == Sim.ACT_KICKS)
            {
                if (RandomUtils.GetRandom() < act.FighterRatings.Agility &&
                    RandomUtils.GetRandom() < act.FighterRatings.Kicking &&
                    RandomUtils.GetRandom() < act.FighterStyles.FancyKicks * Sim.FANCYMOVEPROB)
                {
                    result = Sim.ACT_FANCYKICK;
                }
            }

            //Fancy Standing Submission Prob
            else if (act.FighterStyles.FancySubmissions > 0)
            {
                if (RandomUtils.GetRandom() < act.FighterRatings.Agility &&
                    RandomUtils.GetRandom() < act.FighterRatings.Submission &&
                    RandomUtils.GetRandom() < act.FighterStyles.FancySubmissions * Sim.FANCYMOVEPROB)
                {
                    result = Sim.ACT_FANCYSUB;
                }
            }

            //Prob Slam ou Prob Supplex
            else if (result == Sim.ACT_TAKEDOWNS)
            {
                if (act.FighterRatings.Strength > Sim.SLAMSTRENGTH &&
                    RandomUtils.GetRandom() < Sim.SLAMPROB)
                {
                    result = Sim.ACT_SLAM;
                }
                else if (act.FighterRatings.Strength > Sim.SUPPLEXSTRENGHT &&
                         RandomUtils.GetRandom() < Sim.SUPPLEXPROB)
                {
                    result = Sim.ACT_SUPPLEX;
                }
            }

            //Prob de descanso
            if (new Random().Next(100) < Sim.RESTFREQUENCY)
            {
                if (RandomUtils.GetRandom() > act.FighterRatings.Control &&
                    RandomUtils.GetRandom() * 5 > act.FighterFightAttributes.CurrentStamina &&
                    fightAttributes.CurrentTime > 100)
                {
                    result = Sim.ACT_REST;
                }
            }

            //Lutadores agressivos tentarão capitalizar
            if (pas.FighterFightAttributes.Dazed)
            {
                if (RandomUtils.FixedRandomInt(act.FighterRatings.Aggressiveness) > Sim.CAPITALIZEPROB)
                {
                    result = Sim.ACT_CAPITALIZESTAND;
                }
            }

            return result;
        }

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

        public static bool CheckPunchesExchange(Fighter act, Fighter pas, FightAttributes fightAttributes)
        {
            const int PUNCHES_EXC_PROB = 8;
            bool result = false;

            if (!act.FighterFightAttributes.OnTheGround && !pas.FighterFightAttributes.OnTheGround && !fightAttributes.InTheClinch)
            {
                if (RandomUtils.GetRandom() < PUNCHES_EXC_PROB)
                {
                    if (RandomUtils.GetRandom() < Math.Round(act.FighterRatings.Aggressiveness - act.FighterRatings.Control / 2) ||
                        RandomUtils.GetRandom() < Math.Round(pas.FighterRatings.Aggressiveness - pas.FighterRatings.Control / 2))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

    }
}

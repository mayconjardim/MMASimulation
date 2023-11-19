﻿using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;
using System.Numerics;
using System.Xml.Linq;

namespace MMASimulation.Shared.Engine.Comments.Utils
{
    public static class SpecificComments
    {

        public static void MakeLocutorComment(Fighter fighter1, Fighter fighter2, List<FightPBP> Pbp, FightAttributes fightAttributes, bool titleBout)
        {
            if (titleBout)
            {
                Comment.DoComment(fighter1, fighter2, Comment.ReturnComment(ReadTxts.ReadFileToList("TitleIntro")), Pbp, fightAttributes);
            }
            else
            {
                Comment.DoComment(fighter1, fighter2, Comment.ReturnComment(ReadTxts.ReadFileToList("NoTitleIntro")), Pbp, fightAttributes);
            }
        }

        public static void MakeOddsComment(Fighter fighter1, Fighter fighter2, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            double EQUALS_HIGH = 1.1;
            double EQUALS_LOW = 0.9;
            double WEIGHT_CLASS_DIFF_REDUCTION = 2.0;
            double comparedRanking;
            double r1, r2;

            r1 = (fighter1.FighterRatings.Ranking() * ((int)fighter1.WeightClass + 1) / WEIGHT_CLASS_DIFF_REDUCTION);
            r2 = (fighter2.FighterRatings.Ranking() * ((int)fighter2.WeightClass + 1) / WEIGHT_CLASS_DIFF_REDUCTION);
            comparedRanking = r1 / r2;

            if (comparedRanking > EQUALS_LOW && comparedRanking < EQUALS_HIGH)
            {
                Comment.DoComment(fighter1, fighter2, Comment.ReturnComment(ReadTxts.ReadFileToList("Odds2")), Pbp, fightAttributes);
            }
            else if (r1 > r2)
            {
                Comment.DoComment(fighter1, fighter2, Comment.ReturnComment(ReadTxts.ReadFileToList("Odds1")), Pbp, fightAttributes);
            }
            else
            {
                Comment.DoComment(fighter1, fighter2, Comment.ReturnComment(ReadTxts.ReadFileToList("Odds1")), Pbp, fightAttributes);
            }
        }

        public static void MakeFirstRoundComment(Fighter fighter1, Fighter fighter2, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            if (fightAttributes.CurrentRound == 1 && fightAttributes.FightSeconds == 0)
            {
                Comment.DoComment(fighter1, fighter2, Comment.ReturnComment(ReadTxts.ReadFileToList("FirstRound")), Pbp, fightAttributes);
            }
        }

        public static void MakeCurrentRoundComment(List<FightPBP> Pbp, int currentRound)
        {
            string blank = "";
            string comment = "=====Round " + currentRound + "=====";

            Pbp.Add(new FightPBP
            {
                PbpData = blank
            });

            Pbp.Add(new FightPBP
            {
                PbpData = comment
            });

            Pbp.Add(new FightPBP
            {
                PbpData = blank
            });
        }

        public static void MakeFightTimeComment(List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            if (fightAttributes.FightSeconds > 0 && fightAttributes.FightSeconds < 300)
            {
                string blank = "";
                string comment = TimeUtils.SecondsToMinutes(fightAttributes.FightSeconds, fightAttributes.CurrentRound);

                Pbp.Add(new FightPBP
                {
                    PbpData = blank
                });

                Pbp.Add(new FightPBP
                {
                    PbpData = comment
                });
            }
        }

    }
}

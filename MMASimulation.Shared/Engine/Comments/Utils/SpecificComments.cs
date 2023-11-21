using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;
using System.Collections.Generic;

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

        public static void WriteGuard(Fighter act, Fighter pas, FightAttributes fightAttributes, List<FightPBP> Pbp)
        {
            List<Fighter> fighters = [act, pas];

            int fighterNotOnTop = (fightAttributes.FighterOnTop == 1) ? 0 : 1;

            if (fightAttributes.InTheClinch)
            {
                Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("Clinching")), Pbp, fightAttributes);
            }
            else if (act.FighterFightAttributes.OnTheGround && pas.FighterFightAttributes.OnTheGround)
            {
                switch (fightAttributes.GuardType)
                {
                    case 0:
                        switch (fightAttributes.NumHooks)
                        {
                            case 0:
                                Comment.DoComment(fighters[fightAttributes.FighterOnTop], fighters[fighterNotOnTop], ReadTxts.ReadListToComment("Guards", 0), Pbp, fightAttributes);
                                break;
                            case 1:
                                Comment.DoComment(fighters[fightAttributes.FighterOnTop], fighters[fighterNotOnTop], ReadTxts.ReadListToComment("Guards", 6), Pbp, fightAttributes);
                                break;
                            case 2:
                                Comment.DoComment(fighters[fightAttributes.FighterOnTop], fighters[fighterNotOnTop], ReadTxts.ReadListToComment("Guards", 7), Pbp, fightAttributes);
                                break;
                        }
                        break;
                    case 1:
                        Comment.DoComment(fighters[fightAttributes.FighterOnTop], fighters[fighterNotOnTop], ReadTxts.ReadListToComment("Guards", 1), Pbp, fightAttributes);
                        break;
                    case 2:
                        Comment.DoComment(fighters[fightAttributes.FighterOnTop], fighters[fighterNotOnTop], ReadTxts.ReadListToComment("Guards", 3), Pbp, fightAttributes);
                        break;
                    case 3:
                        Comment.DoComment(fighters[fightAttributes.FighterOnTop], fighters[fighterNotOnTop], ReadTxts.ReadListToComment("Guards", 5), Pbp, fightAttributes);
                        break;
                    case 4:
                        Comment.DoComment(fighters[fightAttributes.FighterOnTop], fighters[fighterNotOnTop], ReadTxts.ReadListToComment("Guards", 4), Pbp, fightAttributes);
                        break;
                    case 5:
                        Comment.DoComment(fighters[fightAttributes.FighterOnTop], fighters[fighterNotOnTop], ReadTxts.ReadListToComment("Guards", 2), Pbp, fightAttributes);
                        break;
                    case 6:
                        Comment.DoComment(fighters[fightAttributes.FighterOnTop], fighters[fighterNotOnTop], ReadTxts.ReadListToComment("Guards", 9), Pbp, fightAttributes);
                        break;
                }
            }
            else if (act.FighterFightAttributes.OnTheGround && !pas.FighterFightAttributes.OnTheGround)
            {
                Comment.DoComment(fighters[fightAttributes.FighterOnTop], fighters[fighterNotOnTop], ReadTxts.ReadListToComment("Guards", 8), Pbp, fightAttributes);
            }
            else if (pas.FighterFightAttributes.OnTheGround && !act.FighterFightAttributes.OnTheGround)
            {
                Comment.DoComment(fighters[fightAttributes.FighterOnTop], fighters[fighterNotOnTop], ReadTxts.ReadListToComment("Guards", 8), Pbp, fightAttributes);
            }
        }

        public static void MakeColorComments(Fighter act, Fighter pas, FightAttributes fightAttributes, List<FightPBP> Pbp)
        {

            if (RandomUtils.GetRandom() < Sim.MOVECOMMMENTSFREQUENCY)
            {
                switch (RandomUtils.GetRandomValue(8))
                {
                    case 0:
                        ColorComments.MakeMoveComment(act, pas, Pbp, fightAttributes);
                        break;
                    case 1:
                        ColorComments.MakeStaminaComment(act, pas, Pbp, fightAttributes);
                        break;
                    case 2:
                        MakeToughnessComment(act, pas, Pbp, fightAttributes);
                        break;
                    case 3:
                        MakeDangerousStrikerComment(act, pas, Pbp, fightAttributes);
                        break;
                    case 4:
                        MakeDangerousSubComment(act, pas, Pbp, fightAttributes);
                        break;
                    case 5:
                        MakeDangerousClinchComment(act, pas, Pbp, fightAttributes);
                        break;
                    case 6:
                        MakeDangerousGnPComment(act, pas, Pbp, fightAttributes);
                        break;
                    case 7:
                        MakeFightStatusComment(act, pas, Pbp, fightAttributes);
                        break;
                }
            }

            MakeBleedingComment(act, pas, Pbp, fightAttributes);
            MakeBleedingComment(act, pas, Pbp, fightAttributes);

            if (fightAttributes.CrowdBoo)
            {
                MakeBooComment(act, pas, Pbp, fightAttributes);
            }
        }


    }
}

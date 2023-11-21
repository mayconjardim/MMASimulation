using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Comments.Utils
{
    public static class ColorComments
    {
        public static void MakeMoveComment(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            if (!act.FighterFightAttributes.OnTheGround && !pas.FighterFightAttributes.OnTheGround && !fightAttributes.InTheClinch)
            {
                int style = act.FighterStyles.FightingStyle;
                int modify = RandomUtils.FixedRandomInt(act.FighterRatings.Aggressiveness) - RandomUtils.FixedRandomInt(act.FighterRatings.Control);
                RandomUtils.SetLimits(modify, 3, -3);

                switch (style + modify)
                {
                    case -10:
                    case -9:
                    case -8:
                    case -7:
                    case -6:
                    case -5:
                    case -4:
                    case -3:
                    case -2:
                    case -1:
                    case 0:
                        Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("MoveBackward")), Pbp, fightAttributes);
                        break;
                    case 1:
                        Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("MoveBackward")), Pbp, fightAttributes);
                        break;
                    case 2:
                        Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("MoveForward")), Pbp, fightAttributes);
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("MoveForward")), Pbp, fightAttributes);
                        break;
                }
            }
        }

        public static void MakeStaminaComment(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {

            //Um ​​lutador está cansado se estiver com menos de 65% de resistência
            //E exausto se estiver abaixo de 30%
            int percentage = DuringFighterUtils.GetPercentage(act.FighterRatings.Conditioning * 5, act.FighterFightAttributes.CurrentStamina);

            string staminaComment = string.Empty;

            if (percentage >= -99 && percentage <= 30)
            {
                staminaComment = Comment.ReturnComment(ReadTxts.ReadFileToList("Exhausted"));
            }
            else if (percentage >= 31 && percentage <= 65)
            {
                staminaComment = Comment.ReturnComment(ReadTxts.ReadFileToList("Tired"));
            }
            else if (percentage >= 85 && percentage <= 100)
            {
                staminaComment = Comment.ReturnComment(ReadTxts.ReadFileToList("GoodShape"));
            }

            Comment.DoComment(act, pas, staminaComment, Pbp, fightAttributes);
        }

        public static void MakeToughnessComment(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {

            //Um ​​lutador se machuca se estiver com menos de 50% de resistência
            //E esta muito magoado se ele estiver abaixo de 25%
            int percentage = DuringFighterUtils.GetPercentage(act.FighterRatings.Conditioning * 5, act.FighterFightAttributes.CurrentStamina);

            string staminaComment = string.Empty;

            if (percentage >= -99 && percentage <= 30)
            {
                staminaComment = Comment.ReturnComment(ReadTxts.ReadFileToList("VeryHurt"));
            }
            else if (percentage >= 31 && percentage <= 65)
            {
                staminaComment = Comment.ReturnComment(ReadTxts.ReadFileToList("Hurt"));
            }
            else if (percentage >= 85 && percentage <= 100)
            {
                staminaComment = Comment.ReturnComment(ReadTxts.ReadFileToList("Healthy"));
            }

            Comment.DoComment(act, pas, staminaComment, Pbp, fightAttributes);
        }

        public static void MakeDangerousStrikerComment(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            if (act.FighterRatings.Punching > Sim.DANGEROUSCOMMENT && !act.FighterFightAttributes.OnTheGround &&
                !pas.FighterFightAttributes.OnTheGround && !fightAttributes.InTheClinch)
            {
                Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("DangerousStriker")), Pbp, fightAttributes);
            }
        }

        public static void MakeDangerousSubComment(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            if (act.FighterRatings.Submission > Sim.DANGEROUSCOMMENT && act.FighterFightAttributes.OnTheGround && pas.FighterFightAttributes.OnTheGround)
            {
                Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("DangerousSub")), Pbp, fightAttributes);
            }
        }

        public static void MakeDangerousGnPComment(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            if (act.FighterRatings.Gnp > Sim.DANGEROUSCOMMENT && act.FighterFightAttributes.OnTheGround && pas.FighterFightAttributes.OnTheGround)
            {
                Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("DangerousGnP")), Pbp, fightAttributes);
            }
        }

        public static void MakeDangerousClinchComment(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            if (act.FighterRatings.ClinchMean() > Sim.DANGEROUSCOMMENT && fightAttributes.InTheClinch)
            {
                Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("DangerousClinch")), Pbp, fightAttributes);
            }
        }



        public static void MakeFightStatusComment(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {

            const int ONE_SIDED = 2;
            const double EVEN = 0.15;

            if (act.FighterFightAttributes.TotalPoints() > 0 && pas.FighterFightAttributes.TotalPoints() > 0)
            {
                int points1, points2;
                double fightStatus;

                if (act.FighterFightAttributes.TotalPoints() > pas.FighterFightAttributes.TotalPoints())
                {
                    points1 = act.FighterFightAttributes.TotalPoints();
                    points2 = pas.FighterFightAttributes.TotalPoints();
                }
                else
                {
                    points1 = pas.FighterFightAttributes.TotalPoints();
                    points2 = act.FighterFightAttributes.TotalPoints();
                }

                fightStatus = (double)points1 / points2;

                if (fightStatus >= ONE_SIDED)
                {
                    Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("OneSided")), Pbp, fightAttributes);
                }
                else if (fightStatus > 1 - EVEN && fightStatus < 1 + EVEN)
                {
                    Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("EvenMatch")), Pbp, fightAttributes);
                }
            }
        }

        public static void MakeBleedingComment(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            if (act.FighterFightAttributes.Cuts > Sim.MINCUTSBLEEDING)
            {
                double randomValue = RandomUtils.GeSmallRandom();
                if (randomValue <= act.FighterFightAttributes.Cuts)
                {
                    Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("Bleeding")), Pbp, fightAttributes);
                }
            }
        }

    }
}

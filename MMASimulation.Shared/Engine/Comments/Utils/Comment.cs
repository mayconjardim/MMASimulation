using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Comments.Utils
{
    public static class Comment
    {
        public static void DoComment(Fighter act, Fighter pas, string comment, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            comment = ReplaceTokens(act, pas, comment, fightAttributes);

            if (Pbp == null)
            {
                Pbp = new List<FightPBP>();
            }

            Pbp.Add(new FightPBP
            {
                PbpData = comment
            });

        }

        public static string ReplaceTokens(Fighter act, Fighter pas, string comment, FightAttributes fightAttributes)
        {

            comment = comment.Replace("%site", Sim.CAGE);

            comment = comment.Replace("%HoldSite", Sim.FENCE);
            comment = comment.Replace("%holdSite", Sim.FENCE);

            comment = comment.Replace("%movename", fightAttributes.MoveName);

            comment = comment.Replace("%a1", act.FullName());
            comment = comment.Replace("%a2", act.Nickname);

            comment = comment.Replace("%d1", pas.FullName());
            comment = comment.Replace("%d2", pas.Nickname);

            comment = comment.Replace("%ref", "Herb Dean");

            comment = comment.Replace("[SIDE]", fightAttributes.Side);

            comment = comment.Replace("%location", fightAttributes.Location);

            if (fightAttributes.FinishMode == Sim.RES_TIMEOUT)
            {
                comment = comment.Replace("%method", fightAttributes.FinishedType);
            }
            else
            {
                comment = comment.Replace("%method", fightAttributes.FinishedType + " (" + fightAttributes.FinishedDescription + ")");
            }

            // Time and round
            comment = comment.Replace("%time_and_round", TimeUtils.GetTime(fightAttributes.CurrentTime) + " Round " + fightAttributes.CurrentRound.ToString());

            // Time
            comment = comment.Replace("%time", TimeUtils.GetTime(fightAttributes.CurrentTime));

            // Round
            comment = comment.Replace("%round", fightAttributes.CurrentRound.ToString());

            // Organization
            comment = comment.Replace("%organization", "MMALiga");

            comment = comment.Replace("%venue", "venue");

            return comment;
        }

        public static void GetComment(List<string> commentList, FightAttributes fightAttributes)
        {
            fightAttributes.FullComment = string.Empty;
            int listSize = commentList.Count;

            // If CommentList has more than 0 elements
            if (listSize > 0)
            {
                while (string.IsNullOrEmpty(fightAttributes.FullComment))
                {
                    fightAttributes.FullComment = commentList[new Random().Next(listSize)];
                }
            }

            // Extract necessary values
            fightAttributes.Side = GetLeftRight(fightAttributes.FullComment);
            fightAttributes.Location = GetLocationName(ExtractHitLocation(fightAttributes.FullComment));
        }

        public static string ReturnComment(List<string> commentList)
        {
            string comment = string.Empty;
            int listSize = commentList.Count;

            if (listSize > 0)
            {
                while (string.IsNullOrEmpty(comment))
                {
                    int randomIndex = new Random().Next(listSize);
                    comment = commentList[randomIndex];
                }
            }

            return comment;
        }

        public static string GetLeftRight(string comment)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(comment))
            {
                int loc = ExtractHitLocation(comment);

                if (new[] { 2, 4, 13, 15, 17, 19 }.Contains(loc))
                {
                    result = Sim.LEFT;
                }
                else
                {
                    result = Sim.RIGHT;
                }
            }

            return result;
        }

        public static string GetLocationName(int location)
        {
            List<string> Misc = ReadTxts.ReadFileToList("Misc");
            string result = string.Empty;

            if (location == 0)
            {
                location = new Random().Next(8) + 1;
            }

            switch (location)
            {
                case 1:
                    result = Misc[Sim.FOREHEAD];
                    break;
                case 2:
                    result = Misc[Sim.LEFT_EYE];
                    break;
                case 3:
                    result = Misc[Sim.RIGHT_EYE];
                    break;
                case 4:
                    result = Misc[Sim.LEFT_CHEEK];
                    break;
                case 5:
                    result = Misc[Sim.RIGHT_CHEEK];
                    break;
                case 6:
                    result = Misc[Sim.NOSE];
                    break;
                case 7:
                    result = Misc[Sim.MOUTH];
                    break;
                case 8:
                    result = Misc[Sim.CHIN];
                    break;
            }

            return result;
        }

        public static string ExtractInitComment(string Comment)
        {
            string result = "Unknown";
            string[] splitFullString = Comment.Split(';');

            if (splitFullString.Length > 0)
            {
                result = splitFullString[0];
            }

            return result;
        }

        public static int ExtractHitLocation(string comment)
        {
            int result = 0;
            string[] splitFullString = comment.Split(';');

            if (splitFullString.Length > 3)
            {
                int.TryParse(splitFullString[3], out result);
            }

            return result;
        }

        public static int ExtractCounterMove1(string Comment)
        {
            string[] splitFullString = Comment.Split(';');

            if (splitFullString.Length > 5)
            {
                return int.Parse(splitFullString[5]);
            }
            else
            {
                return 0;
            }
        }

        public static int ExtractCounterMove2(string Comment)
        {
            string[] splitFullString = Comment.Split(';');

            if (splitFullString.Length > 6)
            {
                return int.Parse(splitFullString[6]);
            }
            else
            {
                return 0;
            }
        }

        public static int ExtractHitsLaunched(string comment)
        {
            int result = 0;
            string[] splitFullString = comment.Split(';');

            if (splitFullString.Length > 8)
            {
                result = int.Parse(splitFullString[8]);
            }

            return result;
        }

        public static string ExtractFailureComment(string comment)
        {
            string result = "Unknown";
            string[] splitFullString = comment.Split(';');

            if (splitFullString.Length > 2)
            {
                result = splitFullString[2];
            }

            return result;
        }

        public static int ExtractFinalFailurePosition(string comment)
        {
            int result = 0;
            string[] splitFullString = comment.Split(';');

            if (splitFullString.Length > 14)
            {
                result = int.Parse(splitFullString[14]);
            }

            return result;
        }

        public static string ExtractComment(string comment)
        {
            string result = "Unknown";
            string[] splitFullString = comment.Split(';');

            if (splitFullString.Length > 1)
            {
                result = splitFullString[1];
            }

            return result;
        }

    }
}

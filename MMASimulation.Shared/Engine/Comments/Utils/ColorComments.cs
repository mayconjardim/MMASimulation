using MMASimulation.Shared.Engine.Comments.ReadTxt;
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

    }
}

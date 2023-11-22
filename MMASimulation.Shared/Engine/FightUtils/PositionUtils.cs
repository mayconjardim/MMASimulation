using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.FightUtils
{
    public static class PositionUtils
    {

        public static void ProcessAfterMovePosition(Fighter act, Fighter pas, int Position, FightAttributes fightAttributes)
        {

            if (Position != 0)
            {
                fightAttributes.GuardType = -1;
                fightAttributes.InTheClinch = false;
            }

            switch (Position)
            {
                case 1:
                    act.FighterFightAttributes.OnTheGround = false;
                    pas.FighterFightAttributes.OnTheGround = false;
                    fightAttributes.NumHooks = -1;
                    break;
                case 2:
                    act.FighterFightAttributes.OnTheGround = false;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(act, fightAttributes);
                    fightAttributes.GuardType = -1;
                    fightAttributes.NumHooks = -1;
                    break;
                case 3:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = false;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(pas, fightAttributes);
                    fightAttributes.GuardType = -1;
                    fightAttributes.NumHooks = -1;
                    break;
                case 4:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(act, fightAttributes);
                    fightAttributes.GuardType = 1;
                    fightAttributes.NumHooks = -1;
                    break;
                case 5:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(act, fightAttributes);
                    fightAttributes.GuardType = 4;
                    fightAttributes.NumHooks = -1;
                    break;
                case 6:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(act, fightAttributes);
                    fightAttributes.GuardType = 5;
                    fightAttributes.NumHooks = -1;
                    break;
                case 7:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(act, fightAttributes);
                    fightAttributes.GuardType = 0;
                    fightAttributes.NumHooks += 1;
                    Sim.SetLimits(fightAttributes.NumHooks, 2, 0);
                    break;
                case 8:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(pas, fightAttributes);
                    fightAttributes.GuardType = 1;
                    fightAttributes.NumHooks = -1;
                    break;
                case 9:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(pas, fightAttributes);
                    fightAttributes.GuardType = 4;
                    fightAttributes.NumHooks = -1;
                    break;
                case 10:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(pas, fightAttributes);
                    fightAttributes.GuardType = 5;
                    fightAttributes.NumHooks = -1;
                    break;
                case 11:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(pas, fightAttributes);
                    fightAttributes.GuardType = 0;
                    fightAttributes.NumHooks = -1;
                    break;
                case 12:
                    act.FighterFightAttributes.OnTheGround = false;
                    pas.FighterFightAttributes.OnTheGround = false;
                    fightAttributes.GuardType = -1;
                    fightAttributes.InTheClinch = true;
                    fightAttributes.NumHooks = -1;
                    break;
                case 13:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(act, fightAttributes);
                    fightAttributes.GuardType = 2;
                    fightAttributes.NumHooks = -1;
                    break;
                case 14:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(pas, fightAttributes);
                    fightAttributes.GuardType = 2;
                    fightAttributes.NumHooks = -1;
                    break;
                case 15:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(pas, fightAttributes);
                    break;
                case 16:
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    fightAttributes.FighterOnTop = GetFighterActions.GetFighterNumber(pas, fightAttributes);
                    fightAttributes.GuardType = 3;
                    fightAttributes.NumHooks = -1;
                    break;
                case 17:
                    act.FighterFightAttributes.OnTheGround = false;
                    fightAttributes.GuardType = -1;
                    fightAttributes.NumHooks = -1;
                    break;
                case 18:
                    fightAttributes.NumHooks--;
                    act.FighterFightAttributes.OnTheGround = true;
                    pas.FighterFightAttributes.OnTheGround = true;
                    Sim.SetLimits(fightAttributes.NumHooks, 2, 0);
                    break;
            }
        }


    }
}

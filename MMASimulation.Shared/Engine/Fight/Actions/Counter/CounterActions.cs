﻿using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
using MMASimulation.Shared.Engine.Fight.Actions.Clinch;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Counter
{
    public static class CounterActions
    {
        public static void DoCounterAttack(Fighter Act, Fighter Pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            int Counter1, Counter2, CounterProb1, CounterProb2, FinalMove, ClinchMove;

            Counter1 = Comment.ExtractCounterMove1(fightAttributes.FullComment);
            Counter2 = Comment.ExtractCounterMove2(fightAttributes.FullComment);
            CounterProb1 = GetMoveProb.CounterMoveProb(Act, Counter1);
            CounterProb2 = GetMoveProb.CounterMoveProb(Act, Counter2);

            FinalMove = (CounterProb1 > CounterProb2) ? Counter1 : Counter2;

            Comment.DoComment(Act, Pas, Comment.ReturnComment(ReadTxts.ReadFileToList("Counter")), Pbp, fightAttributes);

            switch (FinalMove)
            {
                case 1:
                    ActPunch(Act, Pas);
                    break;
                case 2:
                    ActKick(Act, Pas);
                    break;
                case 3:
                    if (fightAttributes.InTheClinch)
                    {
                        ClinchMove = GetClinchActions.ClinchAction(Act, Pas);

                        switch (ClinchMove)
                        {
                            case Sim.ACT_DIRTYBOXING:
                                ActPunchClinch(Act, Pas, DIRTY_BOXING);
                                break;
                            case Sim.ACT_THAICLINCH_KNEES:
                                ActKickClinch(Act, Pas, THAI_ATTACK);
                                break;
                            case Sim.ACT_THAICLINCH_PUNCHES:
                                ActPunchClinch(Act, Pas, THAI_ATTACK);
                                break;
                            case Sim.ACT_TAKEDOWNCLINCH:
                                ActClinchTakedown(Act, Pas);
                                break;
                            case Sim.ACT_BREAKCLINCH:
                                ActBreakClinch(Act, Pas);
                                break;
                            default:
                                ActPunchClinch(Act, Pas, -1);
                                break;
                        }
                    }
                    else
                    {
                        ActClinch(Act, Pas);
                    }
                    break;
                case 4:
                    ActTakedown(Act, Pas);
                    break;
                case 5:
                    ActSubmission(Act, Pas);
                    break;
                case 6:
                    ActSubmission(Act, Pas);
                    break;
                case 7:
                    ActSubmission(Act, Pas);
                    break;
                case 8:
                    if (FighterOnTop == GetFighterNumber(Act))
                    {
                        ActGnP(Act, Pas);
                    }
                    else
                    {
                        ActStrikesFromGuard(Act, Pas);
                    }
                    break;
                case 9:
                    ActSubmission(Act, Pas);
                    break;
                case 10:
                    ActPositioning(Act, Pas);
                    break;
                case 11:
                    ActStandKickToGround(Act, Pas);
                    break;
                case 12:
                    ActGroundKicksToStand(Act, Pas);
                    break;
                case 13:
                    ActStrikesFromGuard(Act, Pas);
                    break;
                case 14:
                    ActMoveToGround(Act, Pas);
                    break;
                case 15:
                    ActStandUp(Act, Pas);
                    break;
            }
        }

    }
}

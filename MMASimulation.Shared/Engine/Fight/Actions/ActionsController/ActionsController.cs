using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.ActionsController
{
    public static class ActionsController
    {

        public static void ActionsController(Fighter fighter1, Fighter fighter2, FightAttributes fightAttributes, int timeInc)
        {
            List<Fighter> fighters = [fighter1, fighter2];

            int fighterActive, fighterPassive;
            int fighterAction1, fighterAction2, fighterAction;
            bool tempInTheClinch;
            bool fighter1OnTheGround, fighter2OnTheGround;
            bool f1Ground, f2Ground;

            // Aumenta o tempo no chão  para os lutadores
            if (fighters[0].FighterFightAttributes.OnTheGround)
            {
                StatsUtils.UpdateTimeOnGround(fightAttributes.Statistics, 0, timeInc);
            }
            if (fighters[1].FighterFightAttributes.OnTheGround)
            {
                StatsUtils.UpdateTimeOnGround(fightAttributes.Statistics, 1, timeInc);
            }

            tempInTheClinch = fightAttributes.InTheClinch;
            fighter1OnTheGround = fighters[0].FighterFightAttributes.OnTheGround;
            fighter2OnTheGround = fighters[1].FighterFightAttributes.OnTheGround;


            fighterAction1 = GetFighterActions.FighterAction(fighters[0], fighters[1], fightAttributes);
            fighterAction2 = GetFighterActions.FighterAction(fighters[1], fighters[0], fightAttributes);


            //Verifica  se ambos estão na mesma condição de atordoamento
            if (fighters[0].FighterFightAttributes.Dazed == fighters[1].FighterFightAttributes.Dazed)
            {
                if (!fighters[0].FighterFightAttributes.OnTheGround && !fighters[1].FighterFightAttributes.OnTheGround)
                {
                    fighterActive = GetStandUpInitiative(fighters[0], fighters[1], GetActionBonus(fighterAction1), GetActionBonus(fighterAction2));
                }
                else
                {
                    fighterActive = GetGroundInitiative(fighters[0], fighters[1], GetActionBonus(fighterAction1), GetActionBonus(fighterAction2));
                }
            }
            //Caso contrário, o lutador atordoado concede a iniciativa
            else
            {
                fighterActive = fighters[0].FighterFightAttributes.Dazed ? 1 : 0;
            }

            fighterPassive = (fighterActive == 1) ? 0 : 1;
            fighterAction = (fighterActive == 1) ? fighterAction2 : fighterAction1;


            WriteGuard(fighters[fighterActive], fighters[fighterPassive]);
            MakeColorComments(fighters[fighterActive], fighters[fighterPassive]);

            if (CheckPunchesExchange(fighters[fighterActive], fighters[fighterPassive]))
            {
                fighterAction = Moves.ACT_PUNCHEXCHANGE;
            }

            f1Ground = fighters[fighterActive].FighterFightAttributes.OnTheGround;
            f2Ground = fighters[fighterPassive].FighterFightAttributes.OnTheGround;

            //O lutador ativo realiza a ação
            switch (fightAction(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive)))
            {
                case Moves.ACT_PUNCHES:
                    actPunch(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_KICKS:
                    actKick(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_CLINCH:
                    actClinch(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_TAKEDOWNS:
                    actTakedown(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_DIRTYBOXING:
                    actPunchClinch(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive), DIRTY_BOXING);
                    break;
                case Moves.ACT_TAKEDOWNCLINCH:
                    actClinchTakedown(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_BREAKCLINCH:
                    actBreakClinch(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_GNP:
                    actGnP(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_POSITIONING:
                    actPositioning(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_SUBMISSION:
                    actSubmission(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_STANDUP:
                    actStandUp(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_LNP:
                    actLnP(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_FANCYPUNCH:
                    actFancyPunch(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_FANCYKICK:
                    actFancyKick(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_HEADBUTT:
                    actHeadButt(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_POKE:
                    actPoke(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_REST:
                    actRest(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_GROINKICK:
                    actGroinKick(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_SLAM:
                    actSlam(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_SUPPLEX:
                    actSupplex(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_SOCCERKICKS:
                    actSoccerKicks(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_STOMPS:
                    actStomps(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_STANDKICK:
                    actStandKickToGround(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_MOVETOGROUND:
                    actMoveToGround(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_STRIKESFROMGUARD:
                    actStrikesFromGuard(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_GROUNDKICK:
                    actGroundKicksToStand(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_RESTCLINCH:
                    actRestInClinch(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_NOACTION:
                    actNoAction(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_ALLOWSTAND:
                    actAllowToStand(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_PUNCHEXCHANGE:
                    actPunchesExchange(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_PULLGUARD:
                    actPullGuard(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_GNPELBOWS:
                    actGnPElbows(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_CAPITALIZESTAND:
                    actCapitalizeStanding(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_CAPITALIZEGROUND:
                    actCapitalizeGround(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_KNEESONGROUND:
                    actKneesOnGround(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_FANCYSUB:
                    actFancySubmission(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive));
                    break;
                case Moves.ACT_THAICLINCH_PUNCHES:
                    actPunchClinch(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive), THAI_ATTACK);
                    break;
                case Moves.ACT_THAICLINCH_KNEES:
                    actKickClinch(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive), THAI_ATTACK);
                    break;
                case Moves.ACT_GRAPPLING_PUNCH:
                    actPunchClinch(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive),
                            GRAPPLING_ATTACK);
                    break;
                case Moves.ACT_GRAPPLING_KNEE:
                    actKickClinch(fighterActiveOrPassive(fighterActive), fighterActiveOrPassive(fighterPasive),
                            GRAPPLING_ATTACK);
                    break;
            }

            //Métodos extras
            if (!fightAttributes.BoutFinished)
            {
                RefStandfighters(fighters[fighterActive], fighters[fighterPassive]);
                ActKeepClinch(fighters[fighterActive], fighters[fighterPassive]);
                MakeStaggeredComment(fighters[fighterActive], fighters[fighterPassive]);
                MakeStandUpComment(fighters[fighterActive], fighters[fighterPassive], !f1Ground, !f2Ground);
            }

            //Cansaço dos lutadores
            ProcessStaminaLoss(fighters[fighterActive], true);
            ProcessStaminaLoss(fighters[fighterPassive], false);


            //Recupera do estado atordoado
            RecoverForDazed(fighters[fighterActive]);
            RecoverForDazed(fighters[fighterPassive]);

            //Atualização de status
            if (tempInTheClinch && !fightAttributes.InTheClinch)
            {
                fightAttributes.Statistics[0].TempDamageClinch = 0;
                fightAttributes.Statistics[1].TempDamageClinch = 0;
            }

            if (fighter1OnTheGround && !fighters[0].FighterFightAttributes.OnTheGround)
            {
                fightAttributes.Statistics[0].TempDamageGround = 0;
            }

            if (fighter2OnTheGround && !fighters[1].FighterFightAttributes.OnTheGround)
            {
                fightAttributes.Statistics[1].TempDamageGround = 0;
            }

            if (!fighters[fighterActive].FighterFightAttributes.OnTheGround)
            {
                fighters[fighterActive].FighterStyles.Stalling = 0;
                fighters[fighterActive].FighterFightAttributes.RoundsInTheGround = 0;
            }
            else
            {
                fighters[fighterActive].FighterFightAttributes.RoundsInTheGround += 1;
            }

            if (!fighters[fighterPassive].FighterFightAttributes.OnTheGround)
            {
                fighters[fighterPassive].FighterStyles.Stalling = 0;
                fighters[fighterPassive].FighterFightAttributes.RoundsInTheGround = 0;
            }
            else
            {
                fighters[fighterPassive].FighterFightAttributes.RoundsInTheGround += 1;
            }

            CheckFightPerformance(fighters[fighterActive], fighters[fighterPassive]);
            CheckFightPerformance(fighters[fighterPassive], fighters[fighterActive]);
            CheckPainAndTiredness(fighters[fighterActive]);
            CheckPainAndTiredness(fighters[fighterPassive]);
            CheckMoral(fighters[fighterActive]);
            CheckMoral(fighters[fighterPassive]);

            if (!fightAttributes.BoutFinished)
            {
                ProcessTowelThrow(fighters[fighterPassive], fighters[fighterActive]);
                RefRestartCentreRing(fighters[fighterPassive], fighters[fighterActive]);
            }

            UpdateHPandStamina();
            UpdatePerformance();
            UpdateMoral();
        }






    }
}

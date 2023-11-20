using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.Fight.Actions.Ground
{
    public static class GetGroundActions
    {

        public static int GroundAction(Fighter act, Fighter pas, FightAttributes fightAttributes)
        {
            List<Fighter> fighters = [act, pas];

            int prob = RandomUtils.GetRandomValue(100) + 1;
            int gnPProb = act.FighterStrategies.StratGNP;

            int subProb = GetSubmissionAvailable(act) ? gnPProb + GetSubmissionProbByPosition(act) : 0;

            //Se o lutador estiver em Full Guard ou Rear Mount, ele para de posicionar
            int posProb = (act.FullName == fighters[fightAttributes.FighterOnTop].FullName && (fightAttributes.GuardType == 0 || fightAttributes.GuardType == 1)) ? 0 : subProb + act.FighterStrategies.StratPositioning;
            int lnPProb = posProb + act.FighterStrategies.StratLNP;

            //O lutador só pode ficar em pé na Guarda Aberta ou no Controle Lateral ou se estiver por cima e na guarda aberta
            int standProb = (((fightAttributes.GuardType == 3 || fightAttributes.GuardType == 4) ||
            (act.FullName == fighters[fightAttributes.FighterOnTop].FullName &&
            (fightAttributes.GuardType == 2 || fightAttributes.GuardType == 4))) &&
            act.FighterFightAttributes.RoundsInTheGround <= 0) ? lnPProb + act.FighterStrategies.StratStandUp : 0;

            posProb = (standProb > 0) ? posProb : posProb + act.FighterStrategies.StratStandUp;
            lnPProb = (standProb > 0) ? lnPProb : lnPProb + act.FighterStrategies.StratLNP;

            int result;
            if (prob <= gnPProb)
            {
                result = Moves.ACT_GNP;
            }
            else if (prob <= subProb)
            {
                result = Moves.ACT_SUBMISSION;
            }
            else if (prob <= posProb)
            {
                result = Moves.ACT_POSITIONING;
            }
            else if (prob <= lnPProb)
            {
                result = Moves.ACT_LNP;
            }
            else if (prob <= standProb && standProb > 0)
            {
                result = Moves.ACT_STANDUP;
            }
            else
            {
                result = Moves.ACT_POSITIONING;
            }

            //Sub diferentes
            if (act.FighterStyles.FancySubmissions > 0 && result == Moves.ACT_SUBMISSION)
            {
                if (RandomUtils.GetRandom() < act.FighterRatings.Agility && RandomUtils.GetRandom() < act.FighterRatings.Submission &&
                    RandomUtils.GetRandom() < act.FighterStyles.FancySubmissions * Sim.FANCYMOVEPROB)
                {
                    result = Moves.ACT_FANCYSUB;
                }
            }


            //Joelhos no chão
            if (result == Moves.ACT_GNP && act.FullName == fighters[fightAttributes.FighterOnTop].FullName
                && (fightAttributes.GuardType == 2 || fightAttributes.GuardType == 3
                    || fightAttributes.GuardType == 7 || fightAttributes.GuardType == 8)
                && act.FighterStyles.UseKneesGround)
            {
                if (RandomUtils.FixedRandomInt(act.FighterRatings.Aggressiveness) + Sim.KNEESFREQUENCY > 20)
                {
                    result = Moves.ACT_KNEESONGROUND;
                }
            }


            // Dirty fighting
            if (act.FighterStrategies.CheckDirtyMove)
            {
                result = Moves.ACT_POKE;
            }

            if (act.CheckDirtyMove())
            {
                result = Moves.ACT_HEADBUTT;
            }

            // Controls the fighters go to ground and standing in a loop
            if (act.ActionsInGround > 0 && act.ActionsInGround < ApplicationUtils.MINACTIONSFORSWITCHING &&
                result == Moves.ACT_STANDUP)
            {
                result = Moves.ACT_LNP;
                if (act.ActionsInGround >= ApplicationUtils.MINACTIONSFORSWITCHING)
                {
                    act.ActionsInGround = -1;
                }
            }

            act.ActionsInGround++;

            // If the fighter is not on top, he can't GnP
            if (act.FullName != fighters[FighterOnTop].FullName && result == Moves.ACT_GNP)
            {
                result = Moves.ACT_STRIKESFROMGUARD;
            }

            if (result == Moves.ACT_GNP && act.UseElbows && Organization.Elbows)
            {
                if (ApplicationUtils.GetRandom() < act.getAggressiveness)
                {
                    result = Moves.ACT_GNPELBOWS;
                }
            }


            // Substitui o plano de jogo padrão se ele estiver recebendo muito dano
            if (Bout.Statistics[GetFighterNumber(act)].TempDamageGround >
                act.getToughness * ApplicationUtils.MAXDAMAGEFORCHANGINGGAMEPLAN)
            {
                if (ApplicationUtils.GetRandom() < act.getControl)
                {
                    result = Moves.ACT_STANDUP;
                }
            }


            // Lutadores agressivos tentarão capitalizar
            if (pas.Dazed && fixedRandomInt(act.getAggressiveness) > ApplicationUtils.CAPITALIZEPROB)
            {
                result = Moves.ACT_CAPITALIZEGROUND;
            }

            act.ActionsInGround = ApplicationUtils.SetLimits(act.ActionsInGround - 1,
                ApplicationUtils.MINSROUNDSINTHEGROUND, 0);

            return result;
        }

    }
}

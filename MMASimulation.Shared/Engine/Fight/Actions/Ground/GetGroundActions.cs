using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
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

            int subProb = SubmissionsUtils.GetSubmissionAvailable(act, fightAttributes) ? gnPProb + SubmissionsUtils.GetSubmissionProbByPosition(act, fightAttributes) : 0;

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

            // Luta suja
            if (act.FighterFightAttributes.CheckDirtyMove(act.FighterRatings.Aggressiveness, act.FighterStyles.DirtyFighting))
            {
                result = Moves.ACT_POKE;
            }

            if (act.FighterFightAttributes.CheckDirtyMove(act.FighterRatings.Aggressiveness, act.FighterStyles.DirtyFighting))
            {
                result = Moves.ACT_HEADBUTT;
            }

            // Controla os lutadores que vão para o chão e ficam em loop
            if (act.FighterFightAttributes.ActionsInGround > 0 && act.FighterFightAttributes.ActionsInGround < Sim.MINACTIONSFORSWITCHING &&
                result == Moves.ACT_STANDUP)
            {
                result = Moves.ACT_LNP;
                if (act.FighterFightAttributes.ActionsInGround >= Sim.MINACTIONSFORSWITCHING)
                {
                    act.FighterFightAttributes.ActionsInGround = -1;
                }
            }

            act.FighterFightAttributes.ActionsInGround++;

            // Se o lutador não estiver por cima, ele não pode fazer GnP
            if (act.FullName != fighters[fightAttributes.FighterOnTop].FullName && result == Moves.ACT_GNP)
            {
                result = Moves.ACT_STRIKESFROMGUARD;
            }

            if (result == Moves.ACT_GNP && act.FighterFightAttributes.UseElbows)
            {
                if (RandomUtils.GetRandom() < act.FighterRatings.Aggressiveness)
                {
                    result = Moves.ACT_GNPELBOWS;
                }
            }

            // Substitui o plano de jogo padrão se ele estiver recebendo muito dano
            if (fightAttributes.Statistics[GetFighterActions.GetFighterNumber(act, fightAttributes)].TempDamageGround >
                act.FighterRatings.Toughness * Sim.MAXDAMAGEFORCHANGINGGAMEPLAN)
            {
                if (RandomUtils.GetRandom() < act.FighterRatings.Control)
                {
                    result = Moves.ACT_STANDUP;
                }
            }

            // Lutadores agressivos tentarão capitalizar
            if (pas.FighterFightAttributes.Dazed && RandomUtils.FixedRandomInt(act.FighterRatings.Aggressiveness) > Sim.CAPITALIZEPROB)
            {
                result = Moves.ACT_CAPITALIZEGROUND;
            }

            act.FighterFightAttributes.ActionsInGround = RandomUtils.SetLimits(act.FighterFightAttributes.ActionsInGround - 1,
                Sim.MINSROUNDSINTHEGROUND, 0);

            return result;
        }

        public static int GroundToStandAction(Fighter act, Fighter pas)
        {
            int standUpProb = RandomUtils.FixedRandomInt(act.FighterStrategies.StratStandUp);
            int kickProb = RandomUtils.FixedRandomInt(act.FighterStrategies.StratKicking / 2);

            if (standUpProb > kickProb)
            {
                return Sim.ACT_STANDUP;
            }
            else
            {
                return Sim.ACT_GROUNDKICK;
            }
        }

    }
}

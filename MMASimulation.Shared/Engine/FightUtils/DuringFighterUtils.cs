using MMASimulation.Shared.Engine.Comments.ReadTxt;
using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Engine.Fight.Actions.ActionsController;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;
using MMASimulation.Shared.Models.Utils;

namespace MMASimulation.Shared.Engine.FightUtils
{
    public static class DuringFighterUtils
    {

        public static int GetHurtFactor(Fighter act)
        {
            int result = 0;
            double hurtFactor = act.FighterFightAttributes.CurrentHP / act.FighterRatings.Toughness;

            switch (Convert.ToInt32(Math.Round(hurtFactor * 10)))
            {
                case int n when n >= -99 && n <= 10:
                    result = 10;
                    break;
                case int n when n >= 11 && n <= 15:
                    result = 5;
                    break;
                case int n when n >= 16 && n <= 20:
                    result = 4;
                    break;
                case int n when n >= 21 && n <= 25:
                    result = 3;
                    break;
                case int n when n >= 26 && n <= 30:
                    result = 2;
                    break;
                case int n when n >= 31 && n <= 45:
                    result = 1;
                    break;
                case int n when n >= 46 && n <= 99:
                    result = 0;
                    break;
            }

            return result * Sim.HURTFACTOR;
        }

        public static int GetGasTankFactor(Fighter act, double Value)
        {
            double reducingFactor = act.FighterFightAttributes.CurrentStamina * Sim.FATIGUECUT / (act.FighterRatings.Conditioning * 5);
            return (int)Math.Round(Value * reducingFactor);
        }

        public static int GetPercentage(double max, double actual)
        {
            int result = 0;

            if (max > 0)
            {
                double aux = 100.0 * actual / max;
                result = (int)Math.Round(aux);
            }

            return result;
        }

        public static int GetTotalTime(FightAttributes fightAttributes)
        {
            int result;

            if (fightAttributes.CurrentRound > 1)
            {
                result = 5 * 60;

                if (fightAttributes.CurrentRound > 2)
                {
                    result += (fightAttributes.CurrentRound - 1) * 5 + fightAttributes.CurrentTime;
                }
                else
                {
                    result += fightAttributes.CurrentTime;
                }
            }
            else
            {
                result = fightAttributes.CurrentTime;
            }

            return result;
        }

        public static double GetFightAction(Fighter fighter1, Fighter fighter2, FightAttributes attributes)
        {

            return (fighter1.FighterFightAttributes.TotalPoints() + fighter2.FighterFightAttributes.TotalPoints() / (GetTotalTime(attributes) + 1));
        }

        public static void RefStandFighters(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {

            List<Fighter> fighters = [act, pas];

            if (fightAttributes.BoutFinished)
            {
                return;
            }

            // Levante-se dos lutadores quando eles estiverem parados no chão
            if (GetFighterOnGround(act, pas) == 2)
            {

                if (5 < act.FighterStyles.Stalling + pas.FighterStyles.Stalling)
                {
                    act.FighterFightAttributes.OnTheGround = false;
                    pas.FighterFightAttributes.OnTheGround = false;
                    act.FighterStyles.Stalling = 0;
                    pas.FighterStyles.Stalling = 0;

                    Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("RefStandUp")), Pbp, fightAttributes);
                }
            }

            // Levante-se dos lutadores quando um deles estiver no chão
            else if (GetFighterOnGround(act, pas) >= 0 && GetFighterOnGround(act, pas) <= 1)
            {

                if (fighters[GetFighterOnGround(act, pas)].FighterFightAttributes.RoundsInTheGround > 1)
                {
                    if (5 + RandomUtils.GeSmallRandom()
                        < (fighters[GetFighterOnGround(act, pas)].FighterFightAttributes.RoundsInTheGround) * Sim.REFTENDENCYTOSTANDUP)
                    {
                        act.FighterFightAttributes.OnTheGround = false;
                        pas.FighterFightAttributes.OnTheGround = false;
                        act.FighterStyles.Stalling = 0;
                        pas.FighterStyles.Stalling = 0;
                        fighters[0].FighterFightAttributes.RoundsInTheGround = 0;
                        fighters[1].FighterFightAttributes.RoundsInTheGround = 0;

                        if (act.FighterFightAttributes.OnTheGround)
                        {
                            Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("RefStandUpOneFighter")), Pbp, fightAttributes);

                        }
                        else
                        {
                            Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("RefStandUpOtherFighter")), Pbp, fightAttributes);

                        }
                    }
                }
            }
        }

        public static int GetFighterOnGround(Fighter fighter1, Fighter fighter2)
        {
            if (fighter1.FighterFightAttributes.OnTheGround && fighter2.FighterFightAttributes.OnTheGround)
            {
                return 2;
            }
            else if (!fighter1.FighterFightAttributes.OnTheGround && fighter2.FighterFightAttributes.OnTheGround)
            {
                return 1;
            }
            else if (fighter1.FighterFightAttributes.OnTheGround && !fighter2.FighterFightAttributes.OnTheGround)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public static int GetAttackLevel(Fighter act, Fighter pas, double AtSkill, double PasSkill, FightAttributes fightAttributes)
        {
            const int DAZED_BONUS = 10;
            double At, Def;

            int result = 1;

            //Valor de ataque
            At = RandomUtils.FixedRandomInt(act.FighterRatings.Aggressiveness);
            At += RandomUtils.FixedRandomInt(act.FighterRatings.Control / 2);
            At += RandomUtils.FixedRandomInt(act.FighterRatings.Conditioning / 2);
            At += RandomUtils.GeSmallRandom() + (act.FighterRatings.AttackBonus(act.FighterFightAttributes));
            At += RandomUtils.FixedRandomInt(act.FighterRatings.Agility / 2);
            At += RandomUtils.FixedRandomInt(AtSkill);

            //Valor de defesa
            Def = RandomUtils.FixedRandomInt(pas.FighterRatings.Control);
            Def += RandomUtils.FixedRandomInt(pas.FighterRatings.Conditioning / 2);
            Def -= RandomUtils.FixedRandomInt(pas.FighterRatings.Aggressiveness / 2);
            Def += RandomUtils.GeSmallRandom() + (pas.FighterRatings.DefenseBonus(pas.FighterFightAttributes));
            Def += RandomUtils.FixedRandomInt(act.FighterRatings.Agility / 2);
            Def += RandomUtils.FixedRandomInt(PasSkill);

            if (pas.FighterFightAttributes.Dazed)
            {
                At += DAZED_BONUS;
            }

            switch ((int)Math.Round(At - Def))
            {
                case int n when n >= 0 && n <= 5:
                    result = 1;
                    break;
                case int n when n >= 6 && n <= 18:
                    result = 2;
                    break;
                case int n when n >= 19 && n <= 99:
                    result = 3;
                    break;
                default:
                    result = 1;
                    break;
            }

            // Skill limite
            if ((result == 2) && (AtSkill < Sim.LEVEL2SKILL))
            {
                result = 1;
            }
            else if ((result == 3) && (AtSkill < Sim.LEVEL3SKILL))
            {
                result = 2;
            }

            act.FighterFightAttributes.IncreasePoints(fightAttributes.CurrentRound, (result * Sim.ATTACKLEVELPOINTS));

            return result;
        }

        public static void DamageFighter(Fighter act, Fighter pas, double DamageDone, FightAttributes fightAttributes, Statistics[] Statistics)
        {

            if (DamageDone < 0)
            {
                DamageDone = 1;
            }

            //Aumenta as estatísticas
            StatsUtils.UpdateDamageDone(Statistics, GetFighterActions.GetFighterNumber(act, fightAttributes), DamageDone, fightAttributes.InTheClinch, act.FighterFightAttributes.OnTheGround);
            StatsUtils.UpdateDamageReceived(Statistics, GetFighterActions.GetFighterNumber(pas, fightAttributes), DamageDone, fightAttributes.InTheClinch, pas.FighterFightAttributes.OnTheGround);

            DamageDone = DamageDone;
            pas.FighterFightAttributes.CurrentHP -= DamageDone / Sim.DAMAGECUT;

            if (pas.FighterFightAttributes.CurrentHP < 0)
            {
                pas.FighterFightAttributes.CurrentHP = 1;
            }

            act.FighterFightAttributes.IncreasePoints(fightAttributes.CurrentRound, (int)(DamageDone / Sim.DAMAGECUTPOINTS));
        }

        public static double UpsetSystem(Fighter act, Fighter pas, double value, FightAttributes fightAttributes)
        {
            const int UPSET_POWER = 100;

            if (act.FighterRatings.Ranking() < pas.FighterRatings.Ranking() &&
                (RandomUtils.GetRandomValue(1000) <= Sim.UPSET_FREQUENCY + fightAttributes.Randomness))
            {
                return value * UPSET_POWER;
            }
            else
            {
                return value;
            }
        }

        public static void ProcessKO(Fighter act, Fighter pas, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {

            if (fightAttributes.BoutFinished)
            {
                return;
            }

            // KO
            if (fightAttributes.FinishMode == Sim.RES_KO)
            {
                if (fightAttributes.HitLocation <= 8)
                {
                    fightAttributes.FinishedType = ReadTxts.ReadListToComment("Misc", Sim.KO);
                    fightAttributes.FinishMode = Sim.RES_KO;


                    // Isso é necessário para estruturas de comentários incomuns, como troca de socos
                    if (fightAttributes.FinishedDescription == string.Empty)
                    {
                        fightAttributes.FinishedDescription = Comment.ExtractMoveName(fightAttributes.FullComment);
                    }

                    if (pas.FighterFightAttributes.OnTheGround)
                    {
                        Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("GroundKO")), Pbp, fightAttributes);
                    }
                    else
                    {
                        Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("StandingKO")), Pbp, fightAttributes);
                    }

                    fightAttributes.BoutFinished = true;
                    fightAttributes.FighterWinner = GetFighterActions.GetFighterNumber(act, fightAttributes);
                }
                else
                {
                    pas.FighterFightAttributes.Dazed = true;
                }
            }
            // TKO
            else
            {
                fightAttributes.BoutFinished = true;
                fightAttributes.FinishedType = ReadTxts.ReadListToComment("Misc", Sim.TKO);
                fightAttributes.FinishMode = Sim.RES_TKO;
                fightAttributes.FinishedDescription = Comment.ExtractMoveName(fightAttributes.FullComment);
                Comment.DoComment(act, pas, Comment.ReturnComment(ReadTxts.ReadFileToList("TKORef")), Pbp, fightAttributes);
                fightAttributes.BoutFinished = true;
                fightAttributes.FighterWinner = GetFighterActions.GetFighterNumber(act, fightAttributes);
            }
        }

        public static void ProcessInjury(Fighter act, Fighter pas, int injuryType, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            bool finishedByInj = false;
            fightAttributes.HitLocation = Comment.ExtractHitLocation(fightAttributes.FullComment);
            string injuryComment = string.Empty;

            if (injuryType == Sim.BIGINJURYORCUTTRUE)
            {
                // Impede que uma luta finalizada seja alterada de KO para vitória por lesão
                if (!fightAttributes.BoutFinished)
                {
                    fightAttributes.BoutFinished = true;
                    fightAttributes.FinishedType = ReadTxts.ReadListToComment("Misc", Sim.INJ);
                    fightAttributes.FinishMode = Sim.RES_INJURY;
                    fightAttributes.FighterWinner = GetFighterActions.GetFighterNumber(act, fightAttributes);
                    finishedByInj = true;
                }

                // Comentário sobre lesão

                switch (fightAttributes.HitLocation)
                {
                    case int n when (n >= 0 && n <= 8):
                        injuryComment = Comment.ReturnComment(ReadTxts.ReadFileToList("FaceInjuries1"));
                        Comment.DoComment(act, pas, Comment.ExtractInjuryCutComment(injuryComment), Pbp, fightAttributes);
                        pas.FighterFightAttributes.AddInjuryToList(Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes));
                        pas.FighterFightAttributes.FaceInjury += 4;
                        break;
                    case int n when (n >= 9 && n <= 12):
                        injuryComment = Comment.ReturnComment(ReadTxts.ReadFileToList("BodyInjuries1"));
                        Comment.DoComment(act, pas, Comment.ExtractInjuryCutComment(injuryComment), Pbp, fightAttributes);
                        pas.FighterFightAttributes.AddInjuryToList(Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes));
                        if (n >= 9 && n <= 10)
                        {
                            pas.FighterFightAttributes.TorsoInjury += 4;
                        }
                        else
                        {
                            pas.FighterFightAttributes.BackInjury += 4;
                        }
                        break;
                    case int n when (n >= 13 && n <= 14):
                        injuryComment = Comment.ReturnComment(ReadTxts.ReadFileToList("ArmInjuries1"));
                        Comment.DoComment(act, pas, Comment.ExtractInjuryCutComment(injuryComment), Pbp, fightAttributes);
                        pas.FighterFightAttributes.AddInjuryToList(Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes));
                        pas.FighterFightAttributes.LeftArmInjury += (n == 13) ? 4 : 4;
                        break;
                    case int n when (n >= 15 && n <= 20):
                        injuryComment = Comment.ReturnComment(ReadTxts.ReadFileToList("LegInjuries1"));
                        Comment.DoComment(act, pas, Comment.ExtractInjuryCutComment(injuryComment), Pbp, fightAttributes);
                        pas.FighterFightAttributes.AddInjuryToList(Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes));
                        pas.FighterFightAttributes.LeftLegInjury += (n == 15 || n == 17 || n == 19) ? 4 : 4;
                        break;
                }

                if (finishedByInj)
                {
                    fightAttributes.FinishedDescription = Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes);
                }
            }
            else
            {

                switch (fightAttributes.HitLocation)
                {
                    case int n when (n >= 0 && n <= 8):
                        injuryComment = Comment.ReturnComment(ReadTxts.ReadFileToList("FaceInjuries0"));
                        Comment.DoComment(act, pas, Comment.ExtractInjuryCutComment(injuryComment), Pbp, fightAttributes);
                        pas.FighterFightAttributes.ControlMod -= 0.5;
                        pas.FighterFightAttributes.Moral -= 0.5;
                        pas.FighterFightAttributes.AddInjuryToList(Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes));
                        pas.FighterFightAttributes.FaceInjury += 1;
                        break;
                    case int n when (n >= 9 && n <= 12):
                        injuryComment = Comment.ReturnComment(ReadTxts.ReadFileToList("BodyInjuries0"));
                        Comment.DoComment(act, pas, Comment.ExtractInjuryCutComment(injuryComment), Pbp, fightAttributes);
                        pas.FighterFightAttributes.AgilityMod -= 0.5;
                        pas.FighterFightAttributes.StrengthMod -= 0.5;
                        pas.FighterFightAttributes.DodgingMod -= 0.5;
                        pas.FighterFightAttributes.AddInjuryToList(Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes));
                        pas.FighterFightAttributes.TorsoInjury += (n >= 9 && n <= 10) ? 1 : 1;
                        break;
                    case int n when (n >= 13 && n <= 14):
                        injuryComment = Comment.ReturnComment(ReadTxts.ReadFileToList("ArmInjuries0"));
                        Comment.DoComment(act, pas, Comment.ExtractInjuryCutComment(injuryComment), Pbp, fightAttributes);
                        pas.FighterFightAttributes.PunchingMod -= 0.5;
                        pas.FighterFightAttributes.StrengthMod -= 0.5;
                        pas.FighterFightAttributes.AddInjuryToList(Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes));
                        pas.FighterFightAttributes.LeftArmInjury += (n == 13) ? 1 : 1;
                        break;
                    case int n when (n >= 15 && n <= 20):
                        injuryComment = Comment.ReturnComment(ReadTxts.ReadFileToList("LegInjuries0"));
                        Comment.DoComment(act, pas, Comment.ExtractInjuryCutComment(injuryComment), Pbp, fightAttributes);
                        pas.FighterFightAttributes.KickingMod -= 0.5;
                        pas.FighterFightAttributes.AgilityMod -= 0.5;
                        pas.FighterFightAttributes.AddInjuryToList(Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes));
                        pas.FighterFightAttributes.LeftLegInjury += (n == 15 || n == 17 || n == 19) ? 1 : 1;
                        break;
                }
            }
        }

        public static void ProcessCut(Fighter act, Fighter pas, int cutType, List<FightPBP> Pbp, FightAttributes fightAttributes)
        {
            bool finishedByCut = false;
            string injuryComment = string.Empty;

            if (!(fightAttributes.HitLocation >= 0 && fightAttributes.HitLocation <= 8))
            {
                return;
            }

            fightAttributes.HitLocation = Comment.ExtractHitLocation(fightAttributes.FullComment);

            if (cutType == Sim.BIGINJURYORCUTTRUE)
            {

                //Impede uma mudança de luta finalizada de KO para Cut
                if (!fightAttributes.BoutFinished)
                {
                    fightAttributes.BoutFinished = true;
                    fightAttributes.FinishedType = ReadTxts.ReadListToComment("Misc", Sim.INJ);
                    fightAttributes.FinishMode = Sim.RES_INJURY;
                    fightAttributes.FighterWinner = GetFighterActions.GetFighterNumber(act, fightAttributes);
                    finishedByCut = true;
                }


                //Comentário sobre lesão
                switch (fightAttributes.HitLocation)
                {
                    case int n when (n >= 0 && n <= 8):
                        {
                            injuryComment = Comment.ReturnComment(ReadTxts.ReadFileToList("FaceCut0"));
                            Comment.DoComment(act, pas, Comment.ExtractInjuryCutComment(injuryComment), Pbp, fightAttributes);
                            pas.FighterFightAttributes.AddInjuryToList(Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes));
                            pas.FighterFightAttributes.Cuts += 4;
                            if (finishedByCut)
                            {
                                fightAttributes.FinishedDescription = Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes);
                            }
                            break;
                        }
                }
            }
            else
            {
                switch (fightAttributes.HitLocation)
                {
                    case int n when (n >= 0 && n <= 8):
                        {
                            injuryComment = Comment.ReturnComment(ReadTxts.ReadFileToList("FaceCut1"));
                            Comment.DoComment(act, pas, Comment.ExtractInjuryCutComment(injuryComment), Pbp, fightAttributes);
                            pas.FighterFightAttributes.ControlMod -= 0.5;
                            pas.FighterFightAttributes.Moral -= 0.5;
                            pas.FighterFightAttributes.AddInjuryToList(Comment.ReplaceTokens(act, pas, Comment.ExtractInjuryCutName(injuryComment), fightAttributes));
                            pas.FighterFightAttributes.Cuts += 1;
                            break;
                        }
                }
            }
        }


    }
}

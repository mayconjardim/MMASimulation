using MMASimulation.Shared.Engine.Constants;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Shared.Engine.FightUtils
{
    public static class PrepareFights
    {

        public static void PrepareFight(Fighter fighter1, Fighter fighter2, int rounds)
        {

            fighter1.FighterFightAttributes = new FighterFightAttributes();
            fighter2.FighterFightAttributes = new FighterFightAttributes();

            fighter1.FighterFightAttributes.OnTheGround = false;
            fighter2.FighterFightAttributes.OnTheGround = false;

            fighter1.FighterFightAttributes.Rush = 0;
            fighter2.FighterFightAttributes.Rush = 0;

            fighter1.FighterFightAttributes.ClearRoundPoints(rounds);
            fighter2.FighterFightAttributes.ClearRoundPoints(rounds);

            UpdateFighterStyle(fighter1);
            UpdateFighterStyle(fighter2);

            CheckWeightDifference(fighter1, fighter2);

            MaxHPandStamina(fighter1);
            MaxHPandStamina(fighter2);
        }

        public static void PrepareRound(Fighter fighter1, Fighter fighter2)
        {
            fighter1.FighterFightAttributes.OnTheGround = false;
            fighter2.FighterFightAttributes.OnTheGround = false;
        }


        public static void UpdateFighterStyle(Fighter fighter)
        {
            fighter.FighterFightAttributes.StaminaLoss = Sim.DEFAULTFIGHTERSTAMINALOSS;
            fighter.FighterFightAttributes.InitMod = Sim.DEFAULTINITMOD;
            fighter.FighterFightAttributes.Defense = Sim.DEFAULTDEFLEVEL;
            fighter.FighterFightAttributes.DamageMod = Sim.DEFAULTDAMAGECUT;
            fighter.FighterFightAttributes.Accuracy = Sim.DEFAULTACCURACY;
            fighter.FighterFightAttributes.AggPower = Sim.DEFAULTAGGPOWER;

            switch (fighter.FighterStyles.FightingStyle)
            {
                case 0:
                    fighter.FighterFightAttributes.StaminaLoss = Sim.DEFLEVEL2STAMINALOSS;
                    fighter.FighterFightAttributes.InitMod = Sim.DEFLEVEL2INITMOD;
                    fighter.FighterFightAttributes.Defense = Sim.DEFLEVEL2DEFLEVEL;
                    break;
                case 1:
                    fighter.FighterFightAttributes.StaminaLoss = Sim.DEFLEVEL1STAMINALOSS;
                    fighter.FighterFightAttributes.InitMod = Sim.DEFLEVEL1INITMOD;
                    fighter.FighterFightAttributes.Defense = Sim.DEFLEVEL1DEFLEVEL;
                    break;
                case 2:
                    break;
                case 3:
                    fighter.FighterFightAttributes.StaminaLoss = Sim.AGGLEVEL1STAMINALOSS;
                    fighter.FighterFightAttributes.InitMod = Sim.PWRLEVEL1INITMOD;
                    fighter.FighterFightAttributes.Defense = Sim.AGGLEVEL1DEFLEVEL;
                    fighter.FighterFightAttributes.AggPower = Sim.AGGLEVEL1DAMAGEMOD;
                    break;
                case 4:
                    fighter.FighterFightAttributes.StaminaLoss = Sim.AGGLEVEL2STAMINALOSS;
                    fighter.FighterFightAttributes.InitMod = Sim.PWRLEVEL2INITMOD;
                    fighter.FighterFightAttributes.Defense = Sim.AGGLEVEL2DEFLEVEL;
                    fighter.FighterFightAttributes.AggPower = Sim.AGGLEVEL2DAMAGEMOD;
                    break;
            }

            switch (fighter.FighterStyles.TacticalStyle)
            {
                case 0:
                    fighter.FighterFightAttributes.DamageMod = Sim.PWRLEVEL2DAMCUT;
                    fighter.FighterFightAttributes.InitMod = +Sim.PWRLEVEL2INITMOD;
                    fighter.FighterFightAttributes.Accuracy = Sim.PWRLEVEL2ACCURACY;
                    break;
                case 1:
                    fighter.FighterFightAttributes.DamageMod = Sim.PWRLEVEL1DAMCUT;
                    fighter.FighterFightAttributes.InitMod += Sim.PWRLEVEL1INITMOD;
                    fighter.FighterFightAttributes.Accuracy = Sim.PWRLEVEL1ACCURACY;
                    break;
                case 2:
                    break;
                case 3:
                    fighter.FighterFightAttributes.DamageMod = Sim.TECHLEVEL1DAMCUT;
                    fighter.FighterFightAttributes.InitMod += Sim.TECHLEVEL1INITMOD;
                    fighter.FighterFightAttributes.Accuracy = Sim.TECHLEVEL1ACCURACY;
                    break;
                case 4:
                    fighter.FighterFightAttributes.DamageMod = Sim.TECHLEVEL2DAMCUT;
                    fighter.FighterFightAttributes.InitMod += Sim.TECHLEVEL2INITMOD;
                    fighter.FighterFightAttributes.Accuracy = Sim.TECHLEVEL2ACCURACY;
                    break;
            }
        }

        public static void CheckWeightDifference(Fighter fighter1, Fighter fighter2)
        {
            double WEIGHT_MOD = 1.5;
            double weightDifference;

            if (fighter1.WeightClass != fighter2.WeightClass)
            {

                weightDifference = ((int)fighter1.WeightClass - (int)fighter2.WeightClass) * WEIGHT_MOD;

                fighter1.FighterFightAttributes.PunchingMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter1.FighterFightAttributes.KickingMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter1.FighterFightAttributes.ClinchStrikingMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter1.FighterFightAttributes.ClinchGrapplingMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter1.FighterFightAttributes.StrengthMod += Sim.STRENGTHONEWEIGHTCLASSDIFERENCE * weightDifference;
                fighter1.FighterFightAttributes.TakedownsMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter1.FighterFightAttributes.TakedownsDefMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter1.FighterFightAttributes.ControlMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;

                weightDifference = ((int)fighter2.WeightClass - (int)fighter1.WeightClass) * WEIGHT_MOD;

                fighter2.FighterFightAttributes.PunchingMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter2.FighterFightAttributes.KickingMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter2.FighterFightAttributes.ClinchStrikingMod += +Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter2.FighterFightAttributes.ClinchGrapplingMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter2.FighterFightAttributes.StrengthMod += Sim.STRENGTHONEWEIGHTCLASSDIFERENCE * weightDifference;
                fighter2.FighterFightAttributes.TakedownsMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter2.FighterFightAttributes.TakedownsDefMod += +Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                fighter2.FighterFightAttributes.ControlMod += +Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;

                if ((int)fighter2.WeightClass < (int)fighter1.WeightClass)
                {
                    fighter2.FighterFightAttributes.AgilityMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                }
                else if ((int)fighter1.WeightClass < (int)fighter2.WeightClass)
                {
                    fighter1.FighterFightAttributes.AgilityMod += Sim.ONEWEIGHTCLASSDIFFERENCE * weightDifference;
                }
            }
        }

        public static void MaxHPandStamina(Fighter fighter)
        {
            fighter.FighterFightAttributes.CurrentHP = ((fighter.FighterRatings.Toughness * 5 * 100) / 100);
            fighter.FighterFightAttributes.CurrentStamina = ((fighter.FighterRatings.Conditioning * 5 * 100) / 100);
        }

        public static void MakeTempFighters(FightAttributes fightAttributes, Fighter fighter1, Fighter fighter2)
        {
            fightAttributes.TempFigher1 = fighter1;
            fightAttributes.TempFigher2 = fighter2;
        }

    }
}

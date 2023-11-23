namespace MMASimulation.Shared.Engine.Constants
{
    public static class Sim
    {

        public const int BIGRANDOM = 20;
        public const int SMALLRANDOM = 10;
        public const int DAMAGECUT = 25;
        public const int KOPROBCUT = 40;
        public const double KOFREQUENCY = 1.1;
        public const int TKORUSHMINIMUN = 2;
        public const int TKOMINHITPOINTS = 30;
        public const int TKOFREQUENCY = 9;
        public const int SUBDIFFICULT = 22;
        public const double SUBDEFENSECUT = 1.5;
        public const int SUBMALUS = 9;
        public const int REFSUBFREQUENCY = 9;
        public const double NINECMDIFFERENCE = 0.75;
        public const double ONEWEIGHTCLASSDIFFERENCE = 0.75;
        public const int STRENGTHONEWEIGHTCLASSDIFERENCE = 1;
        public const int LEVEL2SKILL = 12;
        public const int LEVEL3SKILL = 15;
        public const int COUNTERATTACKCUT = 2;
        public const int FATIGUECUT = 1;
        public const int TIMEADVANCE = 15;
        public const int HURTFACTOR = 1;
        public const int MAXRUSH = 6;
        public const int BREAKCLINCHFREQUENCY = 5;
        public const int BREAKCLINCHPROB = 10;
        public const double MAXDAMAGEFORCHANGINGGAMEPLAN = 12.5;
        public const double STRIKESFROMGUARDDAMAGECUT = 2.5;
        public const int MINCUTSBLEEDING = 2;
        public const int DOCTORCHECKCUTSFREQUENCY = 25;
        public const int DOCTORSTOPPAGE = 20;
        public const int MINACTIONSFORSWITCHING = 3;
        public const int DIRTYMOVEMALUSCUT = 20;
        public const int PULLGUARDCUT = 6;
        public const int CAPITALIZEPROB = 10;
        public const int MINSROUNDSINTHEGROUND = 3;
        public const int FANCY_MOVE_ATTEMP_EXCITEMENT_BONUS = 5;
        public const int FANCY_MOVE_SUCCESS_EXCITEMENT_BONUS = 20;
        public const int SLAM_EXCITEMENT_BONUS = 10;
        public const int UPSET_FREQUENCY = 5;

        // Moral min/max
        public const int MAXMORAL = 20;
        public const int MINMORAL = 1;
        public const int MAXLATESTRESULTS = 5;
        public const int MINLATESTRESULTS = -3;
        public const int MAXFIGHTPERFORMANCE = 3;
        public const int MINFIGHTPERFORMANCE = -3;
        public const int MAXFEARMANAGEMENT = 3;
        public const int MINFEARMANAGEMENT = -3;
        public const int MAXPAINANDTIREDNESS = 1;
        public const int MINPAINANDTIREDNESS = -5;
        public const int FEARMANAGEMENTCUT = 40;
        public const int RANKINGCUT = 2;
        public const double RANKINGWEIGHTCLASS = 0.15;
        public const int MOTIVATIONEDGE = 12;
        public const int MOTIVATIONCUT = 6;

        public const int SMALLINJURIES = 50;
        public const int BIGINJURIES = 175;
        public const int INJURYCUT = 22; //FICAR DE OLHO
        public const int SMALLINJURYORCUTTRUE = 1;
        public const int BIGINJURYORCUTTRUE = 2;
        public const int INJURYORCUTFALSE = 0;

        public const int MOVECOMMMENTSFREQUENCY = 8;

        public const int SOCCERKICKSFREQUENCY = 2;
        public const int STOPMSFREQUENCY = 3;
        public const int KNEESFREQUENCY = 3;

        public const int RESTFREQUENCY = 40;
        public const int ACTIONFREQUENCY = 18;

        // Styles mod
        public const double DEFAULTSTAMINARECOVERY = 0.33;
        public const double ACTIVEFIGHTERSTAMINALOSS = 1.1;
        public const int DEFAULTSTAMINALOSS = 10;
        public const int STAMINACUT = 10;

        public const int DEFAULTFIGHTERSTAMINALOSS = 1;
        public const double AGGLEVEL1STAMINALOSS = 1.1;
        public const double AGGLEVEL2STAMINALOSS = 1.2;
        public const double DEFLEVEL1STAMINALOSS = 0.9;
        public const double DEFLEVEL2STAMINALOSS = 0.8;

        public const int DEFAULTACCURACY = 0;
        public const int TECHLEVEL1ACCURACY = 1;
        public const int TECHLEVEL2ACCURACY = 3;
        public const int PWRLEVEL1ACCURACY = -2;
        public const int PWRLEVEL2ACCURACY = -5;

        public const int DEFAULTDEFLEVEL = 0;
        public const int DEFLEVEL1DEFLEVEL = 1;
        public const int DEFLEVEL2DEFLEVEL = 2;
        public const int AGGLEVEL1DEFLEVEL = -1;
        public const int AGGLEVEL2DEFLEVEL = -2;

        public const int AGGLEVEL1INITMOD = 4;
        public const int AGGLEVEL2INITMOD = 8;
        public const int DEFLEVEL1INITMOD = -4;
        public const int DEFLEVEL2INITMOD = -8;
        public const int DEFAULTINITMOD = 0;

        public const int DEFAULTAGGPOWER = 0;
        public const int AGGLEVEL1DAMAGEMOD = 3;
        public const int AGGLEVEL2DAMAGEMOD = 5;

        public const double PWRLEVEL1INITMOD = 1.5;
        public const double PWRLEVEL2INITMOD = 3.5;
        public const int TECHLEVEL1INITMOD = -1;
        public const int TECHLEVEL2INITMOD = -2;

        public const int TECHLEVEL1DAMCUT = -2;
        public const int TECHLEVEL2DAMCUT = -5;
        public const int PWRLEVEL1DAMCUT = 5;
        public const int PWRLEVEL2DAMCUT = 8;
        public const int DEFAULTDAMAGECUT = 0;

        public const int FANCYMOVEPROB = 2;
        public const int KICKMALUS = 1;
        public const double KICKDAMAGEBONUS = 1.5;
        public const int CLINCHMALUS = 3;

        public const int SLAMSTRENGTH = 15;
        public const int SLAMPROB = 2;
        public const int MAXLOCKINSUBMISSION = 10;

        public const int SUPPLEXSTRENGHT = 15;
        public const int SUPPLEXPROB = 1;

        public const int DANGEROUSCOMMENT = 14;

        // Point system
        public const int FULLMOUNTPOSPOINTS = 2;
        public const int CLOSEDGUARDPOSPOINTS = 1;
        public const int SUCCESSFULTAKEDOWNPOINTS = 5;
        public const int LOCKINSUBMISSIONPOINTS = 5;
        public const int KNOCKDOWNPOINTS = 3;
        public const int MORESTRIKESPOINTS = 5;
        public const int ATTACKLEVELPOINTS = 2;
        public const int DAMAGECUTPOINTS = 10;
        public const int EIGHTPOINTSCRITERIA = 4;
        public const int MOVEPOINTS = 1;

        public const int JUDGEPOINTINGCRITERIA = 9;

        public const double REFTENDENCYTOSTANDUP = 2.5;

        // Rankings
        public const double HIGHERRANKING = 2;
        public const double RANKINGPERCENTAGE = 0.08;
        public const double RANKINGPERCENTAGEHIGH = 0.12;
        public const double RANKINGPERCENTAGELOW = 0.05;
        public const double RANKINGPERCENTAGELOSS = 0.05;
        public const double RANKINGPERCENTAGELOSSSHIGH = 0.1;
        public const double RANKINGPERCENTAGELOSSSLOW = 0.03;

        // Fight outcomes
        public const int RES_KO = 0;
        public const int RES_SUB = 1;
        public const int RES_INJURY = 2;
        public const int RES_DISQ = 3;
        public const int RES_TIMEOUT = 4;
        public const int RES_RDRAW = 5;
        public const int RES_NC = 6;
        public const int RES_TKO = 7;
        public const int RES_CUT = 8;

        // Other
        public const double FOOT = 30.48;
        public const double INCH = 2.54;
        public const double LBS = 453.59;

        // Cons Comments
        public const int GO_TO_THE_JUDGES = 0;
        public const int JUDGE = 1;
        public const int WINNER_IS = 2;
        public const int DECISION = 3;
        public const int SPLIT_DECISION = 4;
        public const int MAJORITY_DECISION = 5;
        public const int MAJORITY_DRAW = 6;
        public const int DRAW = 7;
        public const int BY = 8;
        public const int BETWEEN = 9;
        public const int ANDD = 10;
        public const int PUNCHES = 11;
        public const int KICKS = 12;
        public const int TAKEDOWNS = 13;
        public const int CLINCH_STRIKES = 14;
        public const int SUBMISSIONS = 15;
        public const int GNP_STRIKES = 16;
        public const int STATISTICS = 17;
        public const int DAMAGE_DONE = 18;
        public const int TIME_ON_THE_GROUND = 19;
        public const int FOREHEAD = 20;
        public const int LEFT_EYE = 21;
        public const int RIGHT_EYE = 22;
        public const int LEFT_CHEEK = 23;
        public const int RIGHT_CHEEK = 24;
        public const int NOSE = 25;
        public const int MOUTH = 26;
        public const int CHIN = 27;
        public const int UPPER_BODY = 28;
        public const int RIBS = 29;
        public const int ABDOMEN = 30;
        public const int BACK = 31;
        public const int LEFT_ARM = 32;
        public const int RIGHT_ARM = 33;
        public const int LEFT_TIGH = 34;
        public const int RIGHT_TIGH = 35;
        public const int LEFT_KNEE = 36;
        public const int RIGHT_KNEE = 37;
        public const int LEFT_FOOT = 38;
        public const int RIGHT_FOOT = 39;
        public const int KO = 40;
        public const int TKO = 41;
        public const int SUB = 42;
        public const int INJ = 43;
        public const int NC = 44;
        public const int DQ = 45;
        public const int TIMEOUT = 46;
        public const int FRECORD = 47;
        public const int GRAPPLING = 48;
        public const int CLINCH_DAMAGE = 49;
        public const int GROUND_DAMAGE = 50;
        public const int AVERAGE_DAMAGE = 51;
        public const int CAUSED = 52;
        public const int RECEIVED = 53;
        public const int DAMAGE = 54;
        public const int CUT = 55;
        public const int DOCTOR_STOPPAGE = 56;
        public const int REF_OF_THE_BOUT = 57;
        public const int FREAKSHOW = 58;
        public const int UNFAIR = 59;
        public const int MISMATCH = 60;
        public const int FAIR = 61;
        public const int GREAT_MATCH = 62;
        public const int VERY_ANTICIPATED = 63;
        public const int DREAM_MATCH = 64;
        public const int EVENT_PLACE = 65;
        public const int ATTENDANCE = 66;
        public const int PPV_BUYS = 67;
        public const int PRELIM_CARD_EXC = 68;
        public const int MAIN_CARD_EXC = 69;
        public const int MAIN_EVENTS_EXC = 70;
        public const int OVERALL_QUALITY = 71;
        public const int FIGHTS_PLAYED = 72;
        public const int SHW = 73;
        public const int HW = 74;
        public const int LHW = 75;
        public const int MW = 76;
        public const int WW = 77;
        public const int LW = 78;
        public const int FW = 79;
        public const int BW = 80;
        public const int BOUT = 81;
        public const int TKOS = 82;
        public const int INJURIES = 83;
        public const int DECISSION = 84;
        public const int DISQUALIFICATION = 85;
        public const int AVERAGE_DURATION = 86;
        public const int MOST_USED_REF = 87;
        public const int MOST_USED_ORG = 88;
        public const int TOTAL_DURATION = 89;
        public const int ANY_ORGANIZATION = 90;
        public const int UNKNOWN = 91;
        public const int BREAK_RECORD = 92;
        public const int MORE_DAMAGE_FIGHT = 93;
        public const int MORE_DAMAGE_HIT = 94;
        public const int FASTEST_KO = 95;
        public const int FASTEST_SUB = 96;
        public const int N_ROUND = 97;
        public const int PREVIOUS_FIGHTS = 98;
        public const int WON_BEFORE = 99;
        public const int NEVER_BEFORE = 100;
        public const int LOST_BEFORE = 101;
        public const int TOWEL_THROW = 102;
        public const int GOIN_KICK = 103;
        public const int EYE_POKE = 104;
        public const int HEADBUTT = 105;
        public const int STRANGLE = 106;
        public const int DEFENDS_TITLE = 107;
        public const int WINS_TITLE = 108;
        public const int TIE_BREAK_ROUND = 109;
        public const int WIN_RANKING_POINTS = 110;
        public const int LOSE_RANKING_POINTS = 111;
        public const int BEST_FIGHT = 112;
        public const int WORST_FIGHT = 113;
        public const int WINNING_STREAK = 114;
        public const int LOSING_STREAK = 115;
        public const int VERY_BORING = 116;
        public const int DULL = 117;
        public const int AVERAGE = 118;
        public const int PROMISING = 119;
        public const int EXCITING = 120;

        // Fight
        public const int ACT_PUNCHES = 1;
        public const int ACT_KICKS = 2;
        public const int ACT_CLINCH = 3;
        public const int ACT_TAKEDOWNS = 4;
        public const int ACT_DIRTYBOXING = 5;
        public const int ACT_THAICLINCH_PUNCHES = 6;
        public const int ACT_TAKEDOWNCLINCH = 7;
        public const int ACT_BREAKCLINCH = 8;
        public const int ACT_GNP = 9;
        public const int ACT_POSITIONING = 10;
        public const int ACT_SUBMISSION = 11;
        public const int ACT_STANDINGSUB = 12;
        public const int ACT_STANDUP = 13;
        public const int ACT_LNP = 14;
        public const int ACT_FANCYPUNCH = 15;
        public const int ACT_FANCYKICK = 16;
        public const int ACT_HEADBUTT = 17;
        public const int ACT_BITE = 18;
        public const int ACT_POKE = 19;
        public const int ACT_REST = 20;
        public const int ACT_GROINKICK = 21;
        public const int ACT_SLAM = 22;
        public const int ACT_SUPPLEX = 23;
        public const int ACT_SOCCERKICKS = 24;
        public const int ACT_STOMPS = 25;
        public const int ACT_STANDKICK = 26;
        public const int ACT_MOVETOGROUND = 27;
        public const int ACT_STRIKESFROMGUARD = 28;
        public const int ACT_GROUNDKICK = 29;
        public const int ACT_RESTCLINCH = 30;
        public const int ACT_NOACTION = 31;
        public const int ACT_ALLOWSTAND = 32;
        public const int ACT_PUNCHEXCHANGE = 33;
        public const int ACT_PULLGUARD = 34;
        public const int ACT_GNPELBOWS = 35;
        public const int ACT_CAPITALIZESTAND = 36;
        public const int ACT_CAPITALIZEGROUND = 37;
        public const int ACT_KNEESONGROUND = 38;
        public const int ACT_FANCYSUB = 39;
        public const int ACT_THAICLINCH_KNEES = 40;
        public const int ACT_GRAPPLING_KNEE = 41;
        public const int ACT_GRAPPLING_PUNCH = 42;

        // Takedowns
        public const int JUDO = 1;
        public const int WRESTLING = 2;

        // Clinch Attack
        public const int THAI_ATTACK = 1;
        public const int DIRTY_BOXING = 2;
        public const int GRAPPLING_ATTACK = 3;

        // Guards
        public const int REAR_MOUNT = 0;
        public const int FULL_MOUNT = 1;
        public const int SIDE_MOUNT = 2;
        public const int HALF_GUARD = 3;
        public const int OPEN_GUARD = 4;
        public const int CLOSED_GUARD = 5;
        public const int BUTTERFLY_GUARD = 6;

        // Clinch
        public const int CLINCH_DIRTY_BOXING = 0;
        public const int THAI_CLINCH = 1;
        public const int SIMPLE_GRAPPLING = 2;

        public const string RING = "ring";
        public const string CAGE = "cage";
        public const string OCTAGON = "octagon";
        public const string ROPES = "ropes";
        public const string FENCE = "fence";
        public const string ELBOW = "elbow";
        public const string ELBOWS = "elbows";
        public const string REF = "referee";
        public const string LEFT = "left";
        public const string RIGHT = "right";

        public const bool oldUMSTime = false;

        public static int SetLimits(int actual, int max, int min)
        {
            if (actual > max)
            {
                actual = max;
            }
            else if (actual < min)
            {
                actual = min;
            }
            return actual;
        }

    }
}

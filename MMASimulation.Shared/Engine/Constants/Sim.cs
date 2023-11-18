namespace MMASimulation.Shared.Engine.Constants
{
    public static class Sim
    {

        public static int BIGRANDOM = 20;
        public static int SMALLRANDOM = 10;
        public static int DAMAGECUT = 25;
        public static int KOPROBCUT = 40;
        public static double KOFREQUENCY = 1.1;
        public static int TKORUSHMINIMUN = 2;
        public static int TKOMINHITPOINTS = 30;
        public static int TKOFREQUENCY = 9;
        public static int SUBDIFFICULT = 22;
        public static double SUBDEFENSECUT = 1.5;
        public static int SUBMALUS = 9;
        public static int REFSUBFREQUENCY = 9;
        public static double NINECMDIFFERENCE = 0.75;
        public static double ONEWEIGHTCLASSDIFFERENCE = 0.75;
        public static int STRENGTHONEWEIGHTCLASSDIFERENCE = 1;
        public static int LEVEL2SKILL = 12;
        public static int LEVEL3SKILL = 15;
        public static int COUNTERATTACKCUT = 2;
        public static int FATIGUECUT = 1;
        public static int TIMEADVANCE = 15;
        public static int HURTFACTOR = 1;
        public static int MAXRUSH = 6;
        public static int BREAKCLINCHFREQUENCY = 5;
        public static int BREAKCLINCHPROB = 10;
        public static double MAXDAMAGEFORCHANGINGGAMEPLAN = 12.5;
        public static double STRIKESFROMGUARDDAMAGECUT = 2.5;
        public static int MINCUTSBLEEDING = 2;
        public static int DOCTORCHECKCUTSFREQUENCY = 25;
        public static int DOCTORSTOPPAGE = 20;
        public static int MINACTIONSFORSWITCHING = 3;
        public static int DIRTYMOVEMALUSCUT = 20;
        public static int PULLGUARDCUT = 6;
        public static int CAPITALIZEPROB = 10;
        public static int MINSROUNDSINTHEGROUND = 3;
        public static int FANCY_MOVE_ATTEMP_EXCITEMENT_BONUS = 5;
        public static int FANCY_MOVE_SUCCESS_EXCITEMENT_BONUS = 20;
        public static int SLAM_EXCITEMENT_BONUS = 10;
        public static int UPSET_FREQUENCY = 5;

        // Moral min/max
        public static int MAXMORAL = 20;
        public static int MINMORAL = 1;
        public static int MAXLATESTRESULTS = 5;
        public static int MINLATESTRESULTS = -3;
        public static int MAXFIGHTPERFORMANCE = 3;
        public static int MINFIGHTPERFORMANCE = -3;
        public static int MAXFEARMANAGEMENT = 3;
        public static int MINFEARMANAGEMENT = -3;
        public static int MAXPAINANDTIREDNESS = 1;
        public static int MINPAINANDTIREDNESS = -5;
        public static int FEARMANAGEMENTCUT = 40;
        public static int RANKINGCUT = 2;
        public static double RANKINGWEIGHTCLASS = 0.15;
        public static int MOTIVATIONEDGE = 12;
        public static int MOTIVATIONCUT = 6;

        public static int SMALLINJURIES = 50;
        public static int BIGINJURIES = 175;
        public static int INJURYCUT = 22; //FICAR DE OLHO
        public static int SMALLINJURYORCUTTRUE = 1;
        public static int BIGINJURYORCUTTRUE = 2;
        public static int INJURYORCUTFALSE = 0;

        public static int MOVECOMMMENTSFREQUENCY = 8;

        public static int SOCCERKICKSFREQUENCY = 2;
        public static int STOPMSFREQUENCY = 3;
        public static int KNEESFREQUENCY = 3;

        public static int RESTFREQUENCY = 40;
        public static int ACTIONFREQUENCY = 18;

        // Styles mod
        public static double DEFAULTSTAMINARECOVERY = 0.33;
        public static double ACTIVEFIGHTERSTAMINALOSS = 1.1;
        public static int DEFAULTSTAMINALOSS = 10;
        public static int STAMINACUT = 10;

        public static int DEFAULTFIGHTERSTAMINALOSS = 1;
        public static double AGGLEVEL1STAMINALOSS = 1.1;
        public static double AGGLEVEL2STAMINALOSS = 1.2;
        public static double DEFLEVEL1STAMINALOSS = 0.9;
        public static double DEFLEVEL2STAMINALOSS = 0.8;

        public static int DEFAULTACCURACY = 0;
        public static int TECHLEVEL1ACCURACY = 1;
        public static int TECHLEVEL2ACCURACY = 3;
        public static int PWRLEVEL1ACCURACY = -2;
        public static int PWRLEVEL2ACCURACY = -5;

        public static int DEFAULTDEFLEVEL = 0;
        public static int DEFLEVEL1DEFLEVEL = 1;
        public static int DEFLEVEL2DEFLEVEL = 2;
        public static int AGGLEVEL1DEFLEVEL = -1;
        public static int AGGLEVEL2DEFLEVEL = -2;

        public static int AGGLEVEL1INITMOD = 4;
        public static int AGGLEVEL2INITMOD = 8;
        public static int DEFLEVEL1INITMOD = -4;
        public static int DEFLEVEL2INITMOD = -8;
        public static int DEFAULTINITMOD = 0;

        public static int DEFAULTAGGPOWER = 0;
        public static int AGGLEVEL1DAMAGEMOD = 3;
        public static int AGGLEVEL2DAMAGEMOD = 5;

        public static double PWRLEVEL1INITMOD = 1.5;
        public static double PWRLEVEL2INITMOD = 3.5;
        public static int TECHLEVEL1INITMOD = -1;
        public static int TECHLEVEL2INITMOD = -2;

        public static int TECHLEVEL1DAMCUT = -2;
        public static int TECHLEVEL2DAMCUT = -5;
        public static int PWRLEVEL1DAMCUT = 5;
        public static int PWRLEVEL2DAMCUT = 8;
        public static int DEFAULTDAMAGECUT = 0;

        public static int FANCYMOVEPROB = 2;
        public static int KICKMALUS = 1;
        public static double KICKDAMAGEBONUS = 1.5;
        public static int CLINCHMALUS = 3;

        public static int SLAMSTRENGTH = 15;
        public static int SLAMPROB = 2;
        public static int MAXLOCKINSUBMISSION = 10;

        public static int SUPPLEXSTRENGHT = 15;
        public static int SUPPLEXPROB = 1;

        public static int DANGEROUSCOMMENT = 14;

        // Point system
        public static int FULLMOUNTPOSPOINTS = 2;
        public static int CLOSEDGUARDPOSPOINTS = 1;
        public static int SUCCESSFULTAKEDOWNPOINTS = 5;
        public static int LOCKINSUBMISSIONPOINTS = 5;
        public static int KNOCKDOWNPOINTS = 3;
        public static int MORESTRIKESPOINTS = 5;
        public static int ATTACKLEVELPOINTS = 2;
        public static int DAMAGECUTPOINTS = 10;
        public static int EIGHTPOINTSCRITERIA = 4;
        public static int MOVEPOINTS = 1;

        public static int JUDGEPOINTINGCRITERIA = 9;

        public static double REFTENDENCYTOSTANDUP = 2.5;

        // Rankings
        public static double HIGHERRANKING = 2;
        public static double RANKINGPERCENTAGE = 0.08;
        public static double RANKINGPERCENTAGEHIGH = 0.12;
        public static double RANKINGPERCENTAGELOW = 0.05;
        public static double RANKINGPERCENTAGELOSS = 0.05;
        public static double RANKINGPERCENTAGELOSSSHIGH = 0.1;
        public static double RANKINGPERCENTAGELOSSSLOW = 0.03;

        // Fight outcomes
        public static int RES_KO = 0;
        public static int RES_SUB = 1;
        public static int RES_INJURY = 2;
        public static int RES_DISQ = 3;
        public static int RES_TIMEOUT = 4;
        public static int RES_RDRAW = 5;
        public static int RES_NC = 6;
        public static int RES_TKO = 7;
        public static int RES_CUT = 8;

        // Other
        public static double FOOT = 30.48;
        public static double INCH = 2.54;
        public static double LBS = 453.59;

        // Cons Comments
        public static int GO_TO_THE_JUDGES = 0;
        public static int JUDGE = 1;
        public static int WINNER_IS = 2;
        public static int DECISION = 3;
        public static int SPLIT_DECISION = 4;
        public static int MAJORITY_DECISION = 5;
        public static int MAJORITY_DRAW = 6;
        public static int DRAW = 7;
        public static int BY = 8;
        public static int BETWEEN = 9;
        public static int ANDD = 10;
        public static int PUNCHES = 11;
        public static int KICKS = 12;
        public static int TAKEDOWNS = 13;
        public static int CLINCH_STRIKES = 14;
        public static int SUBMISSIONS = 15;
        public static int GNP_STRIKES = 16;
        public static int STATISTICS = 17;
        public static int DAMAGE_DONE = 18;
        public static int TIME_ON_THE_GROUND = 19;
        public static int FOREHEAD = 20;
        public static int LEFT_EYE = 21;
        public static int RIGHT_EYE = 22;
        public static int LEFT_CHEEK = 23;
        public static int RIGHT_CHEEK = 24;
        public static int NOSE = 25;
        public static int MOUTH = 26;
        public static int CHIN = 27;
        public static int UPPER_BODY = 28;
        public static int RIBS = 29;
        public static int ABDOMEN = 30;
        public static int BACK = 31;
        public static int LEFT_ARM = 32;
        public static int RIGHT_ARM = 33;
        public static int LEFT_TIGH = 34;
        public static int RIGHT_TIGH = 35;
        public static int LEFT_KNEE = 36;
        public static int RIGHT_KNEE = 37;
        public static int LEFT_FOOT = 38;
        public static int RIGHT_FOOT = 39;
        public static int KO = 40;
        public static int TKO = 41;
        public static int SUB = 42;
        public static int INJ = 43;
        public static int NC = 44;
        public static int DQ = 45;
        public static int TIMEOUT = 46;
        public static int FRECORD = 47;
        public static int GRAPPLING = 48;
        public static int CLINCH_DAMAGE = 49;
        public static int GROUND_DAMAGE = 50;
        public static int AVERAGE_DAMAGE = 51;
        public static int CAUSED = 52;
        public static int RECEIVED = 53;
        public static int DAMAGE = 54;
        public static int CUT = 55;
        public static int DOCTOR_STOPPAGE = 56;
        public static int REF_OF_THE_BOUT = 57;
        public static int FREAKSHOW = 58;
        public static int UNFAIR = 59;
        public static int MISMATCH = 60;
        public static int FAIR = 61;
        public static int GREAT_MATCH = 62;
        public static int VERY_ANTICIPATED = 63;
        public static int DREAM_MATCH = 64;
        public static int EVENT_PLACE = 65;
        public static int ATTENDANCE = 66;
        public static int PPV_BUYS = 67;
        public static int PRELIM_CARD_EXC = 68;
        public static int MAIN_CARD_EXC = 69;
        public static int MAIN_EVENTS_EXC = 70;
        public static int OVERALL_QUALITY = 71;
        public static int FIGHTS_PLAYED = 72;
        public static int SHW = 73;
        public static int HW = 74;
        public static int LHW = 75;
        public static int MW = 76;
        public static int WW = 77;
        public static int LW = 78;
        public static int FW = 79;
        public static int BW = 80;
        public static int BOUT = 81;
        public static int TKOS = 82;
        public static int INJURIES = 83;
        public static int DECISSION = 84;
        public static int DISQUALIFICATION = 85;
        public static int AVERAGE_DURATION = 86;
        public static int MOST_USED_REF = 87;
        public static int MOST_USED_ORG = 88;
        public static int TOTAL_DURATION = 89;
        public static int ANY_ORGANIZATION = 90;
        public static int UNKNOWN = 91;
        public static int BREAK_RECORD = 92;
        public static int MORE_DAMAGE_FIGHT = 93;
        public static int MORE_DAMAGE_HIT = 94;
        public static int FASTEST_KO = 95;
        public static int FASTEST_SUB = 96;
        public static int N_ROUND = 97;
        public static int PREVIOUS_FIGHTS = 98;
        public static int WON_BEFORE = 99;
        public static int NEVER_BEFORE = 100;
        public static int LOST_BEFORE = 101;
        public static int TOWEL_THROW = 102;
        public static int GOIN_KICK = 103;
        public static int EYE_POKE = 104;
        public static int HEADBUTT = 105;
        public static int STRANGLE = 106;
        public static int DEFENDS_TITLE = 107;
        public static int WINS_TITLE = 108;
        public static int TIE_BREAK_ROUND = 109;
        public static int WIN_RANKING_POINTS = 110;
        public static int LOSE_RANKING_POINTS = 111;
        public static int BEST_FIGHT = 112;
        public static int WORST_FIGHT = 113;
        public static int WINNING_STREAK = 114;
        public static int LOSING_STREAK = 115;
        public static int VERY_BORING = 116;
        public static int DULL = 117;
        public static int AVERAGE = 118;
        public static int PROMISING = 119;
        public static int EXCITING = 120;

        // Fight
        public static int ACT_PUNCHES = 1;
        public static int ACT_KICKS = 2;
        public static int ACT_CLINCH = 3;
        public static int ACT_TAKEDOWNS = 4;
        public static int ACT_DIRTYBOXING = 5;
        public static int ACT_THAICLINCH_PUNCHES = 6;
        public static int ACT_TAKEDOWNCLINCH = 7;
        public static int ACT_BREAKCLINCH = 8;
        public static int ACT_GNP = 9;
        public static int ACT_POSITIONING = 10;
        public static int ACT_SUBMISSION = 11;
        public static int ACT_STANDINGSUB = 12;
        public static int ACT_STANDUP = 13;
        public static int ACT_LNP = 14;
        public static int ACT_FANCYPUNCH = 15;
        public static int ACT_FANCYKICK = 16;
        public static int ACT_HEADBUTT = 17;
        public static int ACT_BITE = 18;
        public static int ACT_POKE = 19;
        public static int ACT_REST = 20;
        public static int ACT_GROINKICK = 21;
        public static int ACT_SLAM = 22;
        public static int ACT_SUPPLEX = 23;
        public static int ACT_SOCCERKICKS = 24;
        public static int ACT_STOMPS = 25;
        public static int ACT_STANDKICK = 26;
        public static int ACT_MOVETOGROUND = 27;
        public static int ACT_STRIKESFROMGUARD = 28;
        public static int ACT_GROUNDKICK = 29;
        public static int ACT_RESTCLINCH = 30;
        public static int ACT_NOACTION = 31;
        public static int ACT_ALLOWSTAND = 32;
        public static int ACT_PUNCHEXCHANGE = 33;
        public static int ACT_PULLGUARD = 34;
        public static int ACT_GNPELBOWS = 35;
        public static int ACT_CAPITALIZESTAND = 36;
        public static int ACT_CAPITALIZEGROUND = 37;
        public static int ACT_KNEESONGROUND = 38;
        public static int ACT_FANCYSUB = 39;
        public static int ACT_THAICLINCH_KNEES = 40;
        public static int ACT_GRAPPLING_KNEE = 41;
        public static int ACT_GRAPPLING_PUNCH = 42;

        // Takedowns
        public static int JUDO = 1;
        public static int WRESTLING = 2;

        // Clinch Attack
        public static int THAI_ATTACK = 1;
        public static int DIRTY_BOXING = 2;
        public static int GRAPPLING_ATTACK = 3;

        // Guards
        public static int REAR_MOUNT = 0;
        public static int FULL_MOUNT = 1;
        public static int SIDE_MOUNT = 2;
        public static int HALF_GUARD = 3;
        public static int OPEN_GUARD = 4;
        public static int CLOSED_GUARD = 5;
        public static int BUTTERFLY_GUARD = 6;

        // Clinch
        public static int CLINCH_DIRTY_BOXING = 0;
        public static int THAI_CLINCH = 1;
        public static int SIMPLE_GRAPPLING = 2;

        public static string UNKNOWN_STR = "Unknown";
        public static string RING = "ring";
        public static string CAGE = "cage";
        public static string OCTAGON = "octógono";
        public static string ROPES = "cordas";
        public static string FENCE = "grades";
        public static string ELBOW = "cotovelada";
        public static string ELBOWS = "cotoveladas";
        public static string REF = "referee";
        public static string LEFT = "esquerda";
        public static string RIGHT = "direita";

        public static bool oldUMSTime = false;

        public static int setLimits(int actual, int max, int min)
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

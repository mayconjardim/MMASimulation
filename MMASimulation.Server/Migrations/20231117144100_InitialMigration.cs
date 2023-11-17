using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMASimulation.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fighters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Wins = table.Column<int>(type: "INTEGER", nullable: false),
                    Loss = table.Column<int>(type: "INTEGER", nullable: false),
                    Draw = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fighters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FighterRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FighterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Punching = table.Column<double>(type: "REAL", nullable: false),
                    Kicking = table.Column<double>(type: "REAL", nullable: false),
                    ClinchStriking = table.Column<double>(type: "REAL", nullable: false),
                    ClinchGrappling = table.Column<double>(type: "REAL", nullable: false),
                    Takedowns = table.Column<double>(type: "REAL", nullable: false),
                    Gnp = table.Column<double>(type: "REAL", nullable: false),
                    Submission = table.Column<double>(type: "REAL", nullable: false),
                    GroundGame = table.Column<double>(type: "REAL", nullable: false),
                    Dodging = table.Column<double>(type: "REAL", nullable: false),
                    SubDefense = table.Column<double>(type: "REAL", nullable: false),
                    TakedownsDef = table.Column<double>(type: "REAL", nullable: false),
                    Aggressiveness = table.Column<double>(type: "REAL", nullable: false),
                    Control = table.Column<double>(type: "REAL", nullable: false),
                    Motivation = table.Column<double>(type: "REAL", nullable: false),
                    Strength = table.Column<double>(type: "REAL", nullable: false),
                    Agility = table.Column<double>(type: "REAL", nullable: false),
                    Conditioning = table.Column<double>(type: "REAL", nullable: false),
                    KoResistance = table.Column<double>(type: "REAL", nullable: false),
                    Toughness = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FighterRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FighterRatings_Fighters_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FighterStrategies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FighterId = table.Column<int>(type: "INTEGER", nullable: false),
                    StratPunching = table.Column<int>(type: "INTEGER", nullable: false),
                    StratKicking = table.Column<int>(type: "INTEGER", nullable: false),
                    StratClinching = table.Column<int>(type: "INTEGER", nullable: false),
                    StratTakedowns = table.Column<int>(type: "INTEGER", nullable: false),
                    StratDirtyBoxing = table.Column<int>(type: "INTEGER", nullable: false),
                    StratThaiClinch = table.Column<int>(type: "INTEGER", nullable: false),
                    StratClinchTakedowns = table.Column<int>(type: "INTEGER", nullable: false),
                    StratAvoidClinch = table.Column<int>(type: "INTEGER", nullable: false),
                    StratGNP = table.Column<int>(type: "INTEGER", nullable: false),
                    StratSub = table.Column<int>(type: "INTEGER", nullable: false),
                    StratPositioning = table.Column<int>(type: "INTEGER", nullable: false),
                    StratLNP = table.Column<int>(type: "INTEGER", nullable: false),
                    StratStandUp = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FighterStrategies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FighterStrategies_Fighters_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FighterStyles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FighterId = table.Column<int>(type: "INTEGER", nullable: false),
                    FancyPunches = table.Column<int>(type: "INTEGER", nullable: false),
                    FightingStyle = table.Column<int>(type: "INTEGER", nullable: false),
                    TacticalStyle = table.Column<int>(type: "INTEGER", nullable: false),
                    FancyKicks = table.Column<int>(type: "INTEGER", nullable: false),
                    FancySubmissions = table.Column<int>(type: "INTEGER", nullable: false),
                    DirtyFighting = table.Column<int>(type: "INTEGER", nullable: false),
                    Stalling = table.Column<int>(type: "INTEGER", nullable: false),
                    EasySubs = table.Column<bool>(type: "INTEGER", nullable: false),
                    TechSubs = table.Column<bool>(type: "INTEGER", nullable: false),
                    UseKneesGround = table.Column<bool>(type: "INTEGER", nullable: false),
                    UseStomps = table.Column<bool>(type: "INTEGER", nullable: false),
                    UseSoccerKicks = table.Column<bool>(type: "INTEGER", nullable: false),
                    PullsGuard = table.Column<bool>(type: "INTEGER", nullable: false),
                    ClinchType = table.Column<int>(type: "INTEGER", nullable: false),
                    DirtyBoxing = table.Column<bool>(type: "INTEGER", nullable: false),
                    ThaiClinch = table.Column<bool>(type: "INTEGER", nullable: false),
                    JudoTD = table.Column<bool>(type: "INTEGER", nullable: false),
                    WrestlingTD = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FighterStyles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FighterStyles_Fighters_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FighterRatings_FighterId",
                table: "FighterRatings",
                column: "FighterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FighterStrategies_FighterId",
                table: "FighterStrategies",
                column: "FighterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FighterStyles_FighterId",
                table: "FighterStyles",
                column: "FighterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FighterRatings");

            migrationBuilder.DropTable(
                name: "FighterStrategies");

            migrationBuilder.DropTable(
                name: "FighterStyles");

            migrationBuilder.DropTable(
                name: "Fighters");
        }
    }
}

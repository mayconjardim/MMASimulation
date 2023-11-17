using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMASimulation.Server.Migrations
{
    /// <inheritdoc />
    public partial class Fight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumberRounds = table.Column<int>(type: "INTEGER", nullable: false),
                    TitleBout = table.Column<bool>(type: "INTEGER", nullable: false),
                    GeneratePBP = table.Column<bool>(type: "INTEGER", nullable: false),
                    Happened = table.Column<bool>(type: "INTEGER", nullable: false),
                    Fighter1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Fighter2Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fights_Fighters_Fighter1Id",
                        column: x => x.Fighter1Id,
                        principalTable: "Fighters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fights_Fighters_Fighter2Id",
                        column: x => x.Fighter2Id,
                        principalTable: "Fighters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FightPBPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FightId = table.Column<int>(type: "INTEGER", nullable: false),
                    PbpData = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FightPBPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FightPBPs_Fights_FightId",
                        column: x => x.FightId,
                        principalTable: "Fights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FightPBPs_FightId",
                table: "FightPBPs",
                column: "FightId");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_Fighter1Id",
                table: "Fights",
                column: "Fighter1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_Fighter2Id",
                table: "Fights",
                column: "Fighter2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FightPBPs");

            migrationBuilder.DropTable(
                name: "Fights");
        }
    }
}

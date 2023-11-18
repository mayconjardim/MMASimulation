using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMASimulation.Server.Migrations
{
    /// <inheritdoc />
    public partial class RefaturandoFight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "weightClass",
                table: "Fighters",
                newName: "WeightClass");

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Fighters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "FightId",
                table: "FightPBPs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Fighters");

            migrationBuilder.RenameColumn(
                name: "WeightClass",
                table: "Fighters",
                newName: "weightClass");

            migrationBuilder.AlterColumn<int>(
                name: "FightId",
                table: "FightPBPs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMASimulation.Server.Migrations
{
    /// <inheritdoc />
    public partial class WeightClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "weightClass",
                table: "Fighters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "weightClass",
                table: "Fighters");
        }
    }
}

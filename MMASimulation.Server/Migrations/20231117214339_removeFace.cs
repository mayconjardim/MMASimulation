using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMASimulation.Server.Migrations
{
    /// <inheritdoc />
    public partial class removeFace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fighters_Faces_FaceDbId",
                table: "Fighters");

            migrationBuilder.DropTable(
                name: "Eye");

            migrationBuilder.DropTable(
                name: "Eyebrow");

            migrationBuilder.DropTable(
                name: "Faces");

            migrationBuilder.DropTable(
                name: "Hair");

            migrationBuilder.DropTable(
                name: "Head");

            migrationBuilder.DropTable(
                name: "Mouth");

            migrationBuilder.DropTable(
                name: "Nose");

            migrationBuilder.DropIndex(
                name: "IX_Fighters_FaceDbId",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "FaceDbId",
                table: "Fighters");

            migrationBuilder.AddColumn<string>(
                name: "Face",
                table: "Fighters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Face",
                table: "Fighters");

            migrationBuilder.AddColumn<int>(
                name: "FaceDbId",
                table: "Fighters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Hair",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hair", x => x.DbId);
                });

            migrationBuilder.CreateTable(
                name: "Head",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Head", x => x.DbId);
                });

            migrationBuilder.CreateTable(
                name: "Mouth",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cx = table.Column<int>(type: "INTEGER", nullable: false),
                    Cy = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouth", x => x.DbId);
                });

            migrationBuilder.CreateTable(
                name: "Nose",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cx = table.Column<int>(type: "INTEGER", nullable: false),
                    Cy = table.Column<int>(type: "INTEGER", nullable: false),
                    Flip = table.Column<bool>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    LR = table.Column<string>(type: "TEXT", nullable: false),
                    Size = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nose", x => x.DbId);
                });

            migrationBuilder.CreateTable(
                name: "Faces",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HairDbId = table.Column<int>(type: "INTEGER", nullable: false),
                    HeadDbId = table.Column<int>(type: "INTEGER", nullable: false),
                    MouthDbId = table.Column<int>(type: "INTEGER", nullable: false),
                    NoseDbId = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    Fatness = table.Column<double>(type: "REAL", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faces", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_Faces_Hair_HairDbId",
                        column: x => x.HairDbId,
                        principalTable: "Hair",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faces_Head_HeadDbId",
                        column: x => x.HeadDbId,
                        principalTable: "Head",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faces_Mouth_MouthDbId",
                        column: x => x.MouthDbId,
                        principalTable: "Mouth",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faces_Nose_NoseDbId",
                        column: x => x.NoseDbId,
                        principalTable: "Nose",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eye",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Angle = table.Column<double>(type: "REAL", nullable: false),
                    Cx = table.Column<int>(type: "INTEGER", nullable: false),
                    Cy = table.Column<int>(type: "INTEGER", nullable: false),
                    FaceDbId = table.Column<int>(type: "INTEGER", nullable: true),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    LR = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eye", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_Eye_Faces_FaceDbId",
                        column: x => x.FaceDbId,
                        principalTable: "Faces",
                        principalColumn: "DbId");
                });

            migrationBuilder.CreateTable(
                name: "Eyebrow",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cx = table.Column<int>(type: "INTEGER", nullable: false),
                    Cy = table.Column<int>(type: "INTEGER", nullable: false),
                    FaceDbId = table.Column<int>(type: "INTEGER", nullable: true),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    LR = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eyebrow", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_Eyebrow_Faces_FaceDbId",
                        column: x => x.FaceDbId,
                        principalTable: "Faces",
                        principalColumn: "DbId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fighters_FaceDbId",
                table: "Fighters",
                column: "FaceDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Eye_FaceDbId",
                table: "Eye",
                column: "FaceDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Eyebrow_FaceDbId",
                table: "Eyebrow",
                column: "FaceDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Faces_HairDbId",
                table: "Faces",
                column: "HairDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Faces_HeadDbId",
                table: "Faces",
                column: "HeadDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Faces_MouthDbId",
                table: "Faces",
                column: "MouthDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Faces_NoseDbId",
                table: "Faces",
                column: "NoseDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fighters_Faces_FaceDbId",
                table: "Fighters",
                column: "FaceDbId",
                principalTable: "Faces",
                principalColumn: "DbId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPI.Migrations
{
    /// <inheritdoc />
    public partial class CursoParticipante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserCursoEntity",
                columns: table => new
                {
                    CursosId = table.Column<int>(type: "int", nullable: false),
                    ParticipantesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserCursoEntity", x => new { x.CursosId, x.ParticipantesId });
                    table.ForeignKey(
                        name: "FK_AppUserCursoEntity_AspNetUsers_ParticipantesId",
                        column: x => x.ParticipantesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppUserCursoEntity_Cursos_CursosId",
                        column: x => x.CursosId,
                        principalTable: "Cursos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCursoEntity_ParticipantesId",
                table: "AppUserCursoEntity",
                column: "ParticipantesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserCursoEntity");
        }
    }
}

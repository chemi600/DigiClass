using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPI.Migrations
{
    /// <inheritdoc />
    public partial class DeleteParticipantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserCursoEntity");

            migrationBuilder.CreateTable(
                name: "CursoEstudiantes",
                columns: table => new
                {
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoEstudiantes", x => new { x.CursoId, x.EstudianteId });
                    table.ForeignKey(
                        name: "FK_CursoEstudiantes_AspNetUsers_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoEstudiantes_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursoEstudiantes_EstudianteId",
                table: "CursoEstudiantes",
                column: "EstudianteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoEstudiantes");

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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserCursoEntity_Cursos_CursosId",
                        column: x => x.CursosId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCursoEntity_ParticipantesId",
                table: "AppUserCursoEntity",
                column: "ParticipantesId");
        }
    }
}

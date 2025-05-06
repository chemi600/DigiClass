using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPI.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_AspNetUsers_IdProfesor",
                table: "Cursos");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_AspNetUsers_IdProfesor",
                table: "Cursos",
                column: "IdProfesor",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_AspNetUsers_IdProfesor",
                table: "Cursos");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_AspNetUsers_IdProfesor",
                table: "Cursos",
                column: "IdProfesor",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

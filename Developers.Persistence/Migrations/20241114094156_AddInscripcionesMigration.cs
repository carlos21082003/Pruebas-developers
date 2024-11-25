using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Developers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInscripcionesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InscripcionesId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    InscripcionesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionDateInscripciones = table.Column<DateTime>(type: "date", nullable: false),
                    HoursInscripciones = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    DetailsInscripciones = table.Column<string>(type: "text", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.InscripcionesId);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Inscripciones_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_InscripcionesId",
                table: "Enrollments",
                column: "InscripcionesId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_CourseId",
                table: "Inscripciones",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_StudentId",
                table: "Inscripciones",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Inscripciones_InscripcionesId",
                table: "Enrollments",
                column: "InscripcionesId",
                principalTable: "Inscripciones",
                principalColumn: "InscripcionesId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscripciones");
        }
    }
}

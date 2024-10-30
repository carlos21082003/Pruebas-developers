using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Developers.Persistence.Migrations;

/// <inheritdoc />
public partial class AddEnrollmentMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Enrollments",
            columns: table => new
            {
                EnrollmentId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ClassroomId = table.Column<int>(type: "int", nullable: false),
                StudentId = table.Column<int>(type: "int", nullable: false),
                PreTest = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                PostTest = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                Passed = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                Status = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Enrollments", x => x.EnrollmentId);
                table.ForeignKey(
                    name: "FK_Enrollments_Classrooms_ClassroomId",
                    column: x => x.ClassroomId,
                    principalTable: "Classrooms",
                    principalColumn: "ClassroomId");
                table.ForeignKey(
                    name: "FK_Enrollments_Students_StudentId",
                    column: x => x.StudentId,
                    principalTable: "Students",
                    principalColumn: "StudentId");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Enrollments_ClassroomId",
            table: "Enrollments",
            column: "ClassroomId");

        migrationBuilder.CreateIndex(
            name: "IX_Enrollments_StudentId",
            table: "Enrollments",
            column: "StudentId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Enrollments");
    }
}

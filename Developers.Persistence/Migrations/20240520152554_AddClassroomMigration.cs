using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Developers.Persistence.Migrations;

/// <inheritdoc />
public partial class AddClassroomMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Classrooms",
            columns: table => new
            {
                ClassroomId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                SessionDate = table.Column<DateTime>(type: "date", nullable: false),
                Hours = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                Details = table.Column<string>(type: "text", nullable: true),
                TrainerId = table.Column<int>(type: "int", nullable: false),
                CourseId = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                Status = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Classrooms", x => x.ClassroomId);
                table.ForeignKey(
                    name: "FK_Classrooms_Courses_CourseId",
                    column: x => x.CourseId,
                    principalTable: "Courses",
                    principalColumn: "CourseId");
                table.ForeignKey(
                    name: "FK_Classrooms_Trainers_TrainerId",
                    column: x => x.TrainerId,
                    principalTable: "Trainers",
                    principalColumn: "TrainerId");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Classrooms_CourseId",
            table: "Classrooms",
            column: "CourseId");

        migrationBuilder.CreateIndex(
            name: "IX_Classrooms_TrainerId",
            table: "Classrooms",
            column: "TrainerId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Classrooms");
    }
}

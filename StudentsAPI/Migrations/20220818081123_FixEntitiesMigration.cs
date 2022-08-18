using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsAPI.Migrations
{
    public partial class FixEntitiesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UniversityStudents");

            migrationBuilder.CreateTable(
                name: "StudentUniversity",
                columns: table => new
                {
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniversitiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUniversity", x => new { x.StudentsId, x.UniversitiesId });
                    table.ForeignKey(
                        name: "FK_StudentUniversity_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentUniversity_Universities_UniversitiesId",
                        column: x => x.UniversitiesId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentUniversity_UniversitiesId",
                table: "StudentUniversity",
                column: "UniversitiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentUniversity");

            migrationBuilder.CreateTable(
                name: "UniversityStudents",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniversityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityStudents", x => new { x.StudentId, x.UniversityId });
                    table.ForeignKey(
                        name: "FK_UniversityStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UniversityStudents_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UniversityStudents_UniversityId",
                table: "UniversityStudents",
                column: "UniversityId");
        }
    }
}

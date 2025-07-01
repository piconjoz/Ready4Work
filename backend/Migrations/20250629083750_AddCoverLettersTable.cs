using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCoverLettersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cover_letter",
                table: "JobApplications");

            migrationBuilder.AddColumn<int>(
                name: "cover_letter_id",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CoverLetters",
                columns: table => new
                {
                    cover_letter_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    applicant_id = table.Column<int>(type: "int", nullable: false),
                    cover_letter_path = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    original_text = table.Column<string>(type: "longtext", nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    content_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverLetters", x => x.cover_letter_id);
                    table.ForeignKey(
                        name: "FK_CoverLetters_Applicants_applicant_id",
                        column: x => x.applicant_id,
                        principalTable: "Applicants",
                        principalColumn: "applicant_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    resume_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    applicant_id = table.Column<int>(type: "int", nullable: false),
                    resume_path = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    resume_text = table.Column<string>(type: "longtext", nullable: false),
                    uploaded_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.resume_id);
                    table.ForeignKey(
                        name: "FK_Resumes_Applicants_applicant_id",
                        column: x => x.applicant_id,
                        principalTable: "Applicants",
                        principalColumn: "applicant_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_cover_letter_id",
                table: "JobApplications",
                column: "cover_letter_id");

            migrationBuilder.CreateIndex(
                name: "IX_CoverLetters_applicant_id",
                table: "CoverLetters",
                column: "applicant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_applicant_id",
                table: "Resumes",
                column: "applicant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_CoverLetters_cover_letter_id",
                table: "JobApplications",
                column: "cover_letter_id",
                principalTable: "CoverLetters",
                principalColumn: "cover_letter_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_CoverLetters_cover_letter_id",
                table: "JobApplications");

            migrationBuilder.DropTable(
                name: "CoverLetters");

            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_cover_letter_id",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "cover_letter_id",
                table: "JobApplications");

            migrationBuilder.AddColumn<string>(
                name: "cover_letter",
                table: "JobApplications",
                type: "longtext",
                nullable: false);
        }
    }
}

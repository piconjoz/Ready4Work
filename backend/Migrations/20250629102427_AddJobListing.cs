using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddJobListing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobListings",
                columns: table => new
                {
                    job_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    recruiter_id = table.Column<int>(type: "int", nullable: false),
                    job_requirements = table.Column<string>(type: "longtext", nullable: false),
                    job_description = table.Column<string>(type: "longtext", nullable: false),
                    listing_name = table.Column<string>(type: "longtext", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    max_vacancies = table.Column<int>(type: "int", nullable: false),
                    is_visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    renumeration_type = table.Column<string>(type: "longtext", nullable: false),
                    job_duration = table.Column<string>(type: "longtext", nullable: false),
                    rate = table.Column<float>(type: "float", nullable: false),
                    working_hours = table.Column<string>(type: "longtext", nullable: false),
                    job_scheme = table.Column<string>(type: "longtext", nullable: false),
                    permitted_qualifications = table.Column<int>(type: "int", nullable: false),
                    skillsets = table.Column<int>(type: "int", nullable: false),
                    job_status = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobListings", x => x.job_id);
                    table.ForeignKey(
                        name: "FK_JobListings_Recruiters_recruiter_id",
                        column: x => x.recruiter_id,
                        principalTable: "Recruiters",
                        principalColumn: "recruiter_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_JobListings_recruiter_id",
                table: "JobListings",
                column: "recruiter_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobListings");
        }
    }
}

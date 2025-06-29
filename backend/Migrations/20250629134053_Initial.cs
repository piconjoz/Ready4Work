using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    PreferredCompanyName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    CompanyDescription = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    CountryOfBusinessRegistration = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    UEN = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    NumberOfEmployees = table.Column<int>(type: "int", nullable: false),
                    IndustryCluster = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EntityType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    AuthorisedTrainingOrganisation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CompanyWebsite = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    CompanyContact = table.Column<string>(type: "longtext", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: false),
                    State = table.Column<string>(type: "longtext", nullable: false),
                    ZoneLocation = table.Column<string>(type: "longtext", nullable: false),
                    CountryCode = table.Column<int>(type: "int", nullable: false),
                    UnitNumber = table.Column<string>(type: "longtext", nullable: false),
                    Floor = table.Column<string>(type: "longtext", nullable: false),
                    AreaCode = table.Column<int>(type: "int", nullable: false),
                    Block = table.Column<string>(type: "longtext", nullable: false),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    EmploymentType = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    programme_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    programme_name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.programme_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    skill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    skill = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.skill_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    first_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    gender = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    profile_picture_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    salt = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    user_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.admin_id);
                    table.ForeignKey(
                        name: "FK_Admins_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    applicant_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    programme_id = table.Column<int>(type: "int", nullable: false),
                    admit_year = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.applicant_id);
                    table.ForeignKey(
                        name: "FK_Applicants_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recruiters",
                columns: table => new
                {
                    recruiter_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    job_title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    department = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiters", x => x.recruiter_id);
                    table.ForeignKey(
                        name: "FK_Recruiters_Companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruiters_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    refresh_token_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    token = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_revoked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    revoked_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.refresh_token_id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "JobSkills",
                columns: table => new
                {
                    job_skill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    job_id = table.Column<int>(type: "int", nullable: false),
                    skill_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkills", x => x.job_skill_id);
                    table.ForeignKey(
                        name: "FK_JobSkills_JobListings_job_id",
                        column: x => x.job_id,
                        principalTable: "JobListings",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkills_Skills_skill_id",
                        column: x => x.skill_id,
                        principalTable: "Skills",
                        principalColumn: "skill_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    qualification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    programme_id = table.Column<int>(type: "int", nullable: false),
                    job_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.qualification_id);
                    table.ForeignKey(
                        name: "FK_Qualifications_JobListings_job_id",
                        column: x => x.job_id,
                        principalTable: "JobListings",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Qualifications_Programmes_programme_id",
                        column: x => x.programme_id,
                        principalTable: "Programmes",
                        principalColumn: "programme_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_user_id",
                table: "Admins",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_user_id",
                table: "Applicants",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UEN",
                table: "Companies",
                column: "UEN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobListings_recruiter_id",
                table: "JobListings",
                column: "recruiter_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_job_id",
                table: "JobSkills",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_skill_id",
                table: "JobSkills",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_job_id",
                table: "Qualifications",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_programme_id",
                table: "Qualifications",
                column: "programme_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_company_id",
                table: "Recruiters",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_user_id",
                table: "Recruiters",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_expires_at",
                table: "RefreshTokens",
                column: "expires_at");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_token",
                table: "RefreshTokens",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_user_id",
                table: "RefreshTokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                table: "Users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropTable(
                name: "JobSkills");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "JobListings");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "Recruiters");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

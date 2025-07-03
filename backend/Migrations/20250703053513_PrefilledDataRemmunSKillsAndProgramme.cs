using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class PrefilledDataRemmunSKillsAndProgramme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Programmes",
                columns: new[] { "programme_id", "programme_name" },
                values: new object[,]
                {
                    { 1, "Accountancy" },
                    { 2, "Aircraft Systems Engineering" },
                    { 3, "Applied Artificial Intelligence" },
                    { 4, "Applied Computing (Fintech)" },
                    { 5, "Applied Computing (Stackable Micro-credential Pathway)" },
                    { 6, "Aviation Management" },
                    { 7, "Business and Infocomm Technology" },
                    { 8, "Chemical Engineering" },
                    { 9, "Civil Engineering" },
                    { 10, "Communication and Digital Media" },
                    { 11, "Computer Engineering" },
                    { 12, "Computer Science in Interactive Media and Game Development" },
                    { 13, "Computer Science in Real-Time Interactive Simulation" },
                    { 14, "Computing Science" },
                    { 15, "Diagnostic Radiography" },
                    { 16, "Dietetics and Nutrition" },
                    { 17, "Digital Art and Animation" },
                    { 18, "Digital Supply Chain" },
                    { 19, "Electrical and Electronic Engineering" },
                    { 20, "Electrical and Electronic Engineering (Stackable Micro-credential Pathway)" },
                    { 21, "Electrical Power Engineering" },
                    { 22, "Electronics and Data Engineering" },
                    { 23, "Engineering Systems" },
                    { 24, "Food Business Management (Baking and Pastry Arts)" },
                    { 25, "Food Business Management (Culinary Arts)" },
                    { 26, "Food Technology" },
                    { 27, "Hospitality and Tourism Management" },
                    { 28, "Information and Communications Technology (Information Security)" },
                    { 29, "Information and Communications Technology (Software Engineering)" },
                    { 30, "Infrastructure and Systems Engineering" },
                    { 31, "Infrastructure and Systems Engineering (Stackable Micro-credential Pathway)" },
                    { 32, "Mechanical Design and Manufacturing Engineering" },
                    { 33, "Mechanical Engineering" },
                    { 34, "Naval Architecture and Marine Engineering" },
                    { 35, "Nursing (Post-registration)" },
                    { 36, "Nursing (Pre-registration and Specialty Training)" },
                    { 37, "Occupational Therapy" },
                    { 38, "Pharmaceutical Engineering" },
                    { 39, "Physiotherapy" },
                    { 40, "Radiation Therapy" },
                    { 41, "Robotics Systems" }
                });

            migrationBuilder.InsertData(
                table: "RemunerationTypes",
                columns: new[] { "remnmeration_id", "type" },
                values: new object[,]
                {
                    { 1, "Hourly" },
                    { 2, "Monthly" },
                    { 3, "Project-based" },
                    { 4, "Contract-based" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "skill_id", "skill" },
                values: new object[,]
                {
                    { 1, "NetSuite Proficency" },
                    { 2, "Java Proficency" },
                    { 3, "Auditing Proficiency" },
                    { 4, "Oracle Proficiency" },
                    { 5, "Microsoft Excel Proficiency" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Programmes",
                keyColumn: "programme_id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "RemunerationTypes",
                keyColumn: "remnmeration_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RemunerationTypes",
                keyColumn: "remnmeration_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RemunerationTypes",
                keyColumn: "remnmeration_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RemunerationTypes",
                keyColumn: "remnmeration_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "skill_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "skill_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "skill_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "skill_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "skill_id",
                keyValue: 5);
        }
    }
}

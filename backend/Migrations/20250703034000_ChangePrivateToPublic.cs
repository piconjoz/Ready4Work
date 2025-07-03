using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrivateToPublic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentProfiles",
                columns: table => new
                {
                    student_profile_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nric_fin = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    nationality = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    admit_year = table.Column<int>(type: "int", nullable: false),
                    primary_contact_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    gender = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    degree_programme = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    full_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfiles", x => x.student_profile_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "StudentProfiles",
                columns: new[] { "student_profile_id", "admit_year", "degree_programme", "email", "full_name", "gender", "nationality", "nric_fin", "primary_contact_number", "student_id" },
                values: new object[,]
                {
                    { 1, 2020, "BA in User Experience and Game Design", "2941985@sit.singaporetech.edu.sg", "Alice Tan", "Male", "Singapore", "T01234567", "94304313", 2941985 },
                    { 2, 2021, "BSc in Computer Science", "230230@sit.singaporetech.edu.sg", "Bob Lim", "Female", "Singapore", "T01654321", "91234567", 230230 },
                    { 3, 2021, "Information and Communications Technology (Software Engineering)", "2301886@sit.singaporetech.edu.sg", "Marcus Foo", "Male", "Singapore", "T0000000A", "90737044", 2301886 },
                    { 4, 2021, "Information and Communications Technology (Information Security)", "2302221@sit.singaporetech.edu.sg", "Hariz Darwisy Bin Adan", "Male", "Singapore", "T1111111B", "99001344", 2302221 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_student_id",
                table: "StudentProfiles",
                column: "student_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentProfiles");
        }
    }
}

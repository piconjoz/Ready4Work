// using Microsoft.EntityFrameworkCore.Migrations;

// #nullable disable

// namespace backend.Migrations
// {
//     /// <inheritdoc />
//     public partial class FixedNamingConventions : Migration
//     {
//         /// <inheritdoc />
//         protected override void Up(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.RenameColumn(
//                 name: "renumeration_type",
//                 table: "JobListings",
//                 newName: "remuneration_type");

//             migrationBuilder.RenameColumn(
//                 name: "job_listing_id",
//                 table: "JobApplications",
//                 newName: "job_id");

//             migrationBuilder.RenameIndex(
//                 name: "IX_JobApplications_applicant_id_job_listing_id",
//                 table: "JobApplications",
//                 newName: "IX_JobApplications_applicant_id_job_id");

//             migrationBuilder.CreateIndex(
//                 name: "IX_JobApplications_job_id",
//                 table: "JobApplications",
//                 column: "job_id");

//             migrationBuilder.AddForeignKey(
//                 name: "FK_JobApplications_JobListings_job_id",
//                 table: "JobApplications",
//                 column: "job_id",
//                 principalTable: "JobListings",
//                 principalColumn: "job_id",
//                 onDelete: ReferentialAction.Cascade);
//         }

//         /// <inheritdoc />
//         protected override void Down(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.DropForeignKey(
//                 name: "FK_JobApplications_JobListings_job_id",
//                 table: "JobApplications");

//             migrationBuilder.DropIndex(
//                 name: "IX_JobApplications_job_id",
//                 table: "JobApplications");

//             migrationBuilder.RenameColumn(
//                 name: "remuneration_type",
//                 table: "JobListings",
//                 newName: "renumeration_type");

//             migrationBuilder.RenameColumn(
//                 name: "job_id",
//                 table: "JobApplications",
//                 newName: "job_listing_id");

//             migrationBuilder.RenameIndex(
//                 name: "IX_JobApplications_applicant_id_job_id",
//                 table: "JobApplications",
//                 newName: "IX_JobApplications_applicant_id_job_listing_id");
//         }
//     }
// }
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixedNamingConventions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Replace RenameColumn with raw SQL for MySQL
            migrationBuilder.Sql("ALTER TABLE `JobListings` CHANGE `renumeration_type` `remuneration_type` LONGTEXT NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE `JobApplications` CHANGE `job_listing_id` `job_id` INT NOT NULL;");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_applicant_id_job_listing_id",
                table: "JobApplications",
                newName: "IX_JobApplications_applicant_id_job_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_job_id",
                table: "JobApplications",
                column: "job_id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobListings_job_id",
                table: "JobApplications",
                column: "job_id",
                principalTable: "JobListings",
                principalColumn: "job_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobListings_job_id",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_job_id",
                table: "JobApplications");

            // Reverse column renames via raw SQL again
            migrationBuilder.Sql("ALTER TABLE `JobListings` CHANGE `remuneration_type` `renumeration_type` LONGTEXT NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE `JobApplications` CHANGE `job_id` `job_listing_id` INT NOT NULL;");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_applicant_id_job_id",
                table: "JobApplications",
                newName: "IX_JobApplications_applicant_id_job_listing_id");
        }
    }
}
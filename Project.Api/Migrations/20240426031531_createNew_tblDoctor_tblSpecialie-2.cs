using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Api.Migrations
{
    public partial class createNew_tblDoctor_tblSpecialie2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClinicsClinicID",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_ClinicsClinicID",
                table: "Services",
                column: "ClinicsClinicID");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Clinics_ClinicsClinicID",
                table: "Services",
                column: "ClinicsClinicID",
                principalTable: "Clinics",
                principalColumn: "ClinicID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Clinics_ClinicsClinicID",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ClinicsClinicID",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ClinicsClinicID",
                table: "Services");
        }
    }
}

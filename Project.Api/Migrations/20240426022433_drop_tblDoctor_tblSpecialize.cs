using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Api.Migrations
{
    public partial class drop_tblDoctor_tblSpecialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentHistory_Specialties_SpecialtiesDoctorId",
                table: "AppointmentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Specialties_SpecialtiesDoctorId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Specialties",
                newName: "SpecialtyID");

            migrationBuilder.RenameColumn(
                name: "SpecialtiesDoctorId",
                table: "Appointments",
                newName: "SpecialtiesSpecialtyID");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_SpecialtiesDoctorId",
                table: "Appointments",
                newName: "IX_Appointments_SpecialtiesSpecialtyID");

            migrationBuilder.RenameColumn(
                name: "SpecialtiesDoctorId",
                table: "AppointmentHistory",
                newName: "SpecialtiesSpecialtyID");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentHistory_SpecialtiesDoctorId",
                table: "AppointmentHistory",
                newName: "IX_AppointmentHistory_SpecialtiesSpecialtyID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentHistory_Specialties_SpecialtiesSpecialtyID",
                table: "AppointmentHistory",
                column: "SpecialtiesSpecialtyID",
                principalTable: "Specialties",
                principalColumn: "SpecialtyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Specialties_SpecialtiesSpecialtyID",
                table: "Appointments",
                column: "SpecialtiesSpecialtyID",
                principalTable: "Specialties",
                principalColumn: "SpecialtyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentHistory_Specialties_SpecialtiesSpecialtyID",
                table: "AppointmentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Specialties_SpecialtiesSpecialtyID",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "SpecialtyID",
                table: "Specialties",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "SpecialtiesSpecialtyID",
                table: "Appointments",
                newName: "SpecialtiesDoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_SpecialtiesSpecialtyID",
                table: "Appointments",
                newName: "IX_Appointments_SpecialtiesDoctorId");

            migrationBuilder.RenameColumn(
                name: "SpecialtiesSpecialtyID",
                table: "AppointmentHistory",
                newName: "SpecialtiesDoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentHistory_SpecialtiesSpecialtyID",
                table: "AppointmentHistory",
                newName: "IX_AppointmentHistory_SpecialtiesDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentHistory_Specialties_SpecialtiesDoctorId",
                table: "AppointmentHistory",
                column: "SpecialtiesDoctorId",
                principalTable: "Specialties",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Specialties_SpecialtiesDoctorId",
                table: "Appointments",
                column: "SpecialtiesDoctorId",
                principalTable: "Specialties",
                principalColumn: "DoctorId");
        }
    }
}

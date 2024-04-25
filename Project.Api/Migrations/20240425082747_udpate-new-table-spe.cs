using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Api.Migrations
{
    public partial class udpatenewtablespe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialtyID",
                table: "Specialties");

            migrationBuilder.RenameColumn(
                name: "DoctorName",
                table: "Specialties",
                newName: "SpecialtyName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecialtyName",
                table: "Specialties",
                newName: "DoctorName");

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyID",
                table: "Specialties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

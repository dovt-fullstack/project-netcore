using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Api.Migrations
{
    public partial class fixmodelapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Services_ServicesServiceId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ServicesServiceId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ServicesServiceId",
                table: "Appointments");

            migrationBuilder.CreateTable(
                name: "AppointmentsServices",
                columns: table => new
                {
                    AppointmentsId = table.Column<int>(type: "int", nullable: false),
                    ServicesServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsServices", x => new { x.AppointmentsId, x.ServicesServiceId });
                    table.ForeignKey(
                        name: "FK_AppointmentsServices_Appointments_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentsServices_Services_ServicesServiceId",
                        column: x => x.ServicesServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsServices_ServicesServiceId",
                table: "AppointmentsServices",
                column: "ServicesServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentsServices");

            migrationBuilder.AddColumn<int>(
                name: "ServicesServiceId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServicesServiceId",
                table: "Appointments",
                column: "ServicesServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Services_ServicesServiceId",
                table: "Appointments",
                column: "ServicesServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId");
        }
    }
}

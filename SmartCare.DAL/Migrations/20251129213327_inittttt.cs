using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCare.DAL.Migrations
{
    /// <inheritdoc />
    public partial class inittttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MedicalRecordId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MedicalRecordId",
                table: "Appointments");
        }
    }
}

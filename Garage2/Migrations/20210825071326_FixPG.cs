using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2.Migrations
{
    public partial class FixPG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarageSlot");

            migrationBuilder.AddColumn<string>(
                name: "SlotsInUse",
                table: "ParkedVehicle",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlotsInUse",
                table: "ParkedVehicle");

            migrationBuilder.CreateTable(
                name: "GarageSlot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockId = table.Column<int>(type: "int", nullable: false),
                    InUse = table.Column<int>(type: "int", nullable: false),
                    SlotId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarageSlot", x => x.Id);
                });
        }
    }
}

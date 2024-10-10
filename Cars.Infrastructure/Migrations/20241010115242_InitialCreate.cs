using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    DoorsNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    LuggageCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    EngineCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    FuelType = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CarFuelConsumption = table.Column<double>(type: "REAL", nullable: false),
                    BodyType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSupplier.DataAccess.MSSQL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarDealerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxNumberOfCars = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDealerships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarDealershipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarDealerships_CarDealershipId",
                        column: x => x.CarDealershipId,
                        principalTable: "CarDealerships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarDealerships",
                columns: new[] { "Id", "MaxNumberOfCars", "Name" },
                values: new object[] { 1, 10, "Автосалон 1" });

            migrationBuilder.InsertData(
                table: "CarDealerships",
                columns: new[] { "Id", "MaxNumberOfCars", "Name" },
                values: new object[] { 2, 10, "Автосалон 2" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "CarDealershipId", "Color" },
                values: new object[,]
                {
                    { 1, "Mercedes", 1, "серая" },
                    { 2, "Mercedes", 1, "оранжевая" },
                    { 3, "Bmw", 1, "серая" },
                    { 4, "Bmw", 1, "оранжевая" },
                    { 5, "Mercedes", 2, "серая" },
                    { 6, "Mercedes", 2, "оранжевая" },
                    { 7, "Bmw", 2, "серая" },
                    { 8, "Bmw", 2, "оранжевая" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarDealershipId",
                table: "Cars",
                column: "CarDealershipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarDealerships");
        }
    }
}

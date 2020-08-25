using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeseriesDatabase.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Location = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildingDataField",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingDataField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildingObject",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingObject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reading",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingEntityId = table.Column<short>(type: "smallint", nullable: false),
                    DataFieldEntityId = table.Column<byte>(type: "tinyint", nullable: false),
                    ObjectEntityId = table.Column<byte>(type: "tinyint", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reading", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reading_BuildingEntityId",
                table: "Reading",
                column: "BuildingEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Reading_DataFieldEntityId",
                table: "Reading",
                column: "DataFieldEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Reading_ObjectEntityId",
                table: "Reading",
                column: "ObjectEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Reading_Timestamp",
                table: "Reading",
                column: "Timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "BuildingDataField");

            migrationBuilder.DropTable(
                name: "BuildingObject");

            migrationBuilder.DropTable(
                name: "Reading");
        }
    }
}

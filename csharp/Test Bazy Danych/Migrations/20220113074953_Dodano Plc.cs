using Microsoft.EntityFrameworkCore.Migrations;

namespace Test_Bazy_Danych.Migrations
{
    public partial class DodanoPlc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlecId",
                table: "Osoby",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Plcie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plcie", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Osoby_PlecId",
                table: "Osoby",
                column: "PlecId");

            migrationBuilder.AddForeignKey(
                name: "FK_Osoby_Plcie_PlecId",
                table: "Osoby",
                column: "PlecId",
                principalTable: "Plcie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Osoby_Plcie_PlecId",
                table: "Osoby");

            migrationBuilder.DropTable(
                name: "Plcie");

            migrationBuilder.DropIndex(
                name: "IX_Osoby_PlecId",
                table: "Osoby");

            migrationBuilder.DropColumn(
                name: "PlecId",
                table: "Osoby");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppLibros.Migrations
{
    public partial class cambio3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDeNacimiento",
                table: "Autor",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDeNacimiento",
                table: "Autor");
        }
    }
}

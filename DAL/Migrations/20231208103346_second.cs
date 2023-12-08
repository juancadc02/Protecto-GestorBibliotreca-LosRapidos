using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "token_recuperacion",
                table: "Usuarios",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id_usuario",
                keyValue: 1,
                columns: new[] { "fch_alta_usuario", "token_recuperacion" },
                values: new object[] { new DateTime(2023, 12, 8, 10, 33, 45, 958, DateTimeKind.Utc).AddTicks(9021), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token_recuperacion",
                table: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id_usuario",
                keyValue: 1,
                column: "fch_alta_usuario",
                value: new DateTime(2023, 11, 23, 12, 43, 58, 924, DateTimeKind.Utc).AddTicks(8618));
        }
    }
}

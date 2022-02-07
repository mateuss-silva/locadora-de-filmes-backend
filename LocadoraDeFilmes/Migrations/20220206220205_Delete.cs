using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProvaTecnicaEAuditoria.Migrations
{
    public partial class Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_idx",
                table: "Locacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Filme_idx",
                table: "Locacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_idx",
                table: "Locacao",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Filme_idx",
                table: "Locacao",
                column: "FilmeId",
                principalTable: "Filme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_idx",
                table: "Locacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Filme_idx",
                table: "Locacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_idx",
                table: "Locacao",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Filme_idx",
                table: "Locacao",
                column: "FilmeId",
                principalTable: "Filme",
                principalColumn: "Id");
        }
    }
}

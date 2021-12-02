using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Data.Migrations
{
    public partial class AdicionaColunaRemovido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Usuario",
                newName: "Nome");

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "Usuario",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Removido",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Usuario",
                newName: "Name");
        }
    }
}

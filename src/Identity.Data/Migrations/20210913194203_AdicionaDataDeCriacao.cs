using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Data.Migrations
{
    public partial class AdicionaDataDeCriacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeCriacao",
                table: "UsuarioPermissao",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeCriacao",
                table: "UsuarioPerfil",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeCriacao",
                table: "Usuario",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeCriacao",
                table: "PerfilPermissao",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeCriacao",
                table: "Perfil",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDeCriacao",
                table: "UsuarioPermissao");

            migrationBuilder.DropColumn(
                name: "DataDeCriacao",
                table: "UsuarioPerfil");

            migrationBuilder.DropColumn(
                name: "DataDeCriacao",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "DataDeCriacao",
                table: "PerfilPermissao");

            migrationBuilder.DropColumn(
                name: "DataDeCriacao",
                table: "Perfil");
        }
    }
}

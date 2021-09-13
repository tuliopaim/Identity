using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Data.Migrations
{
    public partial class AdicionaDataDeAtualizacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeAtualizacao",
                table: "UsuarioPermissao",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeAtualizacao",
                table: "UsuarioPerfil",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeAtualizacao",
                table: "Usuario",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeAtualizacao",
                table: "PerfilPermissao",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeAtualizacao",
                table: "Perfil",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDeAtualizacao",
                table: "UsuarioPermissao");

            migrationBuilder.DropColumn(
                name: "DataDeAtualizacao",
                table: "UsuarioPerfil");

            migrationBuilder.DropColumn(
                name: "DataDeAtualizacao",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "DataDeAtualizacao",
                table: "PerfilPermissao");

            migrationBuilder.DropColumn(
                name: "DataDeAtualizacao",
                table: "Perfil");
        }
    }
}

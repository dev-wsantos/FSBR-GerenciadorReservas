﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorReservas.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNovoCampoEntityReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TokenConfirmacao",
                table: "Reservas",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenConfirmacao",
                table: "Reservas");
        }
    }
}

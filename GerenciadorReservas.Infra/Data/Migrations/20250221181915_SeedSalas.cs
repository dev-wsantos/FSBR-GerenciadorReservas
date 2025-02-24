using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorReservas.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedSalas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Salas (Nome, Capacidade) VALUES ('Sala 01', 10)");
            migrationBuilder
                .Sql("INSERT INTO Salas (Nome, Capacidade) VALUES ('Sala 02', 20)");
            migrationBuilder
                .Sql("INSERT INTO Salas (Nome, Capacidade) VALUES ('Sala 03', 30)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Salas");
        }
    }
}

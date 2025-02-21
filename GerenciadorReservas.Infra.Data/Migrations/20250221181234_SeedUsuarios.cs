using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorReservas.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Usuarios (Nome, Email) VALUES ('Teste 01', 'dev.wsantos@gmail.com')");
            migrationBuilder
                .Sql("INSERT INTO Usuarios (Nome, Email) VALUES ('Teste 02', 'dev.wsantos@outlook.com')");
            migrationBuilder
                .Sql("INSERT INTO Usuarios (Nome, Email) VALUES ('Teste 03', 'wellington.bezerra.santos@outlook.com')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Usuarios");
        }
    }
}

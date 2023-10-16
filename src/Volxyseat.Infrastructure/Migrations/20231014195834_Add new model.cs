using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volxyseat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addnewmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Clients",
                newName: "Cpf");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Clients",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Clients",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Clients",
                newName: "CPF");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Clients",
                newName: "Nome");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace minimal.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdministrador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Adiminstradors",
                columns: new[] { "Id", "Email", "Perfil", "Senha" },
                values: new object[] { 1, "rinaldo3uchoa@gmail.com", "Rinaldo", "123456" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Adiminstradors",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGInterprise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEsPredeterminadoToAlmacen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsPredeterminado",
                table: "Almacenes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsPredeterminado",
                table: "Almacenes");
        }
    }
}

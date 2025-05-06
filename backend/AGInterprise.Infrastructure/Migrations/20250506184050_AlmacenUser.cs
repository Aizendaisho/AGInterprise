using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGInterprise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlmacenUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Almacenes_AlmacenId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "NombreCompleto",
                table: "AspNetUsers",
                newName: "Nombre");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Almacenes_AlmacenId",
                table: "AspNetUsers",
                column: "AlmacenId",
                principalTable: "Almacenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Almacenes_AlmacenId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "AspNetUsers",
                newName: "NombreCompleto");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Almacenes_AlmacenId",
                table: "AspNetUsers",
                column: "AlmacenId",
                principalTable: "Almacenes",
                principalColumn: "Id");
        }
    }
}

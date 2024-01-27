using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noticias_Proyecto_1_Final.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaNoticias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "noticias",
                columns: new[] { "IdNoticia", "Autor", "Categoria", "Contenido", "Enlace", "Fecha", "Fuente", "Pais", "Titulo" },
                values: new object[] { 1, "Anthony Sanchez", "Farandula", "Se murio joven", "Idk", new DateTime(2023, 12, 14, 22, 36, 16, 323, DateTimeKind.Local).AddTicks(3479), "sabra Dios", "Republica Dominicana", " Muere joven de 18" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "noticias",
                keyColumn: "IdNoticia",
                keyValue: 1);
        }
    }
}

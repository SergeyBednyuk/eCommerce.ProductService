using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace eCommerce.ProductService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // This is correct: It adds the column to the existing table
            migrationBuilder.AddColumn<DateTime>(
                name: "RowVersion",
                table: "products",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // FIXED: Do NOT drop the table. Only drop the column you added.
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "products");
        }
    }
}

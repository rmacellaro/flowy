using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class fixconfingType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Modelling",
                table: "NodeConfigs");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Modelling",
                table: "LinkConfigs");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Modelling",
                table: "ActivityConfigs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Modelling",
                table: "NodeConfigs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Modelling",
                table: "LinkConfigs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Modelling",
                table: "ActivityConfigs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

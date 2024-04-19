using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class fixDataTypeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                schema: "Modelling",
                table: "NodeDataTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                schema: "Modelling",
                table: "ActivityDefinitionDataTypes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                schema: "Modelling",
                table: "NodeDataTypes");

            migrationBuilder.DropColumn(
                name: "Index",
                schema: "Modelling",
                table: "ActivityDefinitionDataTypes");
        }
    }
}

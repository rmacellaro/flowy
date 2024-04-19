using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class fixActivityDefinition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ActivityDefinitions",
                newName: "ActivityDefinitions",
                newSchema: "Modelling");

            migrationBuilder.RenameTable(
                name: "ActivityDefinitionDataTypes",
                newName: "ActivityDefinitionDataTypes",
                newSchema: "Modelling");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ActivityDefinitions",
                schema: "Modelling",
                newName: "ActivityDefinitions");

            migrationBuilder.RenameTable(
                name: "ActivityDefinitionDataTypes",
                schema: "Modelling",
                newName: "ActivityDefinitionDataTypes");
        }
    }
}

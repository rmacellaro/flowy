using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Camunda.Migrations
{
    /// <inheritdoc />
    public partial class fixinteraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "InteractionTracks",
                schema: "Processing",
                newName: "InteractionTracks",
                newSchema: "Modelling");

            migrationBuilder.RenameTable(
                name: "Interactions",
                schema: "Processing",
                newName: "Interactions",
                newSchema: "Modelling");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "InteractionTracks",
                schema: "Modelling",
                newName: "InteractionTracks",
                newSchema: "Processing");

            migrationBuilder.RenameTable(
                name: "Interactions",
                schema: "Modelling",
                newName: "Interactions",
                newSchema: "Processing");
        }
    }
}

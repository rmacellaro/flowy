using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class fixActivityDefHasFrontEnd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasFrontEnd",
                schema: "Modelling",
                table: "ActivityDefinitions",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasFrontEnd",
                schema: "Modelling",
                table: "ActivityDefinitions");
        }
    }
}

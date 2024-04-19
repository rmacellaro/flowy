using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class fixConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdNode",
                schema: "Modelling",
                table: "Configurations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_IdNode",
                schema: "Modelling",
                table: "Configurations",
                column: "IdNode");

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_Nodes_IdNode",
                schema: "Modelling",
                table: "Configurations",
                column: "IdNode",
                principalSchema: "Modelling",
                principalTable: "Nodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_Nodes_IdNode",
                schema: "Modelling",
                table: "Configurations");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_IdNode",
                schema: "Modelling",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "IdNode",
                schema: "Modelling",
                table: "Configurations");
        }
    }
}

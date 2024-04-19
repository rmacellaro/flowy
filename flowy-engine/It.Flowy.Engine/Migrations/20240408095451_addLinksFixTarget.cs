using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class addLinksFixTarget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Links_IdTargetNode",
                schema: "Modelling",
                table: "Links",
                column: "IdTargetNode");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Nodes_IdTargetNode",
                schema: "Modelling",
                table: "Links",
                column: "IdTargetNode",
                principalSchema: "Modelling",
                principalTable: "Nodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Nodes_IdTargetNode",
                schema: "Modelling",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Links_IdTargetNode",
                schema: "Modelling",
                table: "Links");
        }
    }
}

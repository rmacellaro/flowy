using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class addLinksFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Nodes_NodeId",
                schema: "Modelling",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Links_NodeId",
                schema: "Modelling",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "NodeId",
                schema: "Modelling",
                table: "Links");

            migrationBuilder.CreateIndex(
                name: "IX_Links_IdSourceNode",
                schema: "Modelling",
                table: "Links",
                column: "IdSourceNode");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Nodes_IdSourceNode",
                schema: "Modelling",
                table: "Links",
                column: "IdSourceNode",
                principalSchema: "Modelling",
                principalTable: "Nodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Nodes_IdSourceNode",
                schema: "Modelling",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Links_IdSourceNode",
                schema: "Modelling",
                table: "Links");

            migrationBuilder.AddColumn<long>(
                name: "NodeId",
                schema: "Modelling",
                table: "Links",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Links_NodeId",
                schema: "Modelling",
                table: "Links",
                column: "NodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Nodes_NodeId",
                schema: "Modelling",
                table: "Links",
                column: "NodeId",
                principalSchema: "Modelling",
                principalTable: "Nodes",
                principalColumn: "Id");
        }
    }
}

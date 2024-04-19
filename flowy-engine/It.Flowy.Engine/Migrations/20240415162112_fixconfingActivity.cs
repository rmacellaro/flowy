using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class fixconfingActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityConfigs_Activities_ActivityId",
                schema: "Modelling",
                table: "ActivityConfigs");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityConfigs_Nodes_IdNode",
                schema: "Modelling",
                table: "ActivityConfigs");

            migrationBuilder.DropIndex(
                name: "IX_ActivityConfigs_ActivityId",
                schema: "Modelling",
                table: "ActivityConfigs");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                schema: "Modelling",
                table: "ActivityConfigs");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "Modelling",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "IdNode",
                schema: "Modelling",
                table: "ActivityConfigs",
                newName: "IdActivity");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityConfigs_IdNode",
                schema: "Modelling",
                table: "ActivityConfigs",
                newName: "IX_ActivityConfigs_IdActivity");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Modelling",
                table: "Activities",
                newName: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityConfigs_Activities_IdActivity",
                schema: "Modelling",
                table: "ActivityConfigs",
                column: "IdActivity",
                principalSchema: "Modelling",
                principalTable: "Activities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityConfigs_Activities_IdActivity",
                schema: "Modelling",
                table: "ActivityConfigs");

            migrationBuilder.RenameColumn(
                name: "IdActivity",
                schema: "Modelling",
                table: "ActivityConfigs",
                newName: "IdNode");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityConfigs_IdActivity",
                schema: "Modelling",
                table: "ActivityConfigs",
                newName: "IX_ActivityConfigs_IdNode");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "Modelling",
                table: "Activities",
                newName: "Name");

            migrationBuilder.AddColumn<long>(
                name: "ActivityId",
                schema: "Modelling",
                table: "ActivityConfigs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "Modelling",
                table: "Activities",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConfigs_ActivityId",
                schema: "Modelling",
                table: "ActivityConfigs",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityConfigs_Activities_ActivityId",
                schema: "Modelling",
                table: "ActivityConfigs",
                column: "ActivityId",
                principalSchema: "Modelling",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityConfigs_Nodes_IdNode",
                schema: "Modelling",
                table: "ActivityConfigs",
                column: "IdNode",
                principalSchema: "Modelling",
                principalTable: "Nodes",
                principalColumn: "Id");
        }
    }
}

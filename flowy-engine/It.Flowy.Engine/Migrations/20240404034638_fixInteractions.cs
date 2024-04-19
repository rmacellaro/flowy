using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class fixInteractions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_Interactions_IdInteraction",
                schema: "Modelling",
                table: "Configurations");

            migrationBuilder.DropTable(
                name: "Interactions",
                schema: "Modelling");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_IdInteraction",
                schema: "Modelling",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "Color",
                schema: "Modelling",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Percentage",
                schema: "Modelling",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "IdInteraction",
                schema: "Modelling",
                table: "Configurations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                schema: "Modelling",
                table: "Nodes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Percentage",
                schema: "Modelling",
                table: "Nodes",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdInteraction",
                schema: "Modelling",
                table: "Configurations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Interactions",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdNode = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interactions_Nodes_IdNode",
                        column: x => x.IdNode,
                        principalSchema: "Modelling",
                        principalTable: "Nodes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_IdInteraction",
                schema: "Modelling",
                table: "Configurations",
                column: "IdInteraction");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_IdNode",
                schema: "Modelling",
                table: "Interactions",
                column: "IdNode");

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_Interactions_IdInteraction",
                schema: "Modelling",
                table: "Configurations",
                column: "IdInteraction",
                principalSchema: "Modelling",
                principalTable: "Interactions",
                principalColumn: "Id");
        }
    }
}

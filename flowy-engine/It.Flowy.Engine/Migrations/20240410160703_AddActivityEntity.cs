using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Modelling",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "Modelling",
                table: "Nodes");

            migrationBuilder.CreateTable(
                name: "Activities",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdNode = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Index = table.Column<int>(type: "int", nullable: true),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Nodes_IdNode",
                        column: x => x.IdNode,
                        principalSchema: "Modelling",
                        principalTable: "Nodes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ActivityConfigs",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdNode = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActivityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityConfigs_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "Modelling",
                        principalTable: "Activities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivityConfigs_Nodes_IdNode",
                        column: x => x.IdNode,
                        principalSchema: "Modelling",
                        principalTable: "Nodes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_IdNode",
                schema: "Modelling",
                table: "Activities",
                column: "IdNode");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConfigs_ActivityId",
                schema: "Modelling",
                table: "ActivityConfigs",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConfigs_IdNode",
                schema: "Modelling",
                table: "ActivityConfigs",
                column: "IdNode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityConfigs",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "Activities",
                schema: "Modelling");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Modelling",
                table: "Nodes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "Modelling",
                table: "Nodes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

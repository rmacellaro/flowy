using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Modelling");

            migrationBuilder.EnsureSchema(
                name: "Processing");

            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Processes",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Distributions",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProcess = table.Column<long>(type: "bigint", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    State = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distributions_Processes_IdProcess",
                        column: x => x.IdProcess,
                        principalSchema: "Modelling",
                        principalTable: "Processes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Instances",
                schema: "Processing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdDistribution = table.Column<long>(type: "bigint", nullable: true),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instances_Distributions_IdDistribution",
                        column: x => x.IdDistribution,
                        principalSchema: "Modelling",
                        principalTable: "Distributions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Nodes",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdDistribution = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Percentage = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nodes_Distributions_IdDistribution",
                        column: x => x.IdDistribution,
                        principalSchema: "Modelling",
                        principalTable: "Distributions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Datas",
                schema: "Processing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdInstance = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Datas_Instances_IdInstance",
                        column: x => x.IdInstance,
                        principalSchema: "Processing",
                        principalTable: "Instances",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Interactions",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdNode = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Wires",
                schema: "Processing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdInstance = table.Column<long>(type: "bigint", nullable: true),
                    State = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdNode = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wires_Instances_IdInstance",
                        column: x => x.IdInstance,
                        principalSchema: "Processing",
                        principalTable: "Instances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Wires_Nodes_IdNode",
                        column: x => x.IdNode,
                        principalSchema: "Modelling",
                        principalTable: "Nodes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Configurations",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdInteraction = table.Column<long>(type: "bigint", nullable: true),
                    IsForProcessingOnly = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configurations_Interactions_IdInteraction",
                        column: x => x.IdInteraction,
                        principalSchema: "Modelling",
                        principalTable: "Interactions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tracks",
                schema: "Processing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdWire = table.Column<long>(type: "bigint", nullable: true),
                    Message = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Wires_IdWire",
                        column: x => x.IdWire,
                        principalSchema: "Processing",
                        principalTable: "Wires",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_IdInteraction",
                schema: "Modelling",
                table: "Configurations",
                column: "IdInteraction");

            migrationBuilder.CreateIndex(
                name: "IX_Datas_IdInstance",
                schema: "Processing",
                table: "Datas",
                column: "IdInstance");

            migrationBuilder.CreateIndex(
                name: "IX_Distributions_IdProcess",
                schema: "Modelling",
                table: "Distributions",
                column: "IdProcess");

            migrationBuilder.CreateIndex(
                name: "IX_Instances_IdDistribution",
                schema: "Processing",
                table: "Instances",
                column: "IdDistribution");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_IdNode",
                schema: "Modelling",
                table: "Interactions",
                column: "IdNode");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_IdDistribution",
                schema: "Modelling",
                table: "Nodes",
                column: "IdDistribution");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_IdWire",
                schema: "Processing",
                table: "Tracks",
                column: "IdWire");

            migrationBuilder.CreateIndex(
                name: "IX_Wires_IdInstance",
                schema: "Processing",
                table: "Wires",
                column: "IdInstance");

            migrationBuilder.CreateIndex(
                name: "IX_Wires_IdNode",
                schema: "Processing",
                table: "Wires",
                column: "IdNode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "Datas",
                schema: "Processing");

            migrationBuilder.DropTable(
                name: "Tracks",
                schema: "Processing");

            migrationBuilder.DropTable(
                name: "Interactions",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "Wires",
                schema: "Processing");

            migrationBuilder.DropTable(
                name: "Instances",
                schema: "Processing");

            migrationBuilder.DropTable(
                name: "Nodes",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "Distributions",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "Processes",
                schema: "Modelling");
        }
    }
}

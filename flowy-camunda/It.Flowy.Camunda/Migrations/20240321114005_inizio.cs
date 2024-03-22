using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Camunda.Migrations
{
    /// <inheritdoc />
    public partial class inizio : Migration
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
                name: "Scopes",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scopes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Drafts",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdScope = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Schema = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drafts_Scopes_IdScope",
                        column: x => x.IdScope,
                        principalSchema: "Modelling",
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Interactions",
                schema: "Processing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdScope = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interactions_Scopes_IdScope",
                        column: x => x.IdScope,
                        principalSchema: "Modelling",
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Processes",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdScope = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Version = table.Column<int>(type: "int", nullable: false),
                    BpmnProcessId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenantId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processes_Scopes_IdScope",
                        column: x => x.IdScope,
                        principalSchema: "Modelling",
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DraftTracks",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdDraft = table.Column<long>(type: "bigint", nullable: false),
                    EventAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserIdentifier = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Operation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchemaBackup = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftTracks_Drafts_IdDraft",
                        column: x => x.IdDraft,
                        principalSchema: "Modelling",
                        principalTable: "Drafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InteractionTracks",
                schema: "Processing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdInteraction = table.Column<long>(type: "bigint", nullable: false),
                    EventAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserIdentifier = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Operation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataBackup = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractionTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InteractionTracks_Interactions_IdInteraction",
                        column: x => x.IdInteraction,
                        principalSchema: "Processing",
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Instances",
                schema: "Processing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProcess = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Reference = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instances_Processes_IdProcess",
                        column: x => x.IdProcess,
                        principalSchema: "Modelling",
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InstanceDatas",
                schema: "Processing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdInsatnce = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstanceDatas_Instances_IdInsatnce",
                        column: x => x.IdInsatnce,
                        principalSchema: "Processing",
                        principalTable: "Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InstanceTraks",
                schema: "Processing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdInstance = table.Column<long>(type: "bigint", nullable: false),
                    EventAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Operation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceTraks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstanceTraks_Instances_IdInstance",
                        column: x => x.IdInstance,
                        principalSchema: "Processing",
                        principalTable: "Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_IdScope",
                schema: "Modelling",
                table: "Drafts",
                column: "IdScope");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTracks_IdDraft",
                schema: "Modelling",
                table: "DraftTracks",
                column: "IdDraft");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceDatas_IdInsatnce",
                schema: "Processing",
                table: "InstanceDatas",
                column: "IdInsatnce");

            migrationBuilder.CreateIndex(
                name: "IX_Instances_IdProcess",
                schema: "Processing",
                table: "Instances",
                column: "IdProcess");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceTraks_IdInstance",
                schema: "Processing",
                table: "InstanceTraks",
                column: "IdInstance");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_IdScope",
                schema: "Processing",
                table: "Interactions",
                column: "IdScope");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionTracks_IdInteraction",
                schema: "Processing",
                table: "InteractionTracks",
                column: "IdInteraction");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_IdScope",
                schema: "Modelling",
                table: "Processes",
                column: "IdScope");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DraftTracks",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "InstanceDatas",
                schema: "Processing");

            migrationBuilder.DropTable(
                name: "InstanceTraks",
                schema: "Processing");

            migrationBuilder.DropTable(
                name: "InteractionTracks",
                schema: "Processing");

            migrationBuilder.DropTable(
                name: "Drafts",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "Instances",
                schema: "Processing");

            migrationBuilder.DropTable(
                name: "Interactions",
                schema: "Processing");

            migrationBuilder.DropTable(
                name: "Processes",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "Scopes",
                schema: "Modelling");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class fixDataDefinitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityConfigs",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "Datas",
                schema: "Processing");

            migrationBuilder.DropTable(
                name: "LinkConfigs",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "NodeConfigs",
                schema: "Modelling");

            migrationBuilder.EnsureSchema(
                name: "Common");

            migrationBuilder.CreateTable(
                name: "DataDefinitions",
                schema: "Common",
                columns: table => new
                {
                    IdentifierName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Group = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EditSettings = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShowSettings = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DetailSettings = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataDefinitions", x => x.IdentifierName);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ActivityDatas",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdActivity = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityDatas_Activities_IdActivity",
                        column: x => x.IdActivity,
                        principalSchema: "Modelling",
                        principalTable: "Activities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivityDatas_DataDefinitions_Name",
                        column: x => x.Name,
                        principalSchema: "Common",
                        principalTable: "DataDefinitions",
                        principalColumn: "IdentifierName");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InstanceDatas",
                schema: "Processing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdInstance = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstanceDatas_DataDefinitions_Name",
                        column: x => x.Name,
                        principalSchema: "Common",
                        principalTable: "DataDefinitions",
                        principalColumn: "IdentifierName");
                    table.ForeignKey(
                        name: "FK_InstanceDatas_Instances_IdInstance",
                        column: x => x.IdInstance,
                        principalSchema: "Processing",
                        principalTable: "Instances",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NodeDatas",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdNode = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeDatas_DataDefinitions_Name",
                        column: x => x.Name,
                        principalSchema: "Common",
                        principalTable: "DataDefinitions",
                        principalColumn: "IdentifierName");
                    table.ForeignKey(
                        name: "FK_NodeDatas_Nodes_IdNode",
                        column: x => x.IdNode,
                        principalSchema: "Modelling",
                        principalTable: "Nodes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDatas_IdActivity",
                schema: "Modelling",
                table: "ActivityDatas",
                column: "IdActivity");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDatas_Name",
                schema: "Modelling",
                table: "ActivityDatas",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceDatas_IdInstance",
                schema: "Processing",
                table: "InstanceDatas",
                column: "IdInstance");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceDatas_Name",
                schema: "Processing",
                table: "InstanceDatas",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_NodeDatas_IdNode",
                schema: "Modelling",
                table: "NodeDatas",
                column: "IdNode");

            migrationBuilder.CreateIndex(
                name: "IX_NodeDatas_Name",
                schema: "Modelling",
                table: "NodeDatas",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityDatas",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "InstanceDatas",
                schema: "Processing");

            migrationBuilder.DropTable(
                name: "NodeDatas",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "DataDefinitions",
                schema: "Common");

            migrationBuilder.CreateTable(
                name: "ActivityConfigs",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdActivity = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityConfigs_Activities_IdActivity",
                        column: x => x.IdActivity,
                        principalSchema: "Modelling",
                        principalTable: "Activities",
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
                name: "LinkConfigs",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdLink = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkConfigs_Links_IdLink",
                        column: x => x.IdLink,
                        principalSchema: "Modelling",
                        principalTable: "Links",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NodeConfigs",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdNode = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeConfigs_Nodes_IdNode",
                        column: x => x.IdNode,
                        principalSchema: "Modelling",
                        principalTable: "Nodes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityConfigs_IdActivity",
                schema: "Modelling",
                table: "ActivityConfigs",
                column: "IdActivity");

            migrationBuilder.CreateIndex(
                name: "IX_Datas_IdInstance",
                schema: "Processing",
                table: "Datas",
                column: "IdInstance");

            migrationBuilder.CreateIndex(
                name: "IX_LinkConfigs_IdLink",
                schema: "Modelling",
                table: "LinkConfigs",
                column: "IdLink");

            migrationBuilder.CreateIndex(
                name: "IX_NodeConfigs_IdNode",
                schema: "Modelling",
                table: "NodeConfigs",
                column: "IdNode");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    /// <inheritdoc />
    public partial class fixDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDatas_DataDefinitions_Name",
                schema: "Modelling",
                table: "ActivityDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_InstanceDatas_DataDefinitions_Name",
                schema: "Processing",
                table: "InstanceDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_NodeDatas_DataDefinitions_Name",
                schema: "Modelling",
                table: "NodeDatas");

            migrationBuilder.DropTable(
                name: "DataDefinitions",
                schema: "Common");

            migrationBuilder.DropIndex(
                name: "IX_NodeDatas_Name",
                schema: "Modelling",
                table: "NodeDatas");

            migrationBuilder.DropIndex(
                name: "IX_InstanceDatas_Name",
                schema: "Processing",
                table: "InstanceDatas");

            migrationBuilder.DropIndex(
                name: "IX_ActivityDatas_Name",
                schema: "Modelling",
                table: "ActivityDatas");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Modelling",
                table: "NodeDatas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "IdNodeDataType",
                schema: "Modelling",
                table: "NodeDatas",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Processing",
                table: "InstanceDatas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Modelling",
                table: "ActivityDatas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "IdActivityDefinitionDataType",
                schema: "Modelling",
                table: "ActivityDatas",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdActivityDefinition",
                schema: "Modelling",
                table: "Activities",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityDefinitions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Group = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDefinitions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NodeDataTypes",
                schema: "Modelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
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
                    table.PrimaryKey("PK_NodeDataTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ActivityDefinitionDataTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdActivityDefinition = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true)
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
                    table.PrimaryKey("PK_ActivityDefinitionDataTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityDefinitionDataTypes_ActivityDefinitions_IdActivityDe~",
                        column: x => x.IdActivityDefinition,
                        principalTable: "ActivityDefinitions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NodeDatas_IdNodeDataType",
                schema: "Modelling",
                table: "NodeDatas",
                column: "IdNodeDataType");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDatas_IdActivityDefinitionDataType",
                schema: "Modelling",
                table: "ActivityDatas",
                column: "IdActivityDefinitionDataType");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_IdActivityDefinition",
                schema: "Modelling",
                table: "Activities",
                column: "IdActivityDefinition");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDefinitionDataTypes_IdActivityDefinition",
                table: "ActivityDefinitionDataTypes",
                column: "IdActivityDefinition");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ActivityDefinitions_IdActivityDefinition",
                schema: "Modelling",
                table: "Activities",
                column: "IdActivityDefinition",
                principalTable: "ActivityDefinitions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityDatas_ActivityDefinitionDataTypes_IdActivityDefiniti~",
                schema: "Modelling",
                table: "ActivityDatas",
                column: "IdActivityDefinitionDataType",
                principalTable: "ActivityDefinitionDataTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NodeDatas_NodeDataTypes_IdNodeDataType",
                schema: "Modelling",
                table: "NodeDatas",
                column: "IdNodeDataType",
                principalSchema: "Modelling",
                principalTable: "NodeDataTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ActivityDefinitions_IdActivityDefinition",
                schema: "Modelling",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDatas_ActivityDefinitionDataTypes_IdActivityDefiniti~",
                schema: "Modelling",
                table: "ActivityDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_NodeDatas_NodeDataTypes_IdNodeDataType",
                schema: "Modelling",
                table: "NodeDatas");

            migrationBuilder.DropTable(
                name: "ActivityDefinitionDataTypes");

            migrationBuilder.DropTable(
                name: "NodeDataTypes",
                schema: "Modelling");

            migrationBuilder.DropTable(
                name: "ActivityDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_NodeDatas_IdNodeDataType",
                schema: "Modelling",
                table: "NodeDatas");

            migrationBuilder.DropIndex(
                name: "IX_ActivityDatas_IdActivityDefinitionDataType",
                schema: "Modelling",
                table: "ActivityDatas");

            migrationBuilder.DropIndex(
                name: "IX_Activities_IdActivityDefinition",
                schema: "Modelling",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "IdNodeDataType",
                schema: "Modelling",
                table: "NodeDatas");

            migrationBuilder.DropColumn(
                name: "IdActivityDefinitionDataType",
                schema: "Modelling",
                table: "ActivityDatas");

            migrationBuilder.DropColumn(
                name: "IdActivityDefinition",
                schema: "Modelling",
                table: "Activities");

            migrationBuilder.EnsureSchema(
                name: "Common");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Modelling",
                table: "NodeDatas",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Processing",
                table: "InstanceDatas",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Modelling",
                table: "ActivityDatas",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DataDefinitions",
                schema: "Common",
                columns: table => new
                {
                    IdentifierName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DetailSettings = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EditSettings = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Group = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ShowSettings = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataDefinitions", x => x.IdentifierName);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NodeDatas_Name",
                schema: "Modelling",
                table: "NodeDatas",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceDatas_Name",
                schema: "Processing",
                table: "InstanceDatas",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDatas_Name",
                schema: "Modelling",
                table: "ActivityDatas",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityDatas_DataDefinitions_Name",
                schema: "Modelling",
                table: "ActivityDatas",
                column: "Name",
                principalSchema: "Common",
                principalTable: "DataDefinitions",
                principalColumn: "IdentifierName");

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceDatas_DataDefinitions_Name",
                schema: "Processing",
                table: "InstanceDatas",
                column: "Name",
                principalSchema: "Common",
                principalTable: "DataDefinitions",
                principalColumn: "IdentifierName");

            migrationBuilder.AddForeignKey(
                name: "FK_NodeDatas_DataDefinitions_Name",
                schema: "Modelling",
                table: "NodeDatas",
                column: "Name",
                principalSchema: "Common",
                principalTable: "DataDefinitions",
                principalColumn: "IdentifierName");
        }
    }
}

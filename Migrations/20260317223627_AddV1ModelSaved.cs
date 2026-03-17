using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ResourcesManager.Migrations
{
    /// <inheritdoc />
    public partial class AddV1ModelSaved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_resources_FamilyCode_ResourceName",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "CounterDown",
                table: "resource_texts");

            migrationBuilder.RenameColumn(
                name: "CounterUsed",
                table: "resource_texts",
                newName: "SeenCount");

            migrationBuilder.RenameColumn(
                name: "CounterUp",
                table: "resource_texts",
                newName: "CreatedByUserId");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "resources",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedImportBatchId",
                table: "resources",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "resources",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SeenCount",
                table: "resources",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedImportBatchId",
                table: "resource_texts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "import_batches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ImportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ImportedByUserId = table.Column<int>(type: "integer", nullable: false),
                    SourceGroupName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TotalRows = table.Column<int>(type: "integer", nullable: false),
                    AddedResources = table.Column<int>(type: "integer", nullable: false),
                    AddedTexts = table.Column<int>(type: "integer", nullable: false),
                    UpdatedTexts = table.Column<int>(type: "integer", nullable: false),
                    WarningsCount = table.Column<int>(type: "integer", nullable: false),
                    ErrorsCount = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_import_batches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_import_batches_users_ImportedByUserId",
                        column: x => x.ImportedByUserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_resources_CreatedByUserId",
                table: "resources",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_resources_CreatedImportBatchId",
                table: "resources",
                column: "CreatedImportBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_resources_FamilyCode",
                table: "resources",
                column: "FamilyCode");

            migrationBuilder.CreateIndex(
                name: "IX_resources_ResourceName",
                table: "resources",
                column: "ResourceName");

            migrationBuilder.CreateIndex(
                name: "IX_resource_texts_CreatedByUserId",
                table: "resource_texts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_resource_texts_CreatedImportBatchId",
                table: "resource_texts",
                column: "CreatedImportBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_resource_texts_ReplacesTextId",
                table: "resource_texts",
                column: "ReplacesTextId");

            migrationBuilder.CreateIndex(
                name: "IX_import_batches_ImportedByUserId",
                table: "import_batches",
                column: "ImportedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Login",
                table: "users",
                column: "Login",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_resource_texts_import_batches_CreatedImportBatchId",
                table: "resource_texts",
                column: "CreatedImportBatchId",
                principalTable: "import_batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_resource_texts_resource_texts_ReplacesTextId",
                table: "resource_texts",
                column: "ReplacesTextId",
                principalTable: "resource_texts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_resource_texts_users_CreatedByUserId",
                table: "resource_texts",
                column: "CreatedByUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_resources_import_batches_CreatedImportBatchId",
                table: "resources",
                column: "CreatedImportBatchId",
                principalTable: "import_batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_resources_users_CreatedByUserId",
                table: "resources",
                column: "CreatedByUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_resource_texts_import_batches_CreatedImportBatchId",
                table: "resource_texts");

            migrationBuilder.DropForeignKey(
                name: "FK_resource_texts_resource_texts_ReplacesTextId",
                table: "resource_texts");

            migrationBuilder.DropForeignKey(
                name: "FK_resource_texts_users_CreatedByUserId",
                table: "resource_texts");

            migrationBuilder.DropForeignKey(
                name: "FK_resources_import_batches_CreatedImportBatchId",
                table: "resources");

            migrationBuilder.DropForeignKey(
                name: "FK_resources_users_CreatedByUserId",
                table: "resources");

            migrationBuilder.DropTable(
                name: "import_batches");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropIndex(
                name: "IX_resources_CreatedByUserId",
                table: "resources");

            migrationBuilder.DropIndex(
                name: "IX_resources_CreatedImportBatchId",
                table: "resources");

            migrationBuilder.DropIndex(
                name: "IX_resources_FamilyCode",
                table: "resources");

            migrationBuilder.DropIndex(
                name: "IX_resources_ResourceName",
                table: "resources");

            migrationBuilder.DropIndex(
                name: "IX_resource_texts_CreatedByUserId",
                table: "resource_texts");

            migrationBuilder.DropIndex(
                name: "IX_resource_texts_CreatedImportBatchId",
                table: "resource_texts");

            migrationBuilder.DropIndex(
                name: "IX_resource_texts_ReplacesTextId",
                table: "resource_texts");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "CreatedImportBatchId",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "SeenCount",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "CreatedImportBatchId",
                table: "resource_texts");

            migrationBuilder.RenameColumn(
                name: "SeenCount",
                table: "resource_texts",
                newName: "CounterUsed");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "resource_texts",
                newName: "CounterUp");

            migrationBuilder.AddColumn<int>(
                name: "CounterDown",
                table: "resource_texts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_resources_FamilyCode_ResourceName",
                table: "resources",
                columns: new[] { "FamilyCode", "ResourceName" },
                unique: true);
        }
    }
}

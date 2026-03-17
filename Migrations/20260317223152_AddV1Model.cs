using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourcesManager.Migrations
{
    /// <inheritdoc />
    public partial class AddV1Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FamilyCode",
                table: "resources",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResourceName",
                table: "resources",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CounterDown",
                table: "resource_texts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CounterUp",
                table: "resource_texts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CounterUsed",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_resources_FamilyCode_ResourceName",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "FamilyCode",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "ResourceName",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "CounterDown",
                table: "resource_texts");

            migrationBuilder.DropColumn(
                name: "CounterUp",
                table: "resource_texts");

            migrationBuilder.DropColumn(
                name: "CounterUsed",
                table: "resource_texts");
        }
    }
}

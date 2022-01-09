using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonsAndDragons.Database.Migrations
{
    public partial class AddExtraConstraintsToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArmorId",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeaponId",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_ArmorId",
                table: "Players",
                column: "ArmorId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_WeaponId",
                table: "Players",
                column: "WeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Armors_ArmorId",
                table: "Players",
                column: "ArmorId",
                principalTable: "Armors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Weapons_WeaponId",
                table: "Players",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Armors_ArmorId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Weapons_WeaponId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_ArmorId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_WeaponId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ArmorId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "Players");
        }
    }
}

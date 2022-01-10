using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonsAndDragons.Database.Migrations
{
    public partial class ChangeMonsterModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monsters_Classes_ClassId",
                table: "Monsters");

            migrationBuilder.DropForeignKey(
                name: "FK_Monsters_Races_RaceId",
                table: "Monsters");

            migrationBuilder.DropIndex(
                name: "IX_Monsters_ClassId",
                table: "Monsters");

            migrationBuilder.DropIndex(
                name: "IX_Monsters_RaceId",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Characteristics_Charisma",
                table: "Monsters");

            migrationBuilder.RenameColumn(
                name: "RaceId",
                table: "Monsters",
                newName: "Weapon");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "Monsters",
                newName: "HitPoints");

            migrationBuilder.RenameColumn(
                name: "Characteristics_Wisdom",
                table: "Monsters",
                newName: "DamageModifier");

            migrationBuilder.RenameColumn(
                name: "Characteristics_Strength",
                table: "Monsters",
                newName: "Damage");

            migrationBuilder.RenameColumn(
                name: "Characteristics_Intelligence",
                table: "Monsters",
                newName: "AttackPerRound");

            migrationBuilder.RenameColumn(
                name: "Characteristics_Dexterity",
                table: "Monsters",
                newName: "AttackModifier");

            migrationBuilder.RenameColumn(
                name: "Characteristics_Constitution",
                table: "Monsters",
                newName: "ArmorClass");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weapon",
                table: "Monsters",
                newName: "RaceId");

            migrationBuilder.RenameColumn(
                name: "HitPoints",
                table: "Monsters",
                newName: "ClassId");

            migrationBuilder.RenameColumn(
                name: "DamageModifier",
                table: "Monsters",
                newName: "Characteristics_Wisdom");

            migrationBuilder.RenameColumn(
                name: "Damage",
                table: "Monsters",
                newName: "Characteristics_Strength");

            migrationBuilder.RenameColumn(
                name: "AttackPerRound",
                table: "Monsters",
                newName: "Characteristics_Intelligence");

            migrationBuilder.RenameColumn(
                name: "AttackModifier",
                table: "Monsters",
                newName: "Characteristics_Dexterity");

            migrationBuilder.RenameColumn(
                name: "ArmorClass",
                table: "Monsters",
                newName: "Characteristics_Constitution");

            migrationBuilder.AddColumn<int>(
                name: "Characteristics_Charisma",
                table: "Monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_ClassId",
                table: "Monsters",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_RaceId",
                table: "Monsters",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monsters_Classes_ClassId",
                table: "Monsters",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Monsters_Races_RaceId",
                table: "Monsters",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

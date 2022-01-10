using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonsAndDragons.Database.Migrations
{
    public partial class ChangeMonsterModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Damage",
                table: "Monsters",
                newName: "DamageMax");

            migrationBuilder.AddColumn<int>(
                name: "DamageCount",
                table: "Monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DamageCount",
                table: "Monsters");

            migrationBuilder.RenameColumn(
                name: "DamageMax",
                table: "Monsters",
                newName: "Damage");
        }
    }
}

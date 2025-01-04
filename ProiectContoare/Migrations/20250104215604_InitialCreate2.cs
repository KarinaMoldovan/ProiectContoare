using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectContoare.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contor_Consumator_ConsumatorId",
                table: "Contor");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Contor");

            migrationBuilder.AlterColumn<int>(
                name: "ConsumatorId",
                table: "Contor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contor_Consumator_ConsumatorId",
                table: "Contor",
                column: "ConsumatorId",
                principalTable: "Consumator",
                principalColumn: "ConsumatorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contor_Consumator_ConsumatorId",
                table: "Contor");

            migrationBuilder.AlterColumn<int>(
                name: "ConsumatorId",
                table: "Contor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Contor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Contor_Consumator_ConsumatorId",
                table: "Contor",
                column: "ConsumatorId",
                principalTable: "Consumator",
                principalColumn: "ConsumatorId");
        }
    }
}

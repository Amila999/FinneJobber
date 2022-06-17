using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinneJobber.DataAccess.Migrations
{
    public partial class gff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "JobCarts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobCarts_CategoryId",
                table: "JobCarts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobCarts_Categories_CategoryId",
                table: "JobCarts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobCarts_Categories_CategoryId",
                table: "JobCarts");

            migrationBuilder.DropIndex(
                name: "IX_JobCarts_CategoryId",
                table: "JobCarts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "JobCarts");
        }
    }
}

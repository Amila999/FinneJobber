using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinneJobber.DataAccess.Migrations
{
    public partial class blah : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApplied",
                table: "JobCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApplied",
                table: "JobCarts");
        }
    }
}

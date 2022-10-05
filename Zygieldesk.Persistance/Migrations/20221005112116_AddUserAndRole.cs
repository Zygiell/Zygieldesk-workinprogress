using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zygieldesk.Persistance.Migrations
{
    public partial class AddUserAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedByUserId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "TicketComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedByUserId",
                table: "TicketComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedByUserId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "TicketComments");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "TicketComments");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Categories");
        }
    }
}

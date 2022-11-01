using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zygieldesk.Persistance.Migrations
{
    public partial class AddTicketPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketPriority",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketPriority",
                table: "Tickets");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayWishes.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "MessageQueue",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemUniqueId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValue: "True"),
                    SourceRawJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBusyProcessing = table.Column<bool>(type: "bit", nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    MessageStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    MessageType = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageQueue", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageQueue",
                schema: "dbo");
        }
    }
}

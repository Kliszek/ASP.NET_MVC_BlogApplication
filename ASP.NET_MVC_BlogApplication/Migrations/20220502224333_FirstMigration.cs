using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_MVC_BlogApplication.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogEntries",
                columns: table => new
                {
                    BlogEntryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BlogID = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogEntries", x => x.BlogEntryID);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    BlogID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerID = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogEntries");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

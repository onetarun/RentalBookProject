using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRent.Infrastructure.Migrations
{
    /// <inheritdoc />
<<<<<<<< HEAD:BookRent.Infrastructure/Migrations/20250202092855_init_BookRentalDB.cs
    public partial class init_BookRentalDB : Migration
========
    public partial class BookRentalInitial1 : Migration
>>>>>>>> bef8cbf08484192274781109c2b037b78d856bff:BookRent.Infrastructure/Migrations/20250202112111_BookRentalInitial1.cs
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    GenreID = table.Column<int>(type: "int", nullable: false),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPages = table.Column<int>(type: "int", nullable: false),
                    BookDimensions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isdeleted = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreID",
                table: "Books",
                column: "GenreID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}

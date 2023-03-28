using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class issuebook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Books_BookID",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "BookID",
                table: "Members",
                newName: "IssueBookViewModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_BookID",
                table: "Members",
                newName: "IX_Members_IssueBookViewModelId");

            migrationBuilder.CreateTable(
                name: "IssueBookViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssueMemberID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueBookViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueBookViewModel_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BorrowerID",
                table: "Books",
                column: "BorrowerID");

            migrationBuilder.CreateIndex(
                name: "IX_IssueBookViewModel_BookID",
                table: "IssueBookViewModel",
                column: "BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Members_BorrowerID",
                table: "Books",
                column: "BorrowerID",
                principalTable: "Members",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_IssueBookViewModel_IssueBookViewModelId",
                table: "Members",
                column: "IssueBookViewModelId",
                principalTable: "IssueBookViewModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_BorrowerID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_IssueBookViewModel_IssueBookViewModelId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "IssueBookViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowerID",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "IssueBookViewModelId",
                table: "Members",
                newName: "BookID");

            migrationBuilder.RenameIndex(
                name: "IX_Members_IssueBookViewModelId",
                table: "Members",
                newName: "IX_Members_BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Books_BookID",
                table: "Members",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID");
        }
    }
}

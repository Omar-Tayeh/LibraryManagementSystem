using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class issuebook2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueBookViewModel_Books_BookID",
                table: "IssueBookViewModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_IssueBookViewModel_IssueBookViewModelId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueBookViewModel",
                table: "IssueBookViewModel");

            migrationBuilder.RenameTable(
                name: "IssueBookViewModel",
                newName: "IssueTransactions");

            migrationBuilder.RenameColumn(
                name: "IssueBookViewModelId",
                table: "Members",
                newName: "IssueBookViewModelIssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_IssueBookViewModelId",
                table: "Members",
                newName: "IX_Members_IssueBookViewModelIssueId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "IssueTransactions",
                newName: "IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueBookViewModel_BookID",
                table: "IssueTransactions",
                newName: "IX_IssueTransactions_BookID");

            migrationBuilder.AlterColumn<string>(
                name: "IssueDate",
                table: "IssueTransactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "DueDate",
                table: "IssueTransactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueTransactions",
                table: "IssueTransactions",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTransactions_Books_BookID",
                table: "IssueTransactions",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_IssueTransactions_IssueBookViewModelIssueId",
                table: "Members",
                column: "IssueBookViewModelIssueId",
                principalTable: "IssueTransactions",
                principalColumn: "IssueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTransactions_Books_BookID",
                table: "IssueTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_IssueTransactions_IssueBookViewModelIssueId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueTransactions",
                table: "IssueTransactions");

            migrationBuilder.RenameTable(
                name: "IssueTransactions",
                newName: "IssueBookViewModel");

            migrationBuilder.RenameColumn(
                name: "IssueBookViewModelIssueId",
                table: "Members",
                newName: "IssueBookViewModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_IssueBookViewModelIssueId",
                table: "Members",
                newName: "IX_Members_IssueBookViewModelId");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "IssueBookViewModel",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_IssueTransactions_BookID",
                table: "IssueBookViewModel",
                newName: "IX_IssueBookViewModel_BookID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssueDate",
                table: "IssueBookViewModel",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "IssueBookViewModel",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueBookViewModel",
                table: "IssueBookViewModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueBookViewModel_Books_BookID",
                table: "IssueBookViewModel",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_IssueBookViewModel_IssueBookViewModelId",
                table: "Members",
                column: "IssueBookViewModelId",
                principalTable: "IssueBookViewModel",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTransactions_Books_BookID",
                table: "IssueTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_IssueTransactions_IssueBookViewModelIssueId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_IssueBookViewModelIssueId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_IssueTransactions_BookID",
                table: "IssueTransactions");

            migrationBuilder.DropColumn(
                name: "IssueBookViewModelIssueId",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "IssueMemberID",
                table: "IssueTransactions",
                newName: "MemberID");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "IssueTransactions",
                newName: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemberID",
                table: "IssueTransactions",
                newName: "IssueMemberID");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "IssueTransactions",
                newName: "IssueId");

            migrationBuilder.AddColumn<int>(
                name: "IssueBookViewModelIssueId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_IssueBookViewModelIssueId",
                table: "Members",
                column: "IssueBookViewModelIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueTransactions_BookID",
                table: "IssueTransactions",
                column: "BookID");

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
    }
}

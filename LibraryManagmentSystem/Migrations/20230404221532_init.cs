using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueTransactions",
                table: "IssueTransactions");

            migrationBuilder.RenameTable(
                name: "IssueTransactions",
                newName: "IssueTransaction");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "IssueTransaction",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueTransaction",
                table: "IssueTransaction",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueTransaction",
                table: "IssueTransaction");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "IssueTransaction");

            migrationBuilder.RenameTable(
                name: "IssueTransaction",
                newName: "IssueTransactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueTransactions",
                table: "IssueTransactions",
                column: "TransactionId");
        }
    }
}

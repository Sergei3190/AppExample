using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExample.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employess_companies_CompanyId",
                schema: "app",
                table: "employess");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                schema: "app",
                table: "employess",
                newName: "company_id");

            migrationBuilder.RenameIndex(
                name: "IX_employess_CompanyId",
                schema: "app",
                table: "employess",
                newName: "IX_employess_company_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "app",
                table: "companies",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_companies_Name",
                schema: "app",
                table: "companies",
                newName: "IX_companies_name");

            migrationBuilder.AddForeignKey(
                name: "FK_employess_companies_company_id",
                schema: "app",
                table: "employess",
                column: "company_id",
                principalSchema: "app",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employess_companies_company_id",
                schema: "app",
                table: "employess");

            migrationBuilder.RenameColumn(
                name: "company_id",
                schema: "app",
                table: "employess",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_employess_company_id",
                schema: "app",
                table: "employess",
                newName: "IX_employess_CompanyId");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "app",
                table: "companies",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_companies_name",
                schema: "app",
                table: "companies",
                newName: "IX_companies_Name");

            migrationBuilder.AddForeignKey(
                name: "FK_employess_companies_CompanyId",
                schema: "app",
                table: "employess",
                column: "CompanyId",
                principalSchema: "app",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

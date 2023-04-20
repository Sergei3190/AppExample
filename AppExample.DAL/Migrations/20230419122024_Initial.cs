using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExample.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "app",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employess",
                schema: "app",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    middle_name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employess", x => x.id);
                    table.ForeignKey(
                        name: "FK_employess_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "app",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_companies_Name",
                schema: "app",
                table: "companies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employess_age",
                schema: "app",
                table: "employess",
                column: "age");

            migrationBuilder.CreateIndex(
                name: "IX_employess_CompanyId",
                schema: "app",
                table: "employess",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_employess_first_name",
                schema: "app",
                table: "employess",
                column: "first_name");

            migrationBuilder.CreateIndex(
                name: "IX_employess_last_name_first_name_middle_name_age",
                schema: "app",
                table: "employess",
                columns: new[] { "last_name", "first_name", "middle_name", "age" },
                unique: true,
                filter: "[middle_name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_employess_middle_name",
                schema: "app",
                table: "employess",
                column: "middle_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employess",
                schema: "app");

            migrationBuilder.DropTable(
                name: "companies",
                schema: "app");
        }
    }
}

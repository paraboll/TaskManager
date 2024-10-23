using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TM.Persistence.Migrator.CLI.Migrations.PostgreSql
{
    public partial class InitialDbPostgres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TM_SCHEMA");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "TM_SCHEMA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    HashPassword = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuisnessTasks",
                schema: "TM_SCHEMA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExternalId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuisnessTasks_Employees_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "TM_SCHEMA",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuisnessTasks_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "TM_SCHEMA",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskComment",
                schema: "TM_SCHEMA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExternalId = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BuisnessTaskId = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskComment_BuisnessTasks_BuisnessTaskId",
                        column: x => x.BuisnessTaskId,
                        principalSchema: "TM_SCHEMA",
                        principalTable: "BuisnessTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskComment_Employees_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "TM_SCHEMA",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessTasks_AuthorId",
                schema: "TM_SCHEMA",
                table: "BuisnessTasks",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessTasks_EmployeeId",
                schema: "TM_SCHEMA",
                table: "BuisnessTasks",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessTasks_ExternalId",
                schema: "TM_SCHEMA",
                table: "BuisnessTasks",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Login",
                schema: "TM_SCHEMA",
                table: "Employees",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskComment_AuthorId",
                schema: "TM_SCHEMA",
                table: "TaskComment",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComment_BuisnessTaskId",
                schema: "TM_SCHEMA",
                table: "TaskComment",
                column: "BuisnessTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComment_ExternalId",
                schema: "TM_SCHEMA",
                table: "TaskComment",
                column: "ExternalId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskComment",
                schema: "TM_SCHEMA");

            migrationBuilder.DropTable(
                name: "BuisnessTasks",
                schema: "TM_SCHEMA");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "TM_SCHEMA");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmsiStudySpace.Migrations
{
    /// <inheritdoc />
    public partial class historiquemig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ce1d8cd-a59b-4faa-a259-4b546bc9025e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d47aaea3-1ae0-4d74-8401-05ca5d41c076");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1a1f5ff-e0fa-44f2-ab27-e35881185608");

            migrationBuilder.CreateTable(
                name: "UserConnectionHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnectionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConnectionHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b143cc3-1f12-46fe-8635-4d3b7ceb0e4a", null, "Admin", "ADMIN" },
                    { "61a740cf-6853-408c-96be-ba003863f571", null, "Etudiant", "ETUDIANT" },
                    { "eb613806-f5a1-4687-9e86-b6ae348550c4", null, "Professeur", "PROFESSEUR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserConnectionHistories_UserId",
                table: "UserConnectionHistories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConnectionHistories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b143cc3-1f12-46fe-8635-4d3b7ceb0e4a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61a740cf-6853-408c-96be-ba003863f571");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb613806-f5a1-4687-9e86-b6ae348550c4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ce1d8cd-a59b-4faa-a259-4b546bc9025e", null, "Etudiant", "ETUDIANT" },
                    { "d47aaea3-1ae0-4d74-8401-05ca5d41c076", null, "Professeur", "PROFESSEUR" },
                    { "f1a1f5ff-e0fa-44f2-ab27-e35881185608", null, "Admin", "ADMIN" }
                });
        }
    }
}

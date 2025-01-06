using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmsiStudySpace.Migrations
{
    /// <inheritdoc />
    public partial class reservationUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HeureDebut",
                table: "Reservations",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HeureFin",
                table: "Reservations",
                type: "time",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "735e6a52-4764-4830-bf24-6fbb4f6480a5", null, "Admin", "ADMIN" },
                    { "844e8af2-6485-4434-85b9-fdb60c4fad42", null, "Professeur", "PROFESSEUR" },
                    { "857fdf24-b85d-47a5-a68d-02dfe8b1b918", null, "Etudiant", "ETUDIANT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "735e6a52-4764-4830-bf24-6fbb4f6480a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "844e8af2-6485-4434-85b9-fdb60c4fad42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "857fdf24-b85d-47a5-a68d-02dfe8b1b918");

            migrationBuilder.DropColumn(
                name: "HeureDebut",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "HeureFin",
                table: "Reservations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b143cc3-1f12-46fe-8635-4d3b7ceb0e4a", null, "Admin", "ADMIN" },
                    { "61a740cf-6853-408c-96be-ba003863f571", null, "Etudiant", "ETUDIANT" },
                    { "eb613806-f5a1-4687-9e86-b6ae348550c4", null, "Professeur", "PROFESSEUR" }
                });
        }
    }
}

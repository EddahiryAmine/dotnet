using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmsiStudySpace.Migrations
{
    /// <inheritdoc />
    public partial class upReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2468fdaa-57bb-4202-ae59-5d4c5ee6fa1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b015aad-fc84-43a6-9158-fc7e3562d93d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca753199-5803-4a8d-b8e6-88947cde825f");

            migrationBuilder.DropColumn(
                name: "EstApprouve",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "Statut",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Reservations");

            migrationBuilder.AddColumn<bool>(
                name: "EstApprouve",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2468fdaa-57bb-4202-ae59-5d4c5ee6fa1c", null, "Etudiant", "ETUDIANT" },
                    { "4b015aad-fc84-43a6-9158-fc7e3562d93d", null, "Professeur", "PROFESSEUR" },
                    { "ca753199-5803-4a8d-b8e6-88947cde825f", null, "Admin", "ADMIN" }
                });
        }
    }
}

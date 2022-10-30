using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement.Migrations
{
    public partial class x : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bb80575-a02f-45e3-9504-1f225cbf237e",
                column: "ConcurrencyStamp",
                value: "dd890989-3bb8-44c9-aff0-4ed3581155c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "989c13c2-05a1-471b-abe4-1aecf8485887",
                column: "ConcurrencyStamp",
                value: "1c7b5de2-aa3b-44e2-9ab7-b5739adf1ce0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5393722f-d39e-4bb1-8a5e-9641e3ce4a25",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "484e9842-738d-4bfe-b287-344017af26a7", "AQAAAAEAACcQAAAAEH6T1fmj+Yr5HAupp2iZR5rkHMEdUIMfpmSpRXmMUJnlCkoi1vJJcnbJXRP0/uq6kw==", "262349cd-c377-412d-b270-6083c200fbe1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d91481c-2946-4b3d-9117-072c4953475e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "057cb934-2a22-4cbf-9e83-ee5e1f9765c5", "AQAAAAEAACcQAAAAEGiZ0mUEzaYkWFBxo7A98OOtyoLLRo2UDqgY6s2sVhZpgvDDPS75PoKxUKBVe/cgkw==", "958d72aa-d7cc-4afa-8383-e0aac36b5666" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89c4435a-6986-4995-b514-2aec9af0ac3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9828aba1-d0e1-402a-b4e3-d96d2590168f", "AQAAAAEAACcQAAAAEASfTqtbfJandGm7RMmuVIsjK+K9o/AK/lOHhWWLx/a9MZvKpI+9MMQbSO3rfYl7yQ==", "1545969d-77b9-4cf7-a8a0-454e8b6ccdfe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bb80575-a02f-45e3-9504-1f225cbf237e",
                column: "ConcurrencyStamp",
                value: "65261979-f5ee-4e2d-bfa6-bec1ff95c639");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "989c13c2-05a1-471b-abe4-1aecf8485887",
                column: "ConcurrencyStamp",
                value: "fddeb99c-5428-4ec5-bb85-74a78ced67bd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5393722f-d39e-4bb1-8a5e-9641e3ce4a25",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4508485d-1778-4812-8082-08f08217582f", "AQAAAAEAACcQAAAAEIjq+ONSmqXck39RkxUo3IhuVyAxeZgsyfakvL5P7Uv8IjevNcC+f6UeMqg+Xp0L0w==", "514deaaa-0982-408e-89d6-f8efa28cead1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d91481c-2946-4b3d-9117-072c4953475e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0d19f4e-2828-4219-a2b9-ff457ba079c0", "AQAAAAEAACcQAAAAEFheTMjRDo+bhBCDiWI/fB1xXDOnIqfrY1Uupw9Txyz7xdhVljz+1VsGS2M5SR0rnQ==", "1b341c4d-83c9-41b1-8862-48557af14558" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89c4435a-6986-4995-b514-2aec9af0ac3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76a0f7e4-fcec-4fec-a28a-850343c7c687", "AQAAAAEAACcQAAAAECeaF9//g/okO3oakc6znuRiJFiXUzV/8Rq1ZRQ70ynugrqbOEMWHhpUD3giR23R/A==", "7e98ee4c-6ec6-466b-be2e-85ca10084f00" });
        }
    }
}
